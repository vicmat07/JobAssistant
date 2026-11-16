using JobAssistant.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace JobAssistant.Infrastructure.Persistence;

internal class InfrastructureDbContext(DbContextOptions options) : ApplicationDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfrastructureDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}