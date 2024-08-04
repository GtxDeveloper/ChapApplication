using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using SimpleChatAppWithoutDesign.Core;
using SimpleChatAppWithoutDesign.MVM.Model;
using SimpleChatAppWithoutDesign.Net;

namespace SimpleChatAppWithoutDesign.MVM.ViewModel;

public class MainViewModel
{
    public ObservableCollection<UserModel> Users { get; set; }
    public ObservableCollection<string> RecievedMessages { get; set; }
    public RelayCommand ConnectToServerCommand { get; set; }
    public RelayCommand SendMessageCommand { get; set; }
    
    public UserModel SelectedContact { get; set; }

    public string SendingMessage { get; set; }
    private Server _server;

    public string Username { get; set; }
    
    public MainViewModel()
    {
        Users = new ObservableCollection<UserModel>();
        RecievedMessages = new ObservableCollection<string>();
        _server = new Server();
        _server.connectedEvent += UserConnected;
        _server.userDisconnectEvent += RemoveUser;
        _server.msgRecievedEvent += MsgRecievedEvent;
        ConnectToServerCommand = new RelayCommand(o => _server.ConnecToServer(Username), o => !string.IsNullOrEmpty(Username));

        SendMessageCommand =
            new RelayCommand(o => _server.SendMessageToServer(SendingMessage), o => !string.IsNullOrEmpty(SendingMessage));
    }

    private void MsgRecievedEvent()
    {
        var msg = _server.PacketReader.ReadMessage();
        Application.Current.Dispatcher.Invoke(() => RecievedMessages.Add(msg));
    }

    private void RemoveUser()
    {
        var uid = _server.PacketReader.ReadMessage();
        var user = Users.Where(x => x.IUD == uid).FirstOrDefault();
        Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
    }

    private void UserConnected()
    {
        var user = new UserModel()
        {
            UserName = _server.PacketReader.ReadMessage(),
            IUD = _server.PacketReader.ReadMessage()
        };

        if (!Users.Any(x => x.IUD == user.IUD))
        {
            Application.Current.Dispatcher.Invoke(() => Users.Add(user));
        }
    }
}