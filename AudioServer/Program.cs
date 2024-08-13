
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using AudioServer;
using AudioServer.Net.IO;

class Program
{
    static IPEndPoint _endPoint;
    private static TcpListener _listener;
    private static List<Client> _users;
    
    static void Main(string[] args)
    {
        _endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
        _listener = new TcpListener(_endPoint);
        _users = new List<Client>();
        
        _listener.Start();

        while (true)
        {
            var client = new Client(_listener.AcceptTcpClient());
            
            _users.Add(client);
            
            BroadcastConnection();
        }
    }

    static void BroadcastConnection()
    {
        foreach (var user in _users)
        {
            foreach (var usr in _users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteIpEndPoint(usr.Ip.Ip, usr.Ip.Port, 1);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                Console.WriteLine("SENDED");
            }
        }
    }
    
}