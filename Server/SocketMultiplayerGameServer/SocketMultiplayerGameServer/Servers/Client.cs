using System.Net.Sockets;
using MySql.Data.MySqlClient;
using SocketGameProtocol;
using SocketMultiplayerGameServer.DAO;
using SocketMultiplayerGameServer.Tools;

namespace SocketMultiplayerGameServer.Servers;

public class Client
{
        
    private string connectStr =
        "database=sys ; data source=localhost ; user = root ; password = 123456789ddd ; pooling = false ; charset = utf8 ; port = 3306";
    
    private Socket socket;
    private Message message;    
    private UserData userData;
    private Server server;
    private MySqlConnection mysqlConnection;

    private string _username;

    public UserInfo GetUserInfo
    {
        get;
        set;
    }

    public class UserInfo
    {
        public string UserName
        {
            get; set;
        }

        public int Hp
        {
            get;
            set;
        }
        
        public PosPack Pos
        {
            get;
            set;
        }
    }
    
    public Room GetRoom
    {
        get;
        set;
    }
    
    public UserData GetUserData
    {
        get { return userData; }
    }
    
    public MySqlConnection GetMysqlConnection
    {
        get { return mysqlConnection; }
    }
    
    public Client(Socket socket , Server server)
    {
        this.socket = socket;
        this.server = server;

        mysqlConnection = new MySqlConnection(connectStr);
        mysqlConnection.Open();
        
        userData = new UserData();
        message = new Message();
        GetUserInfo = new UserInfo();
        StartRecieve();
    }

    private void StartRecieve()
    {
        socket.BeginReceive(message.Buffer, message.StartIndex, message.RemSize, SocketFlags.None, ReceiveCallback , null);
    }
    
    private void ReceiveCallback(IAsyncResult iar)
    {
        try
        {
            if (socket == null || socket.Connected == false) return;
            int len = socket.EndReceive(iar);
            if(len <= 0)
            {
                Console.WriteLine("接收数据为0");
                Close();
                return;
            }
            
            message.ReadBuffer(len , HandleRequest);
            StartRecieve();
        }
        catch
        {
            
        }

    }

    public void Send(MainPack pack)
    {
        socket.Send(Message.PackData(pack));
    }
    
    private void HandleRequest(MainPack pack)
    {
        server.HandleRequest(pack , this);
    }

    private void Close()
    {
        if (GetRoom != null)
        {
            GetRoom.Exit(server , this);
        }
        
        socket.Close();
        server.RemoveClient(this);
        mysqlConnection.Close();
    }
    
    public void UpPos(MainPack pack)
    {
        GetUserInfo.Pos = pack.Playerpack[0].Pospack;
    }
}