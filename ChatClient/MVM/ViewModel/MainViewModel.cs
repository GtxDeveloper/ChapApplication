using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using SimpleChatAppWithoutDesign.Core;
using SimpleChatAppWithoutDesign.MVM.Model;
using SimpleChatAppWithoutDesign.Net;
using SimpleChatAppWithoutDesign;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Controls;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using Image = SixLabors.ImageSharp.Image;

namespace SimpleChatAppWithoutDesign.MVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public ObservableCollection<UserModel> Users { get; set; }
    public ObservableCollection<MessageModel> RecievedMessages { get; set; }

    private MessageModel _selectedReplyMessage { get; set; }

    public MessageModel SelectedReplyMessage
    {
        get { return _selectedReplyMessage; }
        set
        {
            _selectedReplyMessage = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ConnectToServerCommand { get; set; }
    public RelayCommand SendMessageCommand { get; set; }

    public RelayCommand SetImageCommand { get; set; }
    
    public RelayCommand DeleteImageCommand { get; set; }

    public UserModel SelectedContact { get; set; }

    private bool _isImageActive = false;

    public bool IsImageActive
    {
        get
        {
            return _isImageActive;
        }
        set
        {
            _isImageActive = value;
            OnPropertyChanged();
        }
    }

    public string UsernameString { get; set; }

    private string _sendingMessageString;

    private string _imgPath;

    public string SendingMessageString
    {
        get { return _sendingMessageString; }
        set
        {
            _sendingMessageString = value;
            OnPropertyChanged();
        }
    }

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
        _server.userTypingEvent += UserTyping;
        MainUser = new UserModel();
        ConnectToServerCommand = new RelayCommand(o =>
        {
            MainUser = new UserModel()
            {
                UserName = UsernameString
            };
            _server.ConnecToServer(MainUser);
            MonitorIsTyping();
        }, o => !string.IsNullOrEmpty(UsernameString));
        SendMessageCommand =
            new RelayCommand(o =>
            {
                SendingMessage = new MessageModel()
                {
                    Message = SendingMessageString,
                    MessageBy = MainUser.UserName
                };
                SendingMessage.UID = new Guid().ToString();
                SendingMessage.SendingTime = DateTime.Now;
                SendingMessage.FullMessage = $"[{SendingMessage.SendingTime}] : [{SendingMessage.MessageBy}] : {SendingMessage.Message}";
                if (!string.IsNullOrEmpty(_imgPath))
                {
                    SendingMessage.IsContainsImage = true;
                    var bytes = File.ReadAllBytes(_imgPath);

                    SendingMessage.ImgBytes = bytes;
                }
                SendingMessageString = "";
                if (SelectedReplyMessage != null)
                {
                    SendingMessage.IsReply = true;
                    SendingMessage.ReplyMessage = $"Reply to => {SelectedReplyMessage.FullMessage}";
                }

                _server.SendMessageToServer(SendingMessage);
                IsImageActive = false;

            }, o => !string.IsNullOrEmpty(SendingMessageString));

        SetImageCommand =
            new RelayCommand(o =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                openFileDialog.ShowDialog();
                if (!string.IsNullOrEmpty(openFileDialog.FileName))
                {
                    // Получаем путь к выбранному файлу
                    string filePath = openFileDialog.FileName;

                    // Например, выводим путь к файлу в консоль
                    Console.WriteLine("Выбранный файл: " + filePath);

                    _imgPath = filePath;
                    IsImageActive = true;

                }
            }, o => Users.Count > 0);

        DeleteImageCommand =
            new RelayCommand(o =>
            {
                _imgPath = string.Empty;
                IsImageActive = false;
            }, o => IsImageActive);
    }

    private void UserTyping()
    {
        var typingUser = _server.PacketReader.ReadUser();
        var usr = Users.FirstOrDefault(x => x.IUD == typingUser.IUD);
        usr.IsTyping = typingUser.IsTyping;
    }

    private void MsgRecievedEvent()
    {
        var msg = _server.PacketReader.ReadMessage();
        if (msg.IsContainsImage)
        {
            var imgsDirectory = Path.Combine(Environment.CurrentDirectory, "Image Messages");
            if (!Directory.Exists(imgsDirectory)) Directory.CreateDirectory(imgsDirectory);

            var imgsCount = Directory.EnumerateFiles(imgsDirectory).Count() + 1;
            var imgPath = Path.Combine(imgsDirectory, $"IMG_{imgsCount}.png");

            using (Image image = Image.Load(msg.ImgBytes))
            {
                using (FileStream stream = new FileStream(imgPath, FileMode.Create))
                {
                    image.Save(stream, new PngEncoder());
                }
            }

            msg.ImageSource = imgPath;
            Console.WriteLine(msg.ImageSource);
        }
        Application.Current.Dispatcher.Invoke(() => RecievedMessages.Add(msg));
    }

    private void RemoveUser()
    {
        var uid = _server.PacketReader.ReadString();
        var user = Users.Where(x => x.IUD == uid).FirstOrDefault();
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

    private async Task MonitorIsTyping()
    {
        while (true)
        {
            if (!string.IsNullOrEmpty(SendingMessageString))
            {
                MainUser.IsTyping = true;
                _server.TypingEvent(MainUser);
            }
            else
            {
                MainUser.IsTyping = false;
                _server.TypingEvent(MainUser);
            }

            await Task.Delay(2000);
        }
    }
}

