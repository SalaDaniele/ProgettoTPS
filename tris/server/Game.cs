using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace server;

class Game
{
    private Player player1;
    private Player player2;
    private char[] board;
    private int currentTurn;

    public void Init(Player player1, Player player2)
    {
        this.player1 = player1;
        this.player2 = player2;
        this.board = new char[9];
        this.currentTurn = 0;

        // Notify players about the start of the game
        player1.SendGameStartMessage("Player 1", "Player 2");
        player2.SendGameStartMessage("Player 1", "Player 2");
    }

    public void Start()
    {
        player1.SendGameUpdate("move received");
        while (!IsGameOver())
        {
            // Determine the current player based on the game state
            Player currentPlayer = (currentTurn % 2 == 0) ? player1 : player2;

            // Display the current game board to the current player
            string boardState = new string(board);
            currentPlayer.SendGameUpdate(boardState);

            // Receive the move from the current player
            string playerMove = currentPlayer.ReceivePlayerMove();

            Console.WriteLine("move received");

            // Validate the move and update the game board
            if (IsValidMove(playerMove))
            {
                int movePosition = int.Parse(playerMove);
                if (MakeMove(movePosition, currentTurn % 2))
                {
                    // Check for a win or draw
                    if (IsGameOver())
                    {
                        string resultMessage = GetGameResultMessage();
                        player1.SendGameResult(resultMessage);
                        player2.SendGameResult(resultMessage);
                        break;
                    }

                    // Notify both players about the updated game board
                    string updatedBoardState = new string(board);
                    player1.SendGameUpdate(updatedBoardState);
                    player2.SendGameUpdate(updatedBoardState);

                    // Switch to the other player's turn
                    currentTurn++;
                }
                else
                {
                    currentPlayer.SendInvalidMoveMessage();
                }
            }
            else
            {
                currentPlayer.SendInvalidInputMessage();
            }
        }
    }

    public bool IsValidMove(string move)
    {
        if (int.TryParse(move, out int position) && position >= 0 && position < 9 && board[position] == ' ')
            return true;
        return false;
    }

    public bool MakeMove(int position, int player)
    {
        if (board[position] == ' ')
        {
            board[position] = (player == 0) ? 'X' : 'O';
            return true;
        }
        return false;
    }

    public bool IsGameOver()
    {
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

    public string GetGameResultMessage()
    {
        if (CheckWin('X'))
        {
            return "Player 1 (X) wins!";
        }
        else if (CheckWin('O'))
        {
            return "Player 2 (O) wins!";
        }
        else
        {
            return "It's a draw!";
        }
    }
}
