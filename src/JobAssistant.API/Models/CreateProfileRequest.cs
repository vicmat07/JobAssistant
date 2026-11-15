using System.ComponentModel.DataAnnotations;
using JobAssistant.Domain.Models;

namespace JobAssistant.API.Models;

public record CreateProfileRequest(
    [Required] string Name,
    string Summary,
    ICollection<ExperienceItem> ExperienceItems,
    ICollection<LanguageItem> Languages,
    ICollection<string> Skills);