using System.Net.Sockets;
using System.Net;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

        var listener = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        listener.Bind(ipe);

        listener.Listen();

        Console.WriteLine("Server on, waiting for client");
        while (true)

        {

            var socket = listener.Accept();

            Console.WriteLine("Client connected");

            var data = new byte[1024];

            while (true)

            {

                int received = socket.Receive(data);

                string receivedMessage = Encoding.ASCII.GetString(data, 0, received);

                Console.WriteLine("Message from client: " + receivedMessage);

                // Sending response to client

                string responseMessage = "Message received: " + receivedMessage;

                byte[] responseBytes = Encoding.ASCII.GetBytes(responseMessage);

                socket.Send(responseBytes);

            }

            socket.Shutdown(SocketShutdown.Both);

            socket.Close();

        }
    }
}