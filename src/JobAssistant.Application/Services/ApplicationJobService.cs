using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Models;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Services;

internal class ApplicationJobService(
    IApplicationJobStore store,
    IApplicationJobQueue queue) : IApplicationJobService
{
    public async Task<Guid> CreateJobAsync(JobSubmission jobSubmission, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(jobSubmission);
        
        var applicationJob = new ApplicationJob
        {
            Id = Guid.NewGuid(),
            Submission = jobSubmission,
            CreatedAt = DateTimeOffset.UtcNow,
            Status = ApplicationJobStatus.Pending
        };
        await store.CreateJobAsync(applicationJob, cancellationToken);
        await queue.EnqueueAsync(applicationJob.Id, cancellationToken);
        return applicationJob.Id;
    }

    public async Task<ApplicationJob> GetJobAsync(Guid jobId, CancellationToken cancellationToken)
    {
        return await store.GetByIdAsync(jobId, cancellationToken);
    }
}