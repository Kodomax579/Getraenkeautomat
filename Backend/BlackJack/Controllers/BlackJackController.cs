using BlackJack.DTO;
using BlackJack.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BlackJackController : ControllerBase
{
    private readonly BjService _bjService;
    private ILogger<BlackJackController> _logger;

    public BlackJackController(BjService bjService, ILogger<BlackJackController> logger)
    {
        _bjService = bjService;
        _logger = logger;
    }

    [HttpPost("NewGame")]
    public IActionResult NewGame([FromQuery] int money)
    {
        _logger.LogInformation("Request new game with money:{money}", money);
        if (money > 0)
        {
            _bjService.InitializeGame(money);
            _logger.LogInformation("Game created");
            return Ok(true);
        }
        _logger.LogError("Money is needed");
        return BadRequest("Money needed");
    }

    [HttpGet("GetSingleCard")]
    public ActionResult<CardModelDTO> GetSingleCard([FromQuery] bool isDealer)
    {
        _logger.LogInformation("Request single card");
        var card = _bjService.DrawCard(isDealer);
        if (card == null)
        {
            _logger.LogError("Stack is empty");
            return BadRequest("Stack is empty");
        }
        _logger.LogInformation("Response:{card}", card);
        return Ok(card);
    }

    [HttpGet("Win")]
    public ActionResult<WinModelDTO> WhoWins()
    {
        _logger.LogInformation("Request winner");

        var result = _bjService.WhoWins();

        _logger.LogInformation("Response:{result}", result);
        return Ok(result);
    }
}
