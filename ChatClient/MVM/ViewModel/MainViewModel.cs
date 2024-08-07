using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;
using SimpleChatAppWithoutDesign.Core;
using SimpleChatAppWithoutDesign.MVM.Model;
using SimpleChatAppWithoutDesign.Net;

namespace SimpleChatAppWithoutDesign.MVM.ViewModel;

public class MainViewModel
{
    public ObservableCollection<UserModel> Users { get; set; }
    public ObservableCollection<MessageModel> RecievedMessages { get; set; }
    
    public MessageModel SelectedReplyMessage { get; set; }
    public RelayCommand ConnectToServerCommand { get; set; }
    public RelayCommand SendMessageCommand { get; set; }
    
    public UserModel SelectedContact { get; set; }

    public string UsernameString { get; set; }
    public string SendingMessageString { get; set; }
    
    public MessageModel SendingMessage { get; set; }
    private Server _server;

    public UserModel MainUser { get; set; }
    
    public MainViewModel()
    {
        Users = new ObservableCollection<UserModel>();
        RecievedMessages = new ObservableCollection<MessageModel>();
        _server = new Server();
        _server.connectedEvent += UserConnected;
        _server.userDisconnectEvent += RemoveUser;
        _server.msgRecievedEvent += MsgRecievedEvent;
        ConnectToServerCommand = new RelayCommand(o => _server.ConnecToServer(UsernameString), o => !string.IsNullOrEmpty(UsernameString));

        SendMessageCommand =
            new RelayCommand(o =>
            {
                SendingMessage = new MessageModel()
                {
                    Message = SendingMessageString,
                    UID = new Guid().ToString()
                };
                if (SelectedReplyMessage != null)
                {
                    SendingMessage.IsReply = true;
                    SendingMessage.ReplyMessage = $"Reply to => {SelectedReplyMessage.FullMessage}";
                }
                
                _server.SendMessageToServer(SendingMessage);
            }, o => !string.IsNullOrEmpty(SendingMessageString));
    }

    private void MsgRecievedEvent()
    {
        var msg =   _server.PacketReader.ReadMessage();
        Application.Current.Dispatcher.Invoke(() => RecievedMessages.Add(msg));
    }

    private void RemoveUser()
    {
        var uid = _server.PacketReader.ReadMessage();
        var user = Users.Where(x => x.IUD == uid.UID).FirstOrDefault();
        Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
    }

    private void UserConnected()
    {
        var user = _server.PacketReader.ReadUser();
        Console.WriteLine(user.IUD + $" {user.UserName} IUD");
        if (!Users.Any(x => x.IUD == user.IUD))
        {
            Application.Current.Dispatcher.Invoke(() => Users.Add(user));
        }
    }
}