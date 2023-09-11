using Newtonsoft.Json;

namespace Store.Models;

public enum TypeMessage
{
    Info,
    Error
}

public class MessageModel
{
    public TypeMessage Type { get; set; }
    public string Text { get; set; }

    public MessageModel(string message, TypeMessage type = TypeMessage.Info)
    {
        this.Type = type;
        this.Text = message;
    }

    public static string Serializer(string message, TypeMessage type = TypeMessage.Info)
    {
        var messageModel = new MessageModel(message, type);
        return JsonConvert.SerializeObject(messageModel);
    }

    public static MessageModel Desserializer(string messageString)
    {
        return JsonConvert.DeserializeObject<MessageModel>(messageString)!;
    }
}   