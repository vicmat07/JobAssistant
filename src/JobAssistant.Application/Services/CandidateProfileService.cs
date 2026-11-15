using JobAssistant.Application.Abstractions;
using JobAssistant.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAssistant.Application.Services;

internal class CandidateProfileService(ApplicationDbContext context) : ICandidateProfileService
{
    public async Task<Guid> CreateProfileAsync(CandidateProfile candidateProfile, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(candidateProfile);

        ValidateProfile(candidateProfile);
        
        await context.CandidateProfiles.AddAsync(candidateProfile, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return candidateProfile.Id;
    }

    public async Task<CandidateProfile> GetByIdAsync(Guid candidateProfileId, CancellationToken cancellationToken = default)
    {
        return await context.CandidateProfiles.SingleOrDefaultAsync(x => x.Id == candidateProfileId, cancellationToken)
            ?? throw new KeyNotFoundException();
    }

    private static void ValidateProfile(CandidateProfile profile)
    {
        if (profile.ExperienceItems.Count == 0)
        {
            throw new ArgumentException(nameof(profile.ExperienceItems));
        }

        if (profile.Skills.Count == 0 || profile.Skills.All(string.IsNullOrEmpty))
        {
            throw new ArgumentException(nameof(profile.Skills));
        }
    }
}