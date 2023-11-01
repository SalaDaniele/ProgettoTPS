using System;
using System.Net.Sockets;
using System.Text;

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

                while (model.IsGameActive)
                {
                    // Receive the game board state from the server
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string gameState = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    model.UpdateBoard(gameState);

                    // Display the current game board
                    ClientView.DisplayGameBoard(model.GetBoard());

                    // Prompt the current player for their move
                    string move = ClientView.GetUserMove();
                    byte[] moveBytes = Encoding.ASCII.GetBytes(move);
                    stream.Write(moveBytes, 0, moveBytes.Length);
                }

                // The game is over; you can handle the result here
                HandleGameResult();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Disconnected from the server: {e.Message}");
                client.Close();
                Environment.Exit(0);
            }
        }

        public void HandleGameResult()
        {
            // Implement handling of the game result, e.g., display who won or if it's a draw
            // You can also ask if the player wants to play again and reset the game accordingly
            Console.WriteLine("Game over! Handle game result logic here.");
        }

        public void HandleRestartGame()
        {
            // Implement handling of game restart, e.g., clearing the game board and starting a new game
            model.RestartGame();
            Console.WriteLine("Game restarted.");
        }

        public void HandleQuitGame()
        {
            // Implement handling of quitting the game
            // You might want to send a message to the server that the player is quitting
            // and then gracefully exit the client application
            Console.WriteLine("Quitting the game.");
            client.Close();
            Environment.Exit(0);
        }
    }
}
