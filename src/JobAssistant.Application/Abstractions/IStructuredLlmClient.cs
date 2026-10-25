namespace JobAssistant.Application.Abstractions;

public interface IStructuredLlmClient
{
    Task<TOutput> ExecuteAsync<TOutput>(string systemPrompt, object input, CancellationToken cancellationToken = default);
}