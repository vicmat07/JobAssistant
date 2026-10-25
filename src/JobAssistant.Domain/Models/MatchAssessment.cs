namespace JobAssistant.Domain.Models;

public record MatchAssessment(
    int FitScore,
    string Recommendation,
    IReadOnlyList<RequirementMatch> RequirementMatches,
    IReadOnlyList<string> Strengths,
    IReadOnlyList<string> Gaps,
    IReadOnlyList<string> Unknowns,
    IReadOnlyList<string> ApprovedEvidenceIds);