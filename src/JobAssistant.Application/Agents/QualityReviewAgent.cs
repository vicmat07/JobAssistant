using JobAssistant.Application.Abstractions;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Agents;

internal class QualityReviewAgent(IStructuredLlmClient llmClient) : IAgent<ReviewInput, ValidationReport>
{
    public async Task<ValidationReport> ExecuteAsync(ReviewInput input, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        
        return await llmClient.ExecuteAsync<ValidationReport>(
            """
            You are the Quality and Fact-Checking Agent. Audit every resume and cover-letter claim against
            CandidateProfile. The job description is not evidence about the candidate. Flag unsupported skills,
            metrics, dates, titles, employers, certifications, or accomplishments. Passed is true only when
            no material unsupported claim is found. Be conservative. Return valid JSON only.
            """, input, cancellationToken);
    }
}