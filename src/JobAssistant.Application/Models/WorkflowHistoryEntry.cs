namespace JobAssistant.Application.Models;

public sealed record WorkflowHistoryEntry(
    Guid Id, 
    DateTimeOffset CreatedAt,
    string Status,
    string InputJson,
    string? ResultJson,
    string? Error);