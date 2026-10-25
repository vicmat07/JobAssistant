namespace JobAssistant.Domain.Models;

public sealed record MatchingInput(
    JobRequirement JobRequirements, 
    CandidateProfile CandidateProfile);