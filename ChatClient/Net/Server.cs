using System.Net.Sockets;
using SimpleChatAppWithoutDesign.Net.IO;

namespace SimpleChatAppWithoutDesign.Net;

public class Server
{
    private TcpClient _client;
    public PacketReader PacketReader { get; set; }

    public event Action connectedEvent;
    public event Action msgRecievedEvent;
    public event Action userDisconnectEvent;

    public Server()
    {
        _client = new TcpClient();
    }

    public void ConnecToServer(string username)
    {
        if (!_client.Connected)
        {
            _client.Connect("127.0.0.1", 2000);
            PacketReader = new PacketReader(_client.GetStream());
            if (!string.IsNullOrEmpty(username))
            {
                var connectPacket = new PacketBuilder();
                connectPacket.WriteUpCode(0);
                connectPacket.WriteString(username);
                _client.Client.Send(connectPacket.GetPacketBytes());
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
                    default:
                        Console.WriteLine("ah yes..");
                        break;
                }
            }
        });
    }

    public void SendMessageToServer(string message)
    {
        var messagePacket = new PacketBuilder();
        messagePacket.WriteUpCode(5);
        messagePacket.WriteString(message);
        _client.Client.Send(messagePacket.GetPacketBytes());
    }
}