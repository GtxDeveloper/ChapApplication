using ChatServer.Core;

namespace SimpleChatAppWithoutDesign.MVM.Model;

public class UserModel : ObservableObject
{
    public string Id { get; set; }
    
    public string UserName { get; set; }
    
    private bool _isTyping { get; set; } = false;


    public bool IsTyping
    {
        get
        {
            return _isTyping;
        }
        set
        {
            _isTyping = value;
            OnPropertyChanged();
        }
    }
    
    public string IUD { get; set; }
}