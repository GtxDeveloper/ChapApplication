namespace SimpleChatAppWithoutDesign.MVM.Model;

public class MessageModel
{
    public int Id { get; set; }
    
    public DateTime SendingTime { get; set; }
    
    public string UID { get; set; }
    
    public string MessageBy { get; set; }
    
    public string Message { get; set; }

    public bool IsReply { get; set; } = false;

    public string ReplyMessage { get; set; } 
    
    public int ReplyMessageId { get; set; }

    public string FullMessage { get; set; }
 }