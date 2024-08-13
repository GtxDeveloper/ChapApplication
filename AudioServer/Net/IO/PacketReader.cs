using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using AudioServer.Model;

namespace AudioServer.Net.IO;

public class PacketReader : BinaryReader
{

    private NetworkStream _ns;

    public PacketReader(NetworkStream ns) : base(ns)
    {
        _ns = ns;
    }

    
    public IpModel ReadIpEndPoint()
    {
        byte[] msgBuffer;
        var lenght = ReadInt32();
        msgBuffer = new byte[lenght];
        _ns.Read(msgBuffer, 0, lenght);

        var ipEndPoint = JsonSerializer.Deserialize<IpModel>(Encoding.ASCII.GetString(msgBuffer));

        return ipEndPoint;
    }
    

    public string ReadString()
    {
        byte[] msgBuffer;
        var lenght = ReadInt32();
        msgBuffer = new byte[lenght];
        _ns.Read(msgBuffer, 0, lenght);

        var msg = Encoding.ASCII.GetString(msgBuffer);
        
        return msg;
    }
}