using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using SocketMultiplayerGameServer.Servers;

namespace SocketMultiplayerGameServer;

internal class Program
{
    private static void Main(string[] args)
    {
        Server server = new Server(6666);
        Console.WriteLine("服务器启动成功");
        Console.Read();
    }
}