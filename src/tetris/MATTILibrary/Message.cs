using System;
using System.Text.Json;
namespace MATTILibrary; // Modular Approach to Trasnmitting and Transforming Information


public class Message<T>
{
    public string MessageType { get; set; } = null!;
    public T MessageData { get; set; }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Message<T> FromJson(string jsonString)
    {
        return JsonSerializer.Deserialize<Message<T>>(jsonString);
    }
}
