
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[ApiController]
[Route("api/[controller]")]
public class HeroController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HeroController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("all-heroes"), Authorize]
    public IActionResult GetAllHeroes()
    {
        try
        {
            var sortedHeroes = _context.Heroes
                .OrderByDescending(h => h.CurrentPower)
                .ToList();
            Log.Information("Heroes fetched from database: {@Heroes}", sortedHeroes);
            return Ok(sortedHeroes);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while fetching heroes");
            return StatusCode(500, "Internal server error");
        }
    }
}