// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using ChatServer;
using ChatServer.Net.IO;
using SimpleChatAppWithoutDesign.MVM.Model;

class Program
{
    static List<Client> _users;
    static TcpListener _listener;


    static void Main(string[] args)
    {
        _users = new List<Client>();
        _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 2000);

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
                broadcastPacket.WriteUpCode(1);
                broadcastPacket.WriteUser(usr.User);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }
        }
    }

    public static void BroadcastMessage(MessageModel message)
    {
        foreach (var user in _users)
        {
            var msgPack = new PacketBuilder();
            msgPack.WriteUpCode(5);
            msgPack.WriteMessage(message);
            user.ClientSocket.Client.Send(msgPack.GetPacketBytes());
        }
    }
    
    public static void BroadcastMessage(string message)
    {
        foreach (var user in _users)
        {
            var msgPack = new PacketBuilder();
            msgPack.WriteUpCode(5);
            msgPack.WriteString(message);
            user.ClientSocket.Client.Send(msgPack.GetPacketBytes());
        }
    }

    public static void BroadcastIsTypingEvent(UserModel typingUser)
    {
        foreach (var user in _users)
        {
            var msgPack = new PacketBuilder();
            msgPack.WriteUpCode(15);
            msgPack.WriteUser(typingUser);
            user.ClientSocket.Client.Send(msgPack.GetPacketBytes());
        }
    }
    
    public static void BroadcastDisconnect(string uid)
    {
        var disconnectedUser = _users.Where(x => x.User.IUD == uid).FirstOrDefault();
        _users.Remove(disconnectedUser);
        foreach (var user in _users)
        {
            var broadcastPacket = new PacketBuilder();
            broadcastPacket.WriteUpCode(10);
            broadcastPacket.WriteUID(uid);
            user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
        }
        
        BroadcastMessage($"[{disconnectedUser.User.UserName}] Disconnected!");
    }
}