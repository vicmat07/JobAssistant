namespace JobAssistant.Domain.Models;

public record JobRequirement(
    string RoleTitle,
    string Seniority,
    IReadOnlyList<RequirementItem> Requirements,
    IReadOnlyList<string> Responsibilities,
    IReadOnlyList<string> DomainSignals,
    IReadOnlyList<string> Uncertainties);