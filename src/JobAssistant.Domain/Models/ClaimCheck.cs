namespace JobAssistant.Domain.Models;

public record ClaimCheck(
    string Claim, 
    bool Supported, 
    string Evidence,
    string Explanation);