using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace IQueueAPI.Controllers;

public class BaseApiController : ControllerBase
{
    protected Guid UserId => Guid.Parse(FindClaim(ClaimTypes.NameIdentifier)!);
    
        
    private string? FindClaim(string claimName)
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        var claim = claimsIdentity?.FindFirst(claimName);

        return claim?.Value;
    }
}