//public class ClientController
//{
//    private ClientModel model;
//    private Client client;

//    public ClientController(ClientModel model, Client client)
//    {
//        this.model = model;
//        this.client = client;
//    }

//    public void StartGame()
//    {
//        // Establish a connection with the server
//        // Receive initialization data (e.g., game board and player symbol) from the server

//        while (!GameIsOver())
//        {
//            // Display the current game board to the player
//            client.DisplayGame(model.GetGameBoard());

//            if (model.IsPlayerTurn())
//            {
//                // Wait for the player's move and send it to the server
//                var playerMove = client.GetPlayerMove();
//                client.SendMove(playerMove);
//            }
//            else
//            {
//                // Wait for updates from the server
//                var serverUpdates = client.ReceiveUpdates();
//                model.UpdateGameBoard(serverUpdates);
//            }
//        }
//    }

//    public bool GameIsOver()
//    {
//        return false;
//        // Determine if the game is over based on server updates
//    }
//}
