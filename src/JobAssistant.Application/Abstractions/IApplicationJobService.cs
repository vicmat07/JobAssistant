using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Abstractions;

public interface IApplicationJobService
{
    Task<Guid> CreateJobAsync(JobSubmission jobSubmission, CancellationToken cancellationToken);
}