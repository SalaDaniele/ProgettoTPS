using server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace server
{
    class Program
    {
        public static List<TcpClient> Clients = new List<TcpClient>();

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8888);
            listener.Start();
            Console.WriteLine("Server is running. Waiting for players to connect...");

            for (int i = 0; i < 2; i++)
            {
                TcpClient client = listener.AcceptTcpClient();
                Clients.Add(client);
                Console.WriteLine($"Player {i + 1} connected.");

                ServerModel model = new ServerModel("Player1", "Player2");
                ServerController controller = new ServerController(client, i, model);

                Thread clientThread = new Thread(controller.Run);
                clientThread.Start();
            }
        }
    }

}