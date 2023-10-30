using System;
using System.Linq;

namespace server
{
    class ServerModel
    {
        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }

        private char[] board = new char[9];
        private int currentPlayer = 0;

        public ServerModel(string player1Name, string player2Name)
        {
            Player1Name = player1Name;
            Player2Name = player2Name;

            for (int i = 0; i < 9; i++)
            {
                board[i] = ' ';
            }
        }

        public bool MakeMove(int position, int playerId)
        {
            if (position < 0 || position >= 9 || board[position] != ' ')
                return false;

            board[position] = (playerId == 0) ? 'X' : 'O';
            currentPlayer = 1 - currentPlayer;
            return true;
        }

        public char[] GetBoard()
        {
            return board;
        }

        public bool IsGameOver()
        {
            // Check for a win or draw
            return CheckWin('X') || CheckWin('O') || !board.Contains(' ');
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
