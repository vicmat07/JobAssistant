namespace JobAssistant.Domain.Models;

public record JobSubmission(
    string JobDescription,
    CandidateProfile CandidateProfile,
    string? CompanyName = null,
    string? JobTitle = null,
    string? JobUrl = null);