using AgentGuard.Core.ChatClient;
using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Services;
using JobAssistant.Infrastructure.Configurations;
using JobAssistant.Infrastructure.Persistence;
using JobAssistant.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OllamaSharp;

namespace JobAssistant.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OllamaOptions>(configuration.GetSection(OllamaOptions.Name));
        services.Configure<DocumentOptions>(configuration.GetSection(DocumentOptions.Name));
        
        services.AddSingleton<IChatClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<OllamaOptions>>().Value;
            var chatClient = new OllamaApiClient(new Uri(options.BaseUrl), options.Model);
            var guardedClient = chatClient.UseAgentGuard(g => g
                .NormalizeInput()
                .BlockPromptInjection()
                .EnforceTopicBoundaryWithLlm(chatClient, "job application", "resume creation", "profile"));
            return guardedClient;
        });
        services.AddScoped<IStructuredLlmClient, AgentFrameworkStructuredLlmClient>();
        services.AddScoped<IDocxGenerator, OpenXmlDocxGenerator>();
        services.AddSingleton<IApplicationJobQueue, ApplicationJobQueue>();

        services.AddDbContext<InfrastructureDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<ApplicationDbContext>(sp => sp.GetRequiredService<InfrastructureDbContext>());
        
        return services;
    }
}