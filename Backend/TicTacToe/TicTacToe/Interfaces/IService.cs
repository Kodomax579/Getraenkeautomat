namespace TicTacToe.Interfaces
{
    public interface IService
    {
        public void ResetBoard();

        public int UpdateBoard(int x, int y, char player);
        
    }
}
