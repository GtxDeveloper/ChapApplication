using System.Net.Sockets;
using AudioServer.Model;
using AudioServer.Net.IO;

namespace AudioServer;

public class Client
{
    public TcpClient ClientSocket { get; set; }

    private PacketReader _packetReader;

    public IpModel Ip;
    
    public Client(TcpClient client)
    {
        ClientSocket = client;
        
        _packetReader = new PacketReader(ClientSocket.GetStream());

        var opcode = _packetReader.ReadByte();
        
        Console.WriteLine(opcode);

        Ip = _packetReader.ReadIpEndPoint();

        Console.WriteLine($"Connection with endpoint {Ip.Ip}:{Ip.Port}");
    }
}