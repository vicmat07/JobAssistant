using JobAssistant.Application.Models;
using JobAssistant.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAssistant.Infrastructure.Stores;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ApplicationJob> ApplicationJobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationJob>(e =>
        {
            e.HasKey(x => x.Id)
                .IsClustered();

            e.Property(x => x.Result)
                .HasConversion(new JsonValueConverter<ApplicationWorkflowResult>())
                .HasColumnType("nvarchar(max)");
            
            e.Property(x => x.Submission)
                .HasConversion(new JsonValueConverter<JobSubmission>())
                .HasColumnType("nvarchar(max)");

            e.Property(x => x.Status)
                .HasConversion<string>();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}