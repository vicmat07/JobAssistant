namespace JobAssistant.Infrastructure.Configurations;

public class OllamaOptions
{
    public const string Name = "Ollama";

    public int TimeoutSeconds { get; set; }

    public string Model { get; set; } = null!;

    public string BaseUrl { get; set; } = null!;
}