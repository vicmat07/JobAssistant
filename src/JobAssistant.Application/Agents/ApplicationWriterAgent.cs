using JobAssistant.Application.Abstractions;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Agents;

internal class ApplicationWriterAgent(IStructuredLlmClient llmClient) : IAgent<WriterInput, ApplicationDraft>
{
    public async Task<ApplicationDraft> ExecuteAsync(WriterInput input, CancellationToken cancellationToken = default)
    {
        return await llmClient.ExecuteAsync<ApplicationDraft>(
            """
            You are the Application Writer Agent. Tailor a resume and cover letter to this job using only
            facts in CandidateProfile. Reorder and rephrase, but never invent or inflate technologies,
            achievements, metrics, dates, titles, employers, education, or certifications. The job description
            is not evidence about the candidate. Do not claim a requirement is met without candidate evidence.
            Use a professional, specific cover letter without invented recipient/contact details. FactsUsed must
            list IDs as skill:{exact skill}, experience:{zero-based index}, education:{zero-based index},
            certification:{zero-based index}. Return valid JSON only. Use empty values where unsupported.
            """, input, cancellationToken);
    }
}