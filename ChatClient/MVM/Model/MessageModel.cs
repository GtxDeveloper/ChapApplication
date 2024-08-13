using System.IO;
using System.Windows.Media.Imaging;
using SimpleChatAppWithoutDesign.Core;

namespace SimpleChatAppWithoutDesign.MVM.Model;

public class MessageModel : ObservableObject
{
   
    
    public DateTime SendingTime { get; set; }
    
    public string UID { get; set; }
    
    public string MessageBy { get; set; }
    
    public string Message { get; set; }

    public bool IsReply { get; set; } = false;

    public bool IsContainsImage { get; set; } = false;

    private byte[] _imgBytes;

    public byte[] ImgBytes
    {
        get
        {
            return _imgBytes;
        }
        set
        {
            _imgBytes = value;
            OnPropertyChanged();
        }
    }

    
    public string ImageSource { get; set; }
    public string ReplyMessage { get; set; } 

    public string FullMessage { get; set; }
 }