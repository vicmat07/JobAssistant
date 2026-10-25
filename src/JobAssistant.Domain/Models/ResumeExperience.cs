namespace JobAssistant.Domain.Models;

public record ResumeExperience(
    string Employer,
    string Title,
    string Dates,
    IReadOnlyList<string> Bullets,
    IReadOnlyList<string> Technologies);