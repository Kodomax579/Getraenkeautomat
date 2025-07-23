using Microsoft.AspNetCore.Mvc;
using TicTacToe.DTO;

namespace TicTacToe.Interfaces
{
    public interface ITicTacToeController
    {
        [HttpGet("create")]
        public ActionResult<GameBoardDTO> CreateGame();

        [HttpPut("update")]
        public ActionResult<GameBoardDTO> UpdateGame(int x, int y);


    }
}
