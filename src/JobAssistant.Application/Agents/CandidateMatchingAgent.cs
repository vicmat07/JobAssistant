using JobAssistant.Application.Abstractions;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Agents;

internal class CandidateMatchingAgent(IStructuredLlmClient llmClient) : IAgent<MatchingInput, MatchAssessment>
{
    public async Task<MatchAssessment> ExecuteAsync(MatchingInput input, CancellationToken cancellationToken = default)
    {
        return await llmClient.ExecuteAsync<MatchAssessment>(
            """
            You are the Candidate Matching Agent. Compare requirements only to the supplied candidate profile.
            Never assume skills, dates, employers, metrics, credentials, or achievements. For each requirement,
            status must be "matched", "partial", "not evidenced", or "unknown". Include evidence and notes.
            FitScore is an estimate from 0 to 100, not a probability. Recommendation must be "strong fit",
            "possible fit", or "weak fit". ApprovedEvidenceIds may use only these forms:
            skill:{exact skill}; experience:{zero-based index}; education:{zero-based index};
            certification:{zero-based index}. Separate strengths, gaps, and unknowns. Return JSON only.
            """, input, cancellationToken);
    }
}