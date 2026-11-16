using System.ComponentModel.DataAnnotations;

namespace JobAssistant.API.Models;

public record JobSubmissionRequest(
    [Required(AllowEmptyStrings = false)] string JobDescription,
    Guid CandidateProfileId,
    string? CompanyName = null,
    string? JobTitle = null,
    string? JobUrl = null);