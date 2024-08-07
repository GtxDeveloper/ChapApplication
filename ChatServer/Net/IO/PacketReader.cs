using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using SimpleChatAppWithoutDesign.MVM.Model;

namespace ChatServer.Net.IO;

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
}