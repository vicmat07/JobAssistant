namespace JobAssistant.Domain.Models;

public record ReviewInput(
    JobSubmission Submission, 
    JobRequirement JobRequirements, 
    MatchAssessment MatchAssessment, 
    ApplicationDraft Draft);