namespace JobAssistant.Domain.Models;

public record WriterInput(
    JobRequirement JobRequirements, 
    MatchAssessment MatchAssessment, 
    CandidateProfile CandidateProfile);