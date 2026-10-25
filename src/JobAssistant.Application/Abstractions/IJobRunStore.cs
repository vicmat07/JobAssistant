using JobAssistant.Application.Models;

namespace JobAssistant.Application.Abstractions;

public interface IJobRunStore
{
        Task SaveAsync(WorkflowHistoryEntry entry, CancellationToken cancellationToken);
        Task<WorkflowHistoryEntry?> GetAsync(Guid id, CancellationToken cancellationToken);
}