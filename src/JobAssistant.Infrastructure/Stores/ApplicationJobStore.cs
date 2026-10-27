using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAssistant.Infrastructure.Stores;

public class ApplicationJobStore(ApplicationDbContext context) : IApplicationJobStore
{
    public async Task CreateJobAsync(ApplicationJob jobSubmission, CancellationToken cancellationToken)
    {
        await context.AddAsync(jobSubmission, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateJobAsync(ApplicationJob jobSubmission, CancellationToken cancellationToken)
    {
        context.Update(jobSubmission);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ApplicationJob> GetByIdAsync(Guid jobId, CancellationToken cancellationToken)
    {
        return await context.ApplicationJobs
            .SingleAsync(x => x.Id == jobId, cancellationToken);
    }
}