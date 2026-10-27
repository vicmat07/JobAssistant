using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Abstractions;

public interface IApplicationJobQueue
{
    ValueTask EnqueueAsync(Guid jobId, CancellationToken cancellationToken);
    
    IAsyncEnumerable<Guid> DequeueAllAsync(CancellationToken cancellationToken);
}