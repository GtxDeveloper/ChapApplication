using System.Net.Sockets;
using ChatServer.Net.IO;

namespace ChatServer;

public class Client
{
    public string Name { get; set; }
    
    public Guid UID { get; set; }
    
    public TcpClient ClientSocket { get; set; }

    private PacketReader _packetReader;

    public Client(TcpClient client)
    {
        ClientSocket = client;
        UID = Guid.NewGuid();
        _packetReader = new PacketReader(ClientSocket.GetStream());

        var opcode = _packetReader.ReadByte();

        Name = _packetReader.ReadMessage();
        
        Console.WriteLine($"{DateTime.Now}: Client has connected with the username: {Name}");

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
                        Console.WriteLine($"{DateTime.Now}: Message recieved {msg}");
                        Program.BroadcastMessage($"[{DateTime.Now}] : [{Name}] : {msg}");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{UID.ToString()}: Disconnected");
                Program.BroadcastDisconnect(UID.ToString());
                ClientSocket.Close();
                break;
            }   
        }
    }
}