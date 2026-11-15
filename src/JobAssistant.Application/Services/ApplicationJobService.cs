using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Models;
using JobAssistant.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAssistant.Application.Services;

internal class ApplicationJobService(
    ApplicationDbContext context,
    IApplicationJobQueue queue) : IApplicationJobService
{
    public async Task<Guid> CreateJobAsync(JobSubmission jobSubmission, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(jobSubmission);

        ValidateJobSubmission(jobSubmission);
        
        var applicationJob = new ApplicationJob
        {
            Id = Guid.NewGuid(),
            Submission = jobSubmission,
            CreatedAt = DateTimeOffset.UtcNow,
            Status = ApplicationJobStatus.Pending
        };
        await context.ApplicationJobs.AddAsync(applicationJob, cancellationToken);
        await queue.EnqueueAsync(applicationJob.Id, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return applicationJob.Id;
    }

    public async Task<ApplicationJob> GetJobAsync(Guid jobId, CancellationToken cancellationToken)
    {
        return await context.ApplicationJobs.SingleOrDefaultAsync(x => x.Id == jobId, cancellationToken) 
               ?? throw new KeyNotFoundException();
    }

    private void ValidateJobSubmission(JobSubmission jobSubmission)
    {
        if (string.IsNullOrEmpty(jobSubmission.JobDescription))
        {
            throw new ArgumentException("Job Description cannot be null or empty");
        }
    }
}