using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace client
{
    class ClientController
    {
        private TcpClient client;
        private NetworkStream stream;
        private ClientModel model;

        public ClientController(TcpClient client, ClientModel model)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.model = model;
        }

        public void Run()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                // Receive the welcome message
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string welcomeMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // Display the welcome message and prompt the user for their name
                Console.WriteLine(welcomeMessage);
                string playerName = Console.ReadLine();

                // Send the player's name to the server
                byte[] playerNameBytes = Encoding.ASCII.GetBytes(playerName);
                stream.Write(playerNameBytes, 0, playerNameBytes.Length);

                while (true)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string gameState = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    model.UpdateBoard(gameState);

                    ClientView.DisplayGameBoard(model.GetBoard());

                    string move = ClientView.GetUserMove();
                    byte[] moveBytes = Encoding.ASCII.GetBytes(move);
                    stream.Write(moveBytes, 0, moveBytes.Length);

                    if (model.IsGameOver())
                    {
                        // Game over, handle the result here
                        Console.WriteLine("Game over!");
                        // You can handle restart or exit here
                        // For example, call a method to restart the game
                        // RestartGame();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Disconnected from the server: {e.Message}");
                client.Close();
                Environment.Exit(0);
            }
        }

        // Handle restarting the game
        private void RestartGame()
        {
            // Clear the board and reset player turns
            //model.Reset();
            ClientView.ClearConsole();
            ClientView.DisplayGameBoard(model.GetBoard());
        }
    }
}
