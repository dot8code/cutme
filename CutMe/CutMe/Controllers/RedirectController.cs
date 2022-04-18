using CutMe.Communication;
using CutMe.Services;
using Microsoft.AspNetCore.Mvc;

namespace CutMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RedirectController : ControllerBase
{
    private readonly IRedirectService _redirectService;

    public RedirectController(IRedirectService redirectService)
    {
        _redirectService = redirectService;
    }

    [HttpGet("{shortcut}")]
    public async Task<IActionResult> Get([FromRoute] string shortcut)
    {
        var result = await _redirectService.GetFullUrlAsync(shortcut);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(SetRedirectRequest request)
    {
        await _redirectService.SetRedirectUrlAsync(request);

        return NoContent();
    }
}