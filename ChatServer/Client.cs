using System.Net.Sockets;
using ChatServer.Net.IO;
using SimpleChatAppWithoutDesign.MVM.Model;

namespace ChatServer;

public class Client
{
    public UserModel User { get; set; }

    public TcpClient ClientSocket { get; set; }

    private PacketReader _packetReader;

    public Client(TcpClient client)
    {
        ClientSocket = client;
       
        _packetReader = new PacketReader(ClientSocket.GetStream());

        var opcode = _packetReader.ReadByte();

        User = _packetReader.ReadUser();
        
        Console.WriteLine($"{DateTime.Now}: Client has connected with the username: {User.UserName}");

        Task.Run(() => Process());
    }

    void Process()
    {
        while (true)
        {
            try
            {
                var opcode = _packetReader.ReadByte();
                switch (opcode)
                {
                    case 5:
                        var msg = _packetReader.ReadMessage();
                        Console.WriteLine($"{DateTime.Now}: Message recieved {msg.Message}");
                        msg.SendingTime = DateTime.Now;
                        msg.MessageBy = User.UserName;
                        msg.FullMessage = $"[{msg.SendingTime}] : [{msg.MessageBy}] : {msg.Message}";
                        Program.BroadcastMessage(msg);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{User.IUD}: Disconnected");
                Program.BroadcastDisconnect(User.IUD);
                ClientSocket.Close();
                break;
            }   
        }
    }
}