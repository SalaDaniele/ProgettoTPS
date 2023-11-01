using server;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Player
{
    private TcpClient client;
    private NetworkStream stream;
    private int playerId;
    private string playerName;
    private Game game;
    public int PlayerId { get; private set; }

    public Player(TcpClient client, int playerId, Game game)
    {

        this.client = client;
        this.stream = client.GetStream();
        this.playerId = playerId;
        this.game = game; // Store the game instance
    }

    public void SendGameStartMessage(string player1Name, string player2Name)
    {
        try
        {
            string gameStartMessage = $"Game starts! You are {player1Name}. Opponent is {player2Name}.\n";
            byte[] messageBytes = Encoding.ASCII.GetBytes(gameStartMessage);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending game start message to player {playerId}: {e.Message}");
        }
    }

    public void SendGameUpdate(string boardState)
    {
        try
        {
            // Construct a message with the current game board state
            string updateMessage = $"Current game board:\n{boardState}\n";
            byte[] messageBytes = Encoding.ASCII.GetBytes(updateMessage);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending game update to player {playerId}: {e.Message}");
        }
    }

    public void SendInvalidMoveMessage()
    {
        try
        {
            string invalidMoveMessage = "Invalid move. Try again.\n";
            byte[] messageBytes = Encoding.ASCII.GetBytes(invalidMoveMessage);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending invalid move message to player {playerId}: {e.Message}");
        }
    }

    public void SendInvalidInputMessage()
    {
        try
        {
            string invalidInputMessage = "Invalid input. Please send a valid move (0-8).\n";
            byte[] messageBytes = Encoding.ASCII.GetBytes(invalidInputMessage);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending invalid input message to player {playerId}: {e.Message}");
        }
    }



    public void SendGameResult(string resultMessage)
    {
        try
        {
            // Send the game result message (e.g., "Player X wins!" or "It's a draw.")
            byte[] messageBytes = Encoding.ASCII.GetBytes(resultMessage);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending game result to player {playerId}: {e.Message}");
        }
    }

    public string ReceivePlayerMove()
    {
        try
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            stream.WriteAsync(buffer);
            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error receiving player move from player {playerId}: {e.Message}");
            return null;
        }
    }

    public void SendOpponentMove(string move)
    {
        try
        {
            string opponentMoveMessage = $"Your opponent's move: {move}\n";
            byte[] messageBytes = Encoding.ASCII.GetBytes(opponentMoveMessage);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending opponent's move to player {playerId}: {e.Message}");
        }
    }


    public void HandleClient()
    {
        try
        {
            string welcomeMessage = "Welcome to the Tic-Tac-Toe game! Please enter your name: ";
            byte[] welcomeMessageBytes = Encoding.ASCII.GetBytes(welcomeMessage);
            stream.Write(welcomeMessageBytes, 0, welcomeMessageBytes.Length);

            byte[] buffer = new byte[1024];
            int bytesRead;

            bytesRead = stream.Read(buffer, 0, buffer.Length);
            playerName = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Player {playerId + 1} connected: {playerName}");
            string confirmationMessage = $"Welcome, {playerName}!";
            byte[] confirmationBytes = Encoding.ASCII.GetBytes(confirmationMessage);
            stream.Write(confirmationBytes, 0, confirmationBytes.Length);

            while (true)
            {
                // Receive the move from the current player
                //Console.WriteLine("Move Received");
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string playerMove = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Player Move");
                Console.WriteLine(playerMove);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                // Print the received move to the server console
                Console.WriteLine($"Player {playerId + 1} move: {playerMove}");

                // Validate the move within the Game class
                bool validMove = game.IsValidMove(playerMove);
                if (validMove)
                {
                    Console.WriteLine("Valid Move");
                    int movePosition = int.Parse(playerMove);

                    // Send the move to the other player (you will need to implement this)
                    SendOpponentMove(playerMove);

                    // Check for a win or draw and send the result if the game is over
                    if (game.IsGameOver())
                    {
                        string resultMessage = game.GetGameResultMessage();
                        SendGameResult(resultMessage);
                        break;
                    }
                }
                else
                {
                    SendInvalidMoveMessage();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Player {playerId + 1} disconnected: {e.Message}");
            client.Close();
        }
    }


}
