using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Agents;
using JobAssistant.Application.Services;
using JobAssistant.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JobAssistant.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddScoped<IAgent<JobSubmission, JobRequirement>, JobAnalysisAgent>()
            .AddScoped<IAgent<WriterInput, ApplicationDraft>, ApplicationWriterAgent>()
            .AddScoped<IAgent<ReviewInput, ValidationReport>, QualityReviewAgent>()
            .AddScoped<IAgent<MatchingInput, MatchAssessment>, CandidateMatchingAgent>()
            .AddScoped<ApplicationWorkflow>()
            .AddScoped<IApplicationJobService, ApplicationJobService>();
    }
}