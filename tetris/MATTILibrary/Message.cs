using System;
using System.Text.Json;
namespace MATTILibrary; // Modular Approach to Trasnmitting and Transforming Information

public enum MessageType
{
    Move,
    AskForConnection,
    FinishGame,
    StartGame,
    AnserForConnection,
    GameInterrupted,
    Update
}
public enum AnswerType
{
    Ok,
    ServerFull
}

public class Message<T>
{
    public Message(MessageType messageType, T messageData)
    {
        MessageType = messageType;
        MessageData = messageData;
    }

    public MessageType MessageType { get; set; }
    public T MessageData { get; set; }

    //public string ToJson()
    //{
    //    return JsonSerializer.Serialize(this);
    //}

    //public static Message<T> FromJson(string jsonString)
    //{
    //    return JsonSerializer.Deserialize<Message<T>>(jsonString);
    //}
}

public class Move
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
}

public class AskForConnection
{
    public AskForConnection(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; set; } = null!;
}

public class FinishGame
{
    public bool Win { get; set; }
}
public class StartGame
{
    public StartGame(bool turno)
    {
        Turno = turno;
    }

    public bool Turno { get; set; }
}

public class AnswerForConnection
{
    public AnswerForConnection(AnswerType answerType)
    {
        AnswerType = answerType;
    }

    public AnswerType AnswerType { get; set; }
}

public class GameInterrupted
{

}

public class Update
{
    public Update(char[,] griglia, bool turno)
    {
        Griglia = griglia;
        Turno = turno;
    }
    public char[,] Griglia { get; set; }
    public bool Turno { get; set; }
}