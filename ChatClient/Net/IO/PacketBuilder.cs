using System.IO;
using System.Text;

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

    public void WriteString(string msg)
    {
        var msgLenght = msg.Length;
        
        _ms.Write(BitConverter.GetBytes(msgLenght));
        _ms.Write(Encoding.ASCII.GetBytes(msg));
    }

    public byte[] GetPacketBytes()
    {
        return _ms.ToArray();
    }
}