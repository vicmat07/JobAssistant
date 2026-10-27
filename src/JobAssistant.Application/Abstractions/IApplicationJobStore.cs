using JobAssistant.Application.Models;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Abstractions;

public interface IApplicationJobStore
{
    Task CreateJobAsync(ApplicationJob jobSubmission, CancellationToken cancellationToken);
    
    Task UpdateJobAsync(ApplicationJob jobSubmission, CancellationToken cancellationToken);
    
    Task<ApplicationJob> GetByIdAsync(Guid jobId, CancellationToken cancellationToken);
}