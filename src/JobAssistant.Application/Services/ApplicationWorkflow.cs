using System.Text.Json;
using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Models;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Services;

public class ApplicationWorkflow(
    IAgent<JobSubmission, JobRequirement> jobAnalysis,
    IAgent<MatchingInput, MatchAssessment> matching,
    IAgent<WriterInput, ApplicationDraft> writer,
    IAgent<ReviewInput, ValidationReport> reviewer)
{
    public async Task<ApplicationWorkflowResult> RunAsync(JobSubmission submission, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(submission);
        ArgumentException.ThrowIfNullOrWhiteSpace(submission.JobDescription);
        ArgumentNullException.ThrowIfNull(submission.CandidateProfile);

        var id = Guid.NewGuid();
        var created = DateTimeOffset.UtcNow;

        var requirements = await jobAnalysis.ExecuteAsync(submission, ct);
        var assessment = await matching.ExecuteAsync(new(requirements, submission.CandidateProfile), ct);
        var draft = await writer.ExecuteAsync(new(requirements, assessment, submission.CandidateProfile), ct);
        var validation = await reviewer.ExecuteAsync(new(submission, requirements, assessment, draft), ct);
        
        var result = new ApplicationWorkflowResult(
            id, created, requirements, assessment, draft, validation);

        return result;
    }
}