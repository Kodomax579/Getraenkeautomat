using Microsoft.AspNetCore.Mvc;
using TicTacToe.DTO;
using TicTacToe.Interfaces;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicTacToeController : ControllerBase, ITicTacToeController
    {

        private readonly Service _gameService;
        private readonly ILogger<TicTacToeController> _logger;

        public TicTacToeController(Service gameService, ILogger<TicTacToeController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet("CreateBoard")]
        public ActionResult<GameBoardDTO> CreateGame()
        {
            _logger.LogInformation("Request to create new Game");
            _gameService.ResetBoard();

            var dto = new GameBoardDTO
            {
                Board = _gameService.Board.Board,
                Result = 1
            };
            _logger.LogInformation("game created");

            return dto;
        }

        [HttpPut("UpdateGame")]
        public ActionResult<GameBoardDTO> UpdateGame(int x, int y)
        {
            _logger.LogInformation("Request to update game");
            int messsage = _gameService.UpdateBoard(x, y, 'X');

            if (messsage == 0)
            {
                _logger.LogWarning("You can not select your own box or that from the bot!");
            }
            if (messsage == -1)
            {
                _logger.LogError("The variabe X can only contain 0, 1 or 2");
                return BadRequest("The variabe X can only contain 0, 1 or 2");
            }
            if (messsage == -2)
            {
                _logger.LogError("The variabe Y can only contain 0, 1 or 2");
                return BadRequest("The variabe Y can only contain 0, 1 or 2");
            }

            var dto = new GameBoardDTO
            {
                Board = _gameService.Board.Board,
                Result = messsage
            };
            _logger.LogInformation("Game updated");
            return dto;
        }
    }
}
