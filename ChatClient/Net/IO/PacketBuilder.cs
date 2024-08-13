using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using SimpleChatAppWithoutDesign.MVM.Model;

namespace SimpleChatAppWithoutDesign.Net.IO;

public class PacketBuilder
{
    private MemoryStream _ms;
    public PacketBuilder()
    {
        _ms = new MemoryStream();
    }

    public void WriteUpCode(byte opcode)
    {
        _ms.WriteByte(opcode);
    }

    public void WriteMessage(MessageModel msg)
    {
        var jsonMsg = JsonSerializer.Serialize<MessageModel>(msg);
        var msgLenght = jsonMsg.Length;
        _ms.Write(BitConverter.GetBytes(msgLenght));
        _ms.Write(Encoding.ASCII.GetBytes(jsonMsg));
    }

    public void WriteIpEndPoint(string ip, int port)
    {
        var ipModel = new IpModel()
        {
            Ip = ip,
            Port = port
        };
        
        var json = JsonSerializer.Serialize<IpModel>(ipModel);
        var msgLenght = json.Length;
        _ms.Write(BitConverter.GetBytes(msgLenght));
        _ms.Write(Encoding.ASCII.GetBytes(json));
    }
    
    public void WriteUser(UserModel msg)
    {
        var jsonMsg = JsonSerializer.Serialize<UserModel>(msg);
        
        var msgLenght = jsonMsg.Length;
        
        _ms.Write(BitConverter.GetBytes(msgLenght));
        _ms.Write(Encoding.ASCII.GetBytes(jsonMsg));
    }

    public void WriteUID(string uid)
    {
        var msgLength = uid.Length;
        _ms.Write(BitConverter.GetBytes(msgLength));
        _ms.Write(Encoding.ASCII.GetBytes(uid));
    }

    public byte[] GetPacketBytes()
    {
        return _ms.ToArray();
    }
}