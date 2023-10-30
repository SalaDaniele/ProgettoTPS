using System;

namespace client
{
    class ClientModel
    {
        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }

        private char[] board = new char[9];

        public ClientModel()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = ' ';
            }
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
                board[i] = gameState[i];
            }
        }

        public char[] GetBoard()
        {
            return board;
        }

        public bool IsGameOver()
        {
            // Check for a win or draw
            return CheckWin('X') || CheckWin('O') || !Array.Exists(board, c => c != ' ');
        }

        private bool CheckWin(char playerSymbol)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((board[i] == playerSymbol && board[i + 3] == playerSymbol && board[i + 6] == playerSymbol) ||
                    (board[i * 3] == playerSymbol && board[i * 3 + 1] == playerSymbol && board[i * 3 + 2] == playerSymbol))
                {
                    return true;
                }
            }

            if ((board[0] == playerSymbol && board[4] == playerSymbol && board[8] == playerSymbol) ||
                (board[2] == playerSymbol && board[4] == playerSymbol && board[6] == playerSymbol))
            {
                return true;
            }

            return false;
        }
    }
}
