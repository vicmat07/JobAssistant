namespace JobAssistant.Domain.Models;

public record CandidateProfile(
    Guid Id,
    string Name,
    string Summary,
    ICollection<ExperienceItem> ExperienceItems,
    ICollection<LanguageItem> Languages,
    ICollection<string> Skills);