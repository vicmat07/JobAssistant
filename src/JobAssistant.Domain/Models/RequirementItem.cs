namespace JobAssistant.Domain.Models;

public record RequirementItem(
    string Name, 
    string Importance, 
    string EvidenceFromDescription);