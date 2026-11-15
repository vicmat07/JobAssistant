using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Abstractions;

public interface ICandidateProfileService
{
    Task<Guid> CreateProfileAsync(CandidateProfile candidateProfile, CancellationToken cancellationToken = default);
    
    Task<CandidateProfile> GetByIdAsync(Guid candidateProfileId, CancellationToken cancellationToken = default);
}