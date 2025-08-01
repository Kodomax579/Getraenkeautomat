using Microsoft.AspNetCore.Mvc;
using Serilog;
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

        public TicTacToeController(Service gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("CreateBoard")]
        public ActionResult<GameBoardDTO> CreateGame()
        {
            Log.Information("Request to create new Game");
            _gameService.ResetBoard();

            var dto = new GameBoardDTO
            {
                Board = _gameService.Board.Board,
                Result = 1
            };
            Log.Information("game created");

            return dto;
        }

        [HttpPut("UpdateGame")]
        public ActionResult<GameBoardDTO> UpdateGame(int x, int y)
        {
            Log.Information("Request to update game");
            int messsage = _gameService.UpdateBoard(x, y, 'X');

            if (messsage == 0)
            {
                Log.Warning("You can not select your own box or that from the bot!");
            }
            if (messsage == -1)
            {
                Log.Error("The variabe X can only contain 0, 1 or 2");
                return BadRequest(new { message = "The variabe X can only contain 0, 1 or 2" });
            }
            if (messsage == -2)
            {
                Log.Error("The variabe Y can only contain 0, 1 or 2");
                return BadRequest(new { message = "The variabe Y can only contain 0, 1 or 2" });
            }

            var dto = new GameBoardDTO
            {
                Board = _gameService.Board.Board,
                Result = messsage
            };
            Log.Information("Game updated");
            return dto;
        }
    }
}
