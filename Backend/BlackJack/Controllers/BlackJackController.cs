using BlackJack.DTO;
using BlackJack.Services;
using Microsoft.AspNetCore.Mvc;

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
        if (money > 0)
        {
            _bjService.InitializeGame(money);
            return Ok(true);
        }
        return BadRequest("Money needed");
    }

    [HttpGet("GetSingleCard")]
    public ActionResult<CardModelDTO> GetSingleCard([FromQuery] bool isDealer)
    {
        var card = _bjService.DrawCard(isDealer);
        if (card == null)
            return BadRequest("Keine Karten mehr im Deck.");

        return Ok(card);
    }

    [HttpGet("Win")]
    public ActionResult<WinModelDTO> WhoWins()
    {
        var result = _bjService.WhoWins();
        return Ok(result);
    }
}
