using JobAssistant.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobAssistant.Infrastructure.Persistence.Configurations;

internal class CandidateProfileConfiguration : IEntityTypeConfiguration<CandidateProfile>
{
    public void Configure(EntityTypeBuilder<CandidateProfile> builder)
    {
        builder.HasKey(x => x.Id)
            .IsClustered();
            
        builder.Property(x => x.ExperienceItems)
            .HasConversion(new JsonValueConverter<ICollection<ExperienceItem>>())
            .HasColumnType("nvarchar(max)");
            
        builder.Property(x => x.Languages)
            .HasConversion(new JsonValueConverter<ICollection<LanguageItem>>())
            .HasColumnType("nvarchar(max)");
            
        builder.Property(x => x.Skills)
            .HasConversion(new JsonValueConverter<ICollection<string>>())
            .HasColumnType("nvarchar(max)");
    }
}