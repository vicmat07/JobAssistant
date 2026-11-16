using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Models;
using JobAssistant.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace JobAssistant.API;

internal class ApplicationJobConsumer(
    IServiceScopeFactory scopeFactory,
    IApplicationJobQueue queue,
    ILogger<ApplicationJobConsumer> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await foreach (var jobId in queue.DequeueAllAsync(stoppingToken))
            {
                using var logScope = logger.BeginScope("Starting processing: {ReferenceId}", jobId);
                await ProcessJobAsync(jobId, stoppingToken);
                logger.LogInformation("Job {JobId} processed", jobId);
                
                await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            }
        }
    }

    private async Task ProcessJobAsync(Guid applicationJobId, CancellationToken stoppingToken)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var workflow = scope.ServiceProvider.GetRequiredService<ApplicationWorkflow>();
        var docxService = scope.ServiceProvider.GetRequiredService<IDocxGenerator>();
            
        var applicationJob = await context.ApplicationJobs
                                 .Include(applicationJob => applicationJob.Submission)
                                 .FirstOrDefaultAsync(x => x.Id == applicationJobId, stoppingToken) 
                             ?? throw new KeyNotFoundException($"Job {applicationJobId} could not be found");
        var result = await workflow.RunAsync(applicationJob.Submission, stoppingToken);
            
        await docxService.GenerateResumeAsync(result.Draft, result.RunId, stoppingToken);
        await docxService.GenerateCoverLetterAsync(result.Draft, result.RunId, stoppingToken);
            
        applicationJob.Result = result;
        applicationJob.Status = ApplicationJobStatus.Completed;
        applicationJob.CompletedAt = DateTimeOffset.UtcNow;

        context.ApplicationJobs.Update(applicationJob);
        await context.SaveChangesAsync(stoppingToken);
    }
}