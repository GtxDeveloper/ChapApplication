using System.Text;
using System.Text.Json;
using SimpleChatAppWithoutDesign.MVM.Model;

namespace ChatServer.Net.IO;

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

    public void WriteString(string msg)
    {
        var msgLength = msg.Length;
        _ms.Write(BitConverter.GetBytes(msgLength));
        _ms.Write(Encoding.ASCII.GetBytes(msg));
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