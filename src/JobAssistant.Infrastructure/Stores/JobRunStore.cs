using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Models;

namespace JobAssistant.Infrastructure.Stores;

internal class JobRunStore : IJobRunStore
{
    public Task SaveAsync(WorkflowHistoryEntry entry, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<WorkflowHistoryEntry?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult<WorkflowHistoryEntry?>(null);
    }
}