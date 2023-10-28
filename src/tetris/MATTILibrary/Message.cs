using System;
using System.Text.Json;
namespace MATTILibrary; // Modular Approach to Trasnmitting and Transforming Information


public class Message
{
    public string MessageType { get; set; }
    public string MessageData { get; set; }

    public Message(string messageType, string messageData)
    {
        MessageType = messageType;
        MessageData = messageData;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Message FromJson(string jsonString)
    {
        return JsonSerializer.Deserialize<Message>(jsonString);
    }
}
