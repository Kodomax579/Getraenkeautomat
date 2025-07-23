using Microsoft.AspNetCore.Mvc;
using TicTacToe.DTO;
using TicTacToe.Interfaces;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : ControllerBase, ITicTacToeController
    {

        private readonly Service _gameService;
        private readonly ILogger<TicTacToeController> _logger;

        public TicTacToeController(Service gameService, ILogger<TicTacToeController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet("create")]
        public ActionResult<GameBoardDTO> CreateGame()
        {
            _gameService.ResetBoard();

            var dto = new GameBoardDTO
            {
                Board = _gameService.Board.Board,
                Result = 1
            };

            return dto;
        }

        [HttpPut("update")]
        public ActionResult<GameBoardDTO> UpdateGame(int x, int y)
        {
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

            return dto;
        }
    }
}
