using JobAssistant.Application.Services;
using JobAssistant.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobAssistant.API.Controllers;

[Route("workflows")]
public class ApplicationWorkflowsController(ApplicationWorkflow workflow) : Controller
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
        
        var response = await workflow.RunAsync(jobSubmission, cancellationToken);
        return Ok(response);
    } 
}