using System.Net.Sockets;
using System.Net;
using System.Text;
using MATTILibrary;
using System.Text.Json;

internal class Program
{
    static string nome1;
    static string nome2;
    static bool turno;
    static char[,] griglia = new char[3, 3]
    {
        { '_','_','_' },
        { '_','_','_' },
        { '_','_','_' }
    };
    static int CheckGrid()
    {
        //da implementare

        //0 partita non finita
        //1 pareggio
        //2 qualcuno ha vinto
        return 0;
    }
    private static void Main(string[] args)
    {
        var ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        var listener = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(ipe);
        listener.Listen();
        Console.WriteLine("Server on, waiting for client");
        var client1 = listener.Accept();
        var data = new byte[1024];
        int received = client1.Receive(data);
        string receivedMessage = Encoding.ASCII.GetString(data, 0, received);
        Message<object> message = JsonSerializer.Deserialize<Message<object>>(receivedMessage);
        if(message.MessageType==MessageType.AskForConnection)
        {
            nome1 = JsonSerializer.Deserialize<AskForConnection>(message.MessageData.ToString()).Nome;
            string answer = JsonSerializer.Serialize(new Message<AnswerForConnection>(MessageType.AnserForConnection, new AnswerForConnection(AnswerType.Ok)));
            byte[] responseBytes = Encoding.ASCII.GetBytes(answer);
            client1.Send(responseBytes);
        }
        else
        {
            //da gestire
        }

        var client2 = listener.Accept();
        data = new byte[1024];
        received = client2.Receive(data);
        receivedMessage = Encoding.ASCII.GetString(data, 0, received);
        message = JsonSerializer.Deserialize<Message<object>>(receivedMessage);
        if (message.MessageType == MessageType.AskForConnection)
        {
            nome2 = JsonSerializer.Deserialize<AskForConnection>(message.MessageData.ToString()).Nome;
            string answer = JsonSerializer.Serialize(new Message<AnswerForConnection>(MessageType.AnserForConnection, new AnswerForConnection(AnswerType.Ok)));
            byte[] responseBytes = Encoding.ASCII.GetBytes(answer);
            client2.Send(responseBytes);
        }
        else
        {
            //da gestire
        }
        if (new Random().Next(0, 2) == 0)
            turno = true;
        else 
            turno = false;
        string gameStarted1 = JsonSerializer.Serialize(new Message<StartGame>(MessageType.StartGame, new StartGame(turno)));
        string gameStarted2 = JsonSerializer.Serialize(new Message<StartGame>(MessageType.StartGame, new StartGame(!turno)));
        client1.Send(Encoding.ASCII.GetBytes(gameStarted1));
        client2.Send(Encoding.ASCII.GetBytes(gameStarted2));
        bool game = true;
        while (game)
        {
            if (turno)
            {
                turno = !turno;
                data = new byte[1024];
                received = client1.Receive(data);
                receivedMessage = Encoding.ASCII.GetString(data, 0, received);
                message = JsonSerializer.Deserialize<Message<object>>(receivedMessage);
                if (message.MessageType == MessageType.Move)
                {
                    Move move = JsonSerializer.Deserialize<Move>(message.MessageData.ToString());
                    griglia[move.Row, move.Column] = 'X';
                    switch (CheckGrid())
                    {
                        //da implementare
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                    }
                    string updateMessage1 = JsonSerializer.Serialize(new Message<Update>(MessageType.Update, new Update(griglia, turno)));
                    string updateMessage2 = JsonSerializer.Serialize(new Message<Update>(MessageType.Update, new Update(griglia, !turno)));
                    client1.Send(Encoding.ASCII.GetBytes(updateMessage1));
                    client2.Send(Encoding.ASCII.GetBytes(updateMessage2));
                }
                else
                {
                    //da gestire
                }

            }
            else
            {
                turno = !turno;
                data = new byte[1024];
                received = client2.Receive(data);
                receivedMessage = Encoding.ASCII.GetString(data, 0, received);
                message = JsonSerializer.Deserialize<Message<object>>(receivedMessage);
                if (message.MessageType == MessageType.Move)
                {
                    Move move = JsonSerializer.Deserialize<Move>(message.MessageData.ToString());
                    griglia[move.Row, move.Column] = 'O';
                    switch (CheckGrid())
                    {
                        //da implementare
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                    }
                    string updateMessage1 = JsonSerializer.Serialize(new Message<Update>(MessageType.Update, new Update(griglia, turno)));
                    string updateMessage2 = JsonSerializer.Serialize(new Message<Update>(MessageType.Update, new Update(griglia, !turno)));
                    client1.Send(Encoding.ASCII.GetBytes(updateMessage1));
                    client2.Send(Encoding.ASCII.GetBytes(updateMessage2));
                }
                else
                {
                    //da gestire
                }
            }
        }
    }
}