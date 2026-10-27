using System.Threading.Channels;
using JobAssistant.Application.Abstractions;

namespace JobAssistant.Infrastructure.Services;

internal class ApplicationJobQueue : IApplicationJobQueue
{
    private readonly Channel<Guid> _channel = Channel.CreateUnbounded<Guid>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });
    
    public ValueTask EnqueueAsync(Guid jobId, CancellationToken cancellationToken)
    {
        return _channel.Writer.WriteAsync(jobId, cancellationToken);
    }
    
    public IAsyncEnumerable<Guid> DequeueAllAsync(CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }
}