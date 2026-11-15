using JobAssistant.Application.Models;
using JobAssistant.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAssistant.Application.Services;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ApplicationJob> ApplicationJobs { get; init; }
    
    public DbSet<CandidateProfile> CandidateProfiles { get; set; }
}