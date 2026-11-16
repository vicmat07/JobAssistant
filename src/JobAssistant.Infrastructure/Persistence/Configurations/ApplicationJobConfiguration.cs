using JobAssistant.Application.Models;
using JobAssistant.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobAssistant.Infrastructure.Persistence.Configurations;

internal class ApplicationJobConfiguration : IEntityTypeConfiguration<ApplicationJob>
{
    public void Configure(EntityTypeBuilder<ApplicationJob> builder)
    {
        builder.HasKey(x => x.Id)
            .IsClustered();

        builder.Property(x => x.Result)
            .HasConversion(new JsonValueConverter<ApplicationWorkflowResult>())
            .HasColumnType("nvarchar(max)");
            
        builder.Property(x => x.Submission)
            .HasConversion(new JsonValueConverter<JobSubmission>())
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.Status)
            .HasConversion<string>();
    }
}