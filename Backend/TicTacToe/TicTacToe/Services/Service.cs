using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public class Service : IService
    {

        public GameBoard Board { get; private set; }

        public Service()
        {
            ResetBoard();
        }

        public void ResetBoard()
        {
            Board = new GameBoard
            {
                Board = new char[3][]
                {
                new char[3] { ' ', ' ', ' ' },
                new char[3] { ' ', ' ', ' ' },
                new char[3] { ' ', ' ', ' ' }
                }
            };
        }

        public int UpdateBoard(int x, int y, char player)
        {
            int result = 1;
            bool isloop = true;
            char botSymbol = 'O';
            Random rnd = new Random();

            if (x < 0 || x >= 3)
            {
                return -1;
            }
            if (y < 0 || y >= 3)
            {
                return -2;
            }
            if (Board.Board[x][y] != ' ')
            {
                return 0;
            }

            Board.Board[x][y] = player;
            result = CheckWinner(result);
            while (isloop)
            {
                int rndX = rnd.Next(0, 3);
                int rndY = rnd.Next(0, 3);

                if (!CheckBoardIsFull())
                {
                    isloop = false;
                    result = 4;
                }

                if (Board.Board[rndX][rndY] != player && Board.Board[rndX][rndY] != botSymbol)
                {
                    Board.Board[rndX][rndY] = botSymbol;
                    isloop = false;
                }
            }
            result = CheckWinner(result);
            return result;
        }

        private bool CheckBoardIsFull()
        {
            List<bool> isBoardNotFull = new List<bool>();

            for (int row = 0; row < Board.Board.Length; row++)
            {
                for (int col = 0; col < Board.Board.Length; col++)
                {
                    if (Board.Board[row][col] != ' ')
                    {
                        isBoardNotFull.Add(false);
                    }
                    else
                    {
                        isBoardNotFull.Add(true);
                    }
                }
            }
            if (!isBoardNotFull.Contains(true))
            {
                return false;
            }
            return true;
        }

        private int CheckWinner(int result)
        {
            for (int row = 0; row < 3; row++)
            {
                if (Board.Board[row][0] == Board.Board[row][1] && Board.Board[row][1] == Board.Board[row][2] && Board.Board[row][0] != ' ')
                {
                    return Board.Board[row][0] == 'X' ? 2 : 3;
                }
            }

            for (int col = 0; col < 3; col++)
            {
                if (Board.Board[0][col] == Board.Board[1][col] && Board.Board[1][col] == Board.Board[2][col] && Board.Board[0][col] != ' ')
                {
                    return Board.Board[0][col] == 'X' ? 2 : 3;
                }
            }

            if (Board.Board[0][0] == Board.Board[1][1] && Board.Board[1][1] == Board.Board[2][2] && Board.Board[0][0] != ' ')
            {
                return Board.Board[0][0] == 'X' ? 2 : 3;
            }

            if (Board.Board[0][2] == Board.Board[1][1] && Board.Board[1][1] == Board.Board[2][0] && Board.Board[0][2] != ' ')
            {
                return Board.Board[0][2] == 'X' ? 2 : 3;
            }

            if (result == 4)
            {
                return result;
            }

            return 1;
        }
    }
}
