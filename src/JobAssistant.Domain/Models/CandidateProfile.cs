namespace JobAssistant.Domain.Models;

public record CandidateProfile(
    string Name,
    string Summary,
    ICollection<ExperienceItem> ExperienceItems,
    ICollection<LanguageItem> Languages,
    ICollection<string> Skills);