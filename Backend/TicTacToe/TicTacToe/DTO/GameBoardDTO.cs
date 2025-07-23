namespace TicTacToe.DTO
{
    public class GameBoardDTO
    {
        public char[][] Board { get; set; } = null!;
        public int Result { get; set; }
    }
}
