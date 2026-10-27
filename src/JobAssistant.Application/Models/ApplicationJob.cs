using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Models;

public class ApplicationJob
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public ApplicationJobStatus Status { get; set; } = ApplicationJobStatus.Pending;

    public required JobSubmission Submission { get; init; }

    public ApplicationWorkflowResult? Result { get; set; }

    public string? Error { get; set; }

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? CompletedAt { get; set; }
}

public enum ApplicationJobStatus
{
    Pending,
    Running,
    Completed,
    Failed
}
