using System.Net.Sockets;

namespace client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("localhost", 8888);

                ClientModel model = new ClientModel();
                ClientController controller = new ClientController(client, model);

                Thread clientThread = new Thread(controller.Run);
                clientThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to connect to the server: {e.Message}");
            }
        }
    }
}