using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace server
{
    class ServerController
    {
        private TcpClient client;
        private NetworkStream stream;
        private ServerModel model;
        private int playerId;

        public ServerController(TcpClient client, int playerId, ServerModel model)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.model = model;
            this.playerId = playerId;
        }

        public void Run()
        {
            try
            {
                // Send a welcome message to request the client's name
                string welcomeMessage = "Welcome to the Tic-Tac-Toe game! Please enter your name: ";
                byte[] welcomeMessageBytes = Encoding.ASCII.GetBytes(welcomeMessage);
                stream.Write(welcomeMessageBytes, 0, welcomeMessageBytes.Length);

                byte[] buffer = new byte[1024];
                int bytesRead;

                // Receive the player's name
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string playerName = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // Acknowledge the player's name
                Console.WriteLine($"Player {playerId + 1} connected: {playerName}");
                string confirmationMessage = $"Welcome, {playerName}!";
                byte[] confirmationBytes = Encoding.ASCII.GetBytes(confirmationMessage);
                stream.Write(confirmationBytes, 0, confirmationBytes.Length);

                while (true)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string move = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    if (!string.IsNullOrEmpty(move))
                    {
                        if (int.TryParse(move, out int position))
                        {
                            if (model.MakeMove(position, playerId))
                            {
                                ServerView.DisplayGameBoard(model.GetBoard());

                                if (model.IsGameOver())
                                {
                                    // Game over, handle the result here
                                    Console.WriteLine("Game over!");
                                    // You can handle restart or exit here
                                    // For example, call a method to restart the game
                                    // RestartGame();
                                }
                                else
                                {
                                    BroadcastGameState();
                                }
                            }
                            else
                            {
                                ServerView.DisplayMessage("Invalid move. Try again.");
                            }
                        }
                        else
                        {
                            ServerView.DisplayMessage("Invalid input. Please send a valid move (0-8).");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ServerView.DisplayMessage($"Player {playerId + 1} disconnected: {e.Message}");
                client.Close();
            }
        }

        private void BroadcastGameState()
        {
            foreach (var client in Program.Clients)
            {
                NetworkStream clientStream = client.GetStream();
                string gameState = new string(model.GetBoard());
                byte[] data = Encoding.ASCII.GetBytes(gameState);
                clientStream.Write(data, 0, data.Length);
            }
        }

        // Handle restarting the game
        private void RestartGame()
        {
            // Clear the board and reset player turns
            //model.Reset();
            ServerView.ClearConsole();
            ServerView.DisplayGameBoard(model.GetBoard());
        }
    }
}
