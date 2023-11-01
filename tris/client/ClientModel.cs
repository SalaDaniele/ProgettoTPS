using System;
using System.Linq;

namespace client
{
    class ClientModel
    {
        public char[] GameBoard { get; private set; }
        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }
        public bool IsGameActive { get; private set; }

        public ClientModel()
        {
            GameBoard = new char[9];
            for (int i = 0; i < 9; i++)
            {
                GameBoard[i] = ' ';
            }
            Player1Name = "Player 1";
            Player2Name = "Player 2";
            IsGameActive = true;
        }
        public char[] GetBoard()
        {
            return GameBoard;
        }

        public void UpdateBoard(string gameState)
        {
            if (gameState.Length != 9)
            {
                Console.WriteLine("Invalid game state received from the server.");
                return;
            }

            for (int i = 0; i < 9; i++)
            {
                GameBoard[i] = gameState[i];
            }
        }

        public void RestartGame()
        {
            for (int i = 0; i < 9; i++)
            {
                GameBoard[i] = ' ';
            }
            IsGameActive = true;
        }

        public void ResetGame()
        {
            for (int i = 0; i < 9; i++)
            {
                GameBoard[i] = ' ';
            }
            IsGameActive = true;
            Player1Name = "Player 1";
            Player2Name = "Player 2";
        }

        public void SetPlayerNames(string player1, string player2)
        {
            Player1Name = player1;
            Player2Name = player2;
        }

        public bool IsValidMove(string move)
        {
            return int.TryParse(move, out int position) && position >= 0 && position < 9 && GameBoard[position] == ' ';
        }

        public bool IsGameOver()
        {
            // Check for a win or draw
            return CheckWin('X') || CheckWin('O') || !GameBoard.Contains(' ');
        }

        private bool CheckWin(char playerSymbol)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((GameBoard[i] == playerSymbol && GameBoard[i + 3] == playerSymbol && GameBoard[i + 6] == playerSymbol) ||
                    (GameBoard[i * 3] == playerSymbol && GameBoard[i * 3 + 1] == playerSymbol && GameBoard[i * 3 + 2] == playerSymbol))
                {
                    return true;
                }
            }

            if ((GameBoard[0] == playerSymbol && GameBoard[4] == playerSymbol && GameBoard[8] == playerSymbol) ||
                (GameBoard[2] == playerSymbol && GameBoard[4] == playerSymbol && GameBoard[6] == playerSymbol))
            {
                return true;
            }

            return false;
        }
    }
}
