using JobAssistant.API.Extensions;
using JobAssistant.API.Models;
using JobAssistant.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace JobAssistant.API.Controllers;

[Route("api/job-applications")]
public class JobApplicationsController(
    IApplicationJobService applicationJobService,
    ICandidateProfileService candidateProfileService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Submit(
        [FromBody] JobSubmissionRequest request, 
        CancellationToken cancellationToken)
    {
        var profile = await candidateProfileService.GetByIdAsync(request.CandidateProfileId, cancellationToken);
        var referenceId = await applicationJobService.CreateJobAsync(request.ToModel(profile), cancellationToken);
        
        return Accepted(new { referenceId });
    }

    [HttpGet("{referenceId:guid}")]
    public async Task<IActionResult> Get(
        Guid referenceId, 
        CancellationToken cancellationToken)
    {
        var jobApplication = await applicationJobService.GetJobAsync(referenceId, cancellationToken);
        
        return Ok(jobApplication);
    }
}