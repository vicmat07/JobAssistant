namespace JobAssistant.Domain.Models;

public record ExperienceItem(
    string Employer,
    string Title,
    DateTime FromDate,
    DateTime? ToDate,
    string Description,
    ICollection<string> Technologies);