using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SimpleChatAppWithoutDesign.MVM.Model;
using SimpleChatAppWithoutDesign.Net.IO;

namespace SimpleChatAppWithoutDesign.MVM.View;

public partial class VideoChatWindow : Window
{
    public bool IsCallActive = false;
    public bool IsAudioActive = false;
    public bool IsVideoActive = false;
    public UdpClient SenderUdpClient;
    public TcpClient LocalTcpClient;
    public IPEndPoint CurrentIpEndPoint;
    public IPEndPoint ServerIP;
    public IPEndPoint SendTo;
    public PacketReader Reader;

    public VideoChatWindow()
    {
        InitializeComponent();
        Random random = new Random();
        CurrentIpEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), random.Next(4000, 4100));
        ServerIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
        SenderUdpClient = new UdpClient(CurrentIpEndPoint);
        LocalTcpClient = new TcpClient();
    }

    private void Button_StartCall_OnClick(object sender, RoutedEventArgs e)
    {
        if (IsCallActive)
        {
            Button_StartCall.Background = Brushes.Green;
            Button_StartCall.Content = "GO";
            LocalTcpClient.Close();
            IsCallActive = false;
        }
        else
        {
            Button_StartCall.Background = Brushes.Red;
            Button_StartCall.Content = "X";
            var packetBuilder = new PacketBuilder();
            LocalTcpClient.Connect(ServerIP);
            Reader = new PacketReader(LocalTcpClient.GetStream());
            packetBuilder.WriteUpCode(0);
            packetBuilder.WriteIpEndPoint(CurrentIpEndPoint.Address.ToString(), CurrentIpEndPoint.Port);
            LocalTcpClient.Client.Send(packetBuilder.GetPacketBytes());
            IsCallActive = true;
            ReadPackets();
        }
    }

    private void ReadPackets()
    {
        Task.Run(() =>
        {
            while (IsCallActive)
            {
                Console.WriteLine("Waiting");
                var msg = Reader.ReadIpEndPoint();
                
                    switch (msg.OpCode)
                    {
                        case 1:
                            UserConnected(msg);
                            break;
                        default:
                            Console.WriteLine("something wrong...");
                            break;
                    }
               Console.WriteLine("End");
            }
        });
    }

    private void Button_Video_OnClick(object sender, RoutedEventArgs e)
    {
        BrushConverter converter = new BrushConverter();
        if (IsVideoActive)
        {
            Button_Video.Background = (Brush)converter.ConvertFromString("#2b2d31");
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("../../Img/camoff.png", UriKind.Relative);
            bitmapImage.EndInit();
            Image_ButtonVideo.Source = bitmapImage;
            IsVideoActive = false;
        }
        else
        {
            Button_Video.Background = Brushes.White;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("../../Img/camon.png", UriKind.Relative);
            bitmapImage.EndInit();
            Image_ButtonVideo.Source = bitmapImage;
            IsVideoActive = true;
        }
    }

    private void Button_Audio_OnClick(object sender, RoutedEventArgs e)
    {
        BrushConverter converter = new BrushConverter();
        if (IsAudioActive)
        {
            Button_Audio.Background = (Brush)converter.ConvertFromString("#2b2d31");
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("../../Img/micoff.png", UriKind.Relative);
            bitmapImage.EndInit();
            Image_ButtonAudio.Source = bitmapImage;
            IsAudioActive = false;
        }
        else
        {
            Button_Audio.Background = Brushes.White;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("../../Img/micon.png", UriKind.Relative);
            bitmapImage.EndInit();
            Image_ButtonAudio.Source = bitmapImage;
            IsAudioActive = true;
        }
    }

    private void UserConnected(IpModel ip)
    {
        Console.WriteLine($"User Recieved with ip {ip.Ip}:{ip.Port}");

        if (ip.Port != CurrentIpEndPoint.Port)
        {
            Console.WriteLine("!");
            SendTo = new IPEndPoint(IPAddress.Parse(ip.Ip), ip.Port);
            Console.WriteLine("!!");
        }
    }
}