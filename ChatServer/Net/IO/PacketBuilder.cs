using System.Text;

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

    public void WriteMessage(string msg)
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