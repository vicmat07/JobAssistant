using System.Text.Json;
using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Models;
using JobAssistant.Domain;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Services;

public class ApplicationWorkflow(
    IAgent<JobSubmission, JobRequirement> jobAnalysis,
    IAgent<MatchingInput, MatchAssessment> matching,
    IAgent<WriterInput, ApplicationDraft> writer,
    IAgent<ReviewInput, ValidationReport> reviewer,
    IDocxGenerator docx,
    IJobRunStore historyStore)
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    
    public async Task<ApplicationWorkflowResult> RunAsync(JobSubmission submission, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(submission);
        ArgumentException.ThrowIfNullOrWhiteSpace(submission.JobDescription);
        ArgumentNullException.ThrowIfNull(submission.CandidateProfile);

        var id = Guid.NewGuid();
        var created = DateTimeOffset.UtcNow;
        var inputJson = JsonSerializer.Serialize(submission, JsonOptions);
        await historyStore.SaveAsync(new(id, created, "running", inputJson, null, null), ct);

        try
        {
            var requirements = await jobAnalysis.ExecuteAsync(submission, ct);
            var assessment = await matching.ExecuteAsync(new(requirements, submission.CandidateProfile), ct);
            var draft = await writer.ExecuteAsync(new(requirements, assessment, submission.CandidateProfile), ct);
            var validation = await reviewer.ExecuteAsync(new(submission, requirements, assessment, draft), ct);

            var resumePath = await docx.GenerateResumeAsync(draft, id, ct);
            var letterPath = await docx.GenerateCoverLetterAsync(draft, id, ct);
            var result = new ApplicationWorkflowResult(
                id, created, requirements, assessment, draft, validation, resumePath, letterPath);

            await historyStore.SaveAsync(new(
                id, created, validation.Passed ? "completed" : "needs-review",
                inputJson, JsonSerializer.Serialize(result, JsonOptions), null), ct);
            return result;
        }
        catch (Exception ex)
        {
            await historyStore.SaveAsync(new(id, created, "failed", inputJson, null, ex.Message), ct);
            throw;
        }
    }
}