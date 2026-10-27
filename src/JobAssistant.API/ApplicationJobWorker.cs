using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Models;
using JobAssistant.Application.Services;

namespace JobAssistant.API;

internal class ApplicationJobWorker(
    IServiceScopeFactory scopeFactory,
    IApplicationJobQueue queue,
    ILogger<ApplicationJobWorker> logger) : BackgroundService
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
            }
        }
    }

    private async Task ProcessJobAsync(Guid applicationJobId, CancellationToken stoppingToken)
    {
        using var scope = scopeFactory.CreateScope();
        var store = scope.ServiceProvider.GetRequiredService<IApplicationJobStore>();
        var workflow = scope.ServiceProvider.GetRequiredService<ApplicationWorkflow>();
        var docxService = scope.ServiceProvider.GetRequiredService<IDocxGenerator>();
            
        var applicationJob = await store.GetByIdAsync(applicationJobId, stoppingToken);
        var result = await workflow.RunAsync(applicationJob.Submission, stoppingToken);
            
        await docxService.GenerateResumeAsync(result.Draft, result.RunId, stoppingToken);
        await docxService.GenerateCoverLetterAsync(result.Draft, result.RunId, stoppingToken);
            
        applicationJob.Result = result;
        applicationJob.Status = ApplicationJobStatus.Completed;
        applicationJob.CompletedAt = DateTimeOffset.UtcNow;

        await store.UpdateJobAsync(applicationJob, stoppingToken);
    }
}