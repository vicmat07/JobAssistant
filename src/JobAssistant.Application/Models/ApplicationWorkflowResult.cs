using JobAssistant.Domain;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Models;

public record ApplicationWorkflowResult(
    Guid RunId,
    DateTimeOffset CreatedAt,
    JobRequirement JobRequirements,
    MatchAssessment MatchAssessment,
    ApplicationDraft Draft,
    ValidationReport Validation,
    string ResumeDocxPath,
    string CoverLetterDocxPath);