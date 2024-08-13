using System.Net;
using System.Text;
using System.Text.Json;
using AudioServer.Model;

namespace AudioServer.Net.IO;

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

    

    public void WriteIpEndPoint(string ip, int port, int opCode)
    {
        var ipModel = new IpModel()
        {
            Ip = ip,
            Port = port,
            OpCode = opCode
        };
        
        var json = JsonSerializer.Serialize<IpModel>(ipModel);
        var msgLenght = json.Length;
        _ms.Write(BitConverter.GetBytes(msgLenght));
        _ms.Write(Encoding.ASCII.GetBytes(json));
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