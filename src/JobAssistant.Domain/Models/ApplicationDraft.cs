namespace JobAssistant.Domain.Models;

public record ApplicationDraft(
    string ResumeHeadline,
    string ProfessionalSummary,
    IReadOnlyList<string> CoreSkills,
    IReadOnlyList<ResumeExperience> Experience,
    IReadOnlyList<string> Education,
    IReadOnlyList<string> Certifications,
    string CoverLetter,
    IReadOnlyList<string> FactsUsed);