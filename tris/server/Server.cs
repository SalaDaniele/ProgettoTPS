using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;
namespace server;

class Server
{
    private TcpListener listener;
    private List<Player> players;
    private Game game;
    private bool gameStarted;

    public Server()
    {
        listener = new TcpListener(IPAddress.Any, 8888);
        players = new List<Player>();
        game = new Game();
        gameStarted = false;

        listener.Start();
        Console.WriteLine("Server is running. Waiting for players to connect...");

        // Wait for players to connect indefinitely
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Player player = new Player(client, players.Count, game);
            players.Add(player);
            Console.WriteLine($"Player {players.Count} connected.");

            Thread playerThread = new Thread(player.HandleClient);
            playerThread.Start();
            if (players.Count == 2 && !gameStarted) StartGame();
        }
    }

    public void StartGame()
    {
        // Initialize the game and notify the players
        game.Init(players[0], players[1]);
        game.Start();
    }
}

