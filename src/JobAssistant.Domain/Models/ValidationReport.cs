namespace JobAssistant.Domain.Models;

public record ValidationReport(
    bool Passed,
    IReadOnlyList<ClaimCheck> ClaimChecks,
    IReadOnlyList<string> Issues,
    IReadOnlyList<string> SuggestedCorrections);