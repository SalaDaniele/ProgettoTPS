using MATTILibrary;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        var ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        var socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        socket.Connect(ipe);

        Console.WriteLine("Connected");

        var writer = new StreamWriter(new NetworkStream(socket));
        writer.AutoFlush = true;

        var reader = new StreamReader(new NetworkStream(socket));


        while (true)
        {
            //Console.WriteLine("Enter message to send:");
            //var message = Console.ReadLine();
            //writer.WriteLine(message);

            Message<AskForConnection> clientMessage = new Message<AskForConnection>(MessageType.AskForConnection,new AskForConnection("Daniele"));
            writer.WriteLine(JsonSerializer.Serialize(clientMessage));

            var responseMessage = reader.ReadLine();
            Console.WriteLine("Message from server: " + responseMessage);
            Console.ReadLine();
        }

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}