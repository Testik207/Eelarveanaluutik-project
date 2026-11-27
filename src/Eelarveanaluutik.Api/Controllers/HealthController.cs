using Eelarveanaluutik.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Eelarveanaluutik.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult<ApiStatusDto> Get()
    {
        return Ok(new ApiStatusDto
        {
            Name = "Eelarveanaluutik API",
            Status = "ok"
        });
    }
}
