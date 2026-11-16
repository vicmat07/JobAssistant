using JobAssistant.API.Models;
using JobAssistant.Domain.Models;

namespace JobAssistant.API.Extensions;

public static class CandidateProfileExtensions
{
    public static CandidateProfile ToCandidateProfile(this CreateProfileRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new CandidateProfile(
            Guid.NewGuid(),
            request.Name,
            request.Summary,
            request.ExperienceItems,
            request.Languages,
            request.Skills);
    }
}