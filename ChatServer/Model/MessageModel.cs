using ChatServer.Core;
namespace SimpleChatAppWithoutDesign.MVM.Model;

public class MessageModel : ObservableObject
{
    public int Id { get; set; }
    
    public DateTime SendingTime { get; set; }
    
    public string UID { get; set; }
    
    public string MessageBy { get; set; }
    
    public string Message { get; set; }

    public bool IsReply { get; set; } = false;

    public bool IsContainsImage { get; set; } = false;

    private byte[] _imageBytes;
    
    
    public byte[] ImageBytes
    {
        get
        {
            return _imageBytes;
        }
        set
        {
            _imageBytes = value;
            OnPropertyChanged();
        }
    }

    public string ReplyMessage { get; set; } 
    
    public int ReplyMessageId { get; set; }

    public string FullMessage { get; set; }
}