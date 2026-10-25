namespace JobAssistant.Domain.Models;

public record RequirementMatch(
    string Requirement,
    string Status,
    string CandidateEvidence,
    string Notes);