using JobAssistant.Application.Models;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Abstractions;

public interface IApplicationJobService
{
    Task<Guid> CreateJobAsync(JobSubmission jobSubmission, CancellationToken cancellationToken);
    
    Task<ApplicationJob> GetJobAsync(Guid jobId, CancellationToken cancellationToken);
}