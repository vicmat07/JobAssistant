using JobAssistant.API.Extensions;
using JobAssistant.API.Models;
using JobAssistant.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace JobAssistant.API.Controllers;

[Route("api/profiles")]
public class CandidateProfilesController(ICandidateProfileService profileService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateProfile(
        [FromBody] CreateProfileRequest request, 
        CancellationToken cancellationToken = default)
    {
        var result = await profileService.CreateProfileAsync(request.ToCandidateProfile(), cancellationToken);
        
        return CreatedAtAction(actionName: nameof(GetProfile), routeValues: new { profileId = result }, value: result);
    }

    [HttpGet("{profileId:guid}")]
    public async Task<IActionResult> GetProfile(
        [FromRoute] Guid profileId, 
        CancellationToken cancellationToken = default)
    {
        var candidateProfile = await profileService.GetByIdAsync(profileId, cancellationToken);
        
        return Ok(candidateProfile);
    }
}