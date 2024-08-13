using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using SimpleChatAppWithoutDesign.MVM.Model;

namespace SimpleChatAppWithoutDesign.Net.IO;

public class PacketReader : BinaryReader
{

    private NetworkStream _ns;

    public PacketReader(NetworkStream ns) : base(ns)
    {
        _ns = ns;
    }

    public MessageModel ReadMessage()
    {
        byte[] msgBuffer;
        var lenght = ReadInt32();
        msgBuffer = new byte[lenght];
        _ns.Read(msgBuffer, 0, lenght);

        var jsonMsg = Encoding.ASCII.GetString(msgBuffer);

        var msg = JsonSerializer.Deserialize<MessageModel>(jsonMsg);
        return msg;
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
    
    public UserModel ReadUser()
    {
        byte[] msgBuffer;
        var lenght = ReadInt32();
        msgBuffer = new byte[lenght];
        _ns.Read(msgBuffer, 0, lenght);

        var jsonMsg = Encoding.ASCII.GetString(msgBuffer);

        var msg = JsonSerializer.Deserialize<UserModel>(jsonMsg);
        return msg;
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