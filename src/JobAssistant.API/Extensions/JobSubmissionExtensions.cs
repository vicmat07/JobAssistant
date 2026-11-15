using JobAssistant.API.Models;
using JobAssistant.Domain.Models;

namespace JobAssistant.API.Extensions;

public static class JobSubmissionExtensions
{
    public static JobSubmission ToModel(this JobSubmissionRequest request, CandidateProfile profile)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new JobSubmission(
            request.JobDescription, 
            profile,
            request.CompanyName,
            request.JobTitle,
            request.JobUrl);
    }
}