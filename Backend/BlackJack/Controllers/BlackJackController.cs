using BlackJack.DTO;
using BlackJack.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[ApiController]
[Route("api/[controller]")]
public class BlackJackController : ControllerBase
{
    private readonly BjService _bjService;

    public BlackJackController(BjService bjService)
    {
        _bjService = bjService;
    }

    [HttpPost("NewGame")]
    public IActionResult NewGame([FromQuery] int money)
    {
        Log.Information("Request new game with money:{@money}", money);
        if (money > 0)
        {
            _bjService.InitializeGame(money);
            Log.Information("Game created");
            return Ok(true);
        }
        Log.Error("Money is needed");
        return BadRequest("Money needed");
    }

    [HttpGet("GetSingleCard")]
    public ActionResult<CardModelDTO> GetSingleCard([FromQuery] bool isDealer)
    {
        Log.Information("Request single card");
        var card = _bjService.DrawCard(isDealer);
        if (card == null)
        {
            Log.Error("Stack is empty");
            return BadRequest("Stack is empty");
        }
        Log.Information("Response:{@card}", card);
        return Ok(card);
    }

    [HttpGet("Win")]
    public ActionResult<WinModelDTO> WhoWins()
    {
        Log.Information("Request winner");

        var result = _bjService.WhoWins();

        Log.Information("Response:{@result}", result);
        return Ok(result);
    }
}
