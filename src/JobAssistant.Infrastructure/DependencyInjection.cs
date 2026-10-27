using JobAssistant.Application.Abstractions;
using JobAssistant.Infrastructure.Configurations;
using JobAssistant.Infrastructure.Services;
using JobAssistant.Infrastructure.Stores;
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
            return new OllamaApiClient(new Uri(options.BaseUrl), options.Model);
        });
        services.AddScoped<IStructuredLlmClient, AgentFrameworkStructuredLlmClient>();
        services.AddScoped<IDocxGenerator, OpenXmlDocxGenerator>();
        services.AddScoped<IApplicationJobStore, ApplicationJobStore>();
        services.AddSingleton<IApplicationJobQueue, ApplicationJobQueue>();

        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        return services;
    }
}