using JobAssistant.Application.Abstractions;
using JobAssistant.Application.Services;
using JobAssistant.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAssistant.API.Controllers;

[Route("workflows")]
public class ApplicationWorkflowsController(IApplicationJobService applicationJobService) : Controller
{
    [HttpPost("job-application/run")]
    public async Task<IActionResult> Submit(
        [FromBody] JobSubmission jobSubmission, 
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(jobSubmission.JobDescription))
        {
            return BadRequest();
        }
        
        var referenceId = await applicationJobService.CreateJobAsync(jobSubmission, cancellationToken);
        
        return Accepted(referenceId);
    } 
}