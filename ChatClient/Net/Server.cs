using System.Net.Sockets;
using SimpleChatAppWithoutDesign.MVM.Model;
using SimpleChatAppWithoutDesign.Net.IO;

namespace SimpleChatAppWithoutDesign.Net;

public class Server
{
    private TcpClient _client;
    public PacketReader PacketReader { get; set; }

    public event Action connectedEvent;
    public event Action msgRecievedEvent;
    public event Action userDisconnectEvent;

    public event Action userTypingEvent;

    public Server()
    {
        _client = new TcpClient();
    }

    public void ConnecToServer(UserModel user)
    {
        if (!_client.Connected)
        {
            _client.Connect("127.0.0.1", 2000);
            PacketReader = new PacketReader(_client.GetStream());
            if (!string.IsNullOrEmpty(user.UserName))
            {
                
                var connectPacket = new PacketBuilder();
                connectPacket.WriteUpCode(0);
                connectPacket.WriteUser(user);
                _client.Client.Send(connectPacket.GetPacketBytes());
                Console.WriteLine($"User {user.UserName} sended ");
            }

            ReadPackets();
        }
    }

    private void ReadPackets()
    {
        Task.Run(() =>
        {
            while (true)
            {
                var opcode = PacketReader.ReadByte();
                switch (opcode)
                {
                    case 1:
                        connectedEvent?.Invoke();
                        break;
                    case 5:
                        msgRecievedEvent?.Invoke();
                        break;
                    case 10:
                        userDisconnectEvent?.Invoke();
                        break;
                    case 15:
                        userTypingEvent?.Invoke();
                        break;
                    default:
                        Console.WriteLine("ah yes..");
                        break;
                }
            }
        });
    }

    public void SendMessageToServer(MessageModel message)
    {
        var messagePacket = new PacketBuilder();
        messagePacket.WriteUpCode(5);
        messagePacket.WriteMessage(message);
        _client.Client.Send(messagePacket.GetPacketBytes());
    }

    public void TypingEvent(UserModel mainUser)
    {
        var packet = new PacketBuilder();
        packet.WriteUpCode(15);
        packet.WriteUser(mainUser);
        _client.Client.Send(packet.GetPacketBytes());
    }
}

