using JobAssistant.Domain;
using JobAssistant.Domain.Models;

namespace JobAssistant.Application.Abstractions;

public interface IDocxGenerator
{
    Task<string> GenerateResumeAsync(
        ApplicationDraft draft, 
        Guid runId, 
        CancellationToken cancellationToken);
    
    Task<string> GenerateCoverLetterAsync(
        ApplicationDraft draft, 
        Guid runId, 
        CancellationToken cancellationToken);
}