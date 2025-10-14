using System.Diagnostics;
using Google.Protobuf.Collections;
using SocketGameProtocol;

namespace SocketMultiplayerGameServer.Servers;

public class Room
{
    private RoomPack _roomPack;

    private Server _server;

    List<Client> _clients = new List<Client>();
    public RoomPack GetRoomInfo
    {
        get
        {
            _roomPack.Curnum = _clients.Count;
            return _roomPack;
        }
    }
    
    public Room(Client client , RoomPack pack , Server server)
    {
        _roomPack = pack;
        _clients.Add(client);
        client.GetRoom = this;
        _server = server;
    }

    public RepeatedField<PlayerPack> GetplayerInfo()
    {
        RepeatedField<PlayerPack> playerPacks = new RepeatedField<PlayerPack>();
        foreach (var c in _clients)
        {
            PlayerPack player = new PlayerPack();
            player.Playername = c.GetUserInfo.UserName;
            playerPacks.Add(player);

        }

        return playerPacks;
    }

    public void Boardcast(Client client , MainPack pack)
    {
        foreach (Client c in _clients)
        {
            if (c.Equals(client))
            {
                continue;
                
            }
            
            c.Send(pack); 
        }
    }

    public void Join(Client client)
    {
        _clients.Add(client);

        if (_roomPack.Maxnum >= _clients.Count)
        {
            //满人了
            _roomPack.Statc = 1;
        }
        
        client.GetRoom = this;
        MainPack pack = new MainPack();
        pack.Actioncode = ActionCode.PlayerList;

        foreach (PlayerPack player in GetplayerInfo())
        {
            pack.Playerpack.Add(player);
        }
        
        Boardcast(client , pack);
    }

    public void Exit(Server server , Client client)
    {
        MainPack mainPack = new MainPack();
        
        //判断房主退出
        if (client == _clients[0])
        {
            client.GetRoom = null;
            mainPack.Actioncode = ActionCode.Exit;
            Boardcast(client , mainPack);
            server.RemoveRoom(this);
            Console.WriteLine("房主退出");
            return;
        }
        
        _clients.Remove(client);

        //更新房间状态为可加入
        _roomPack.Statc = 0;
        
        client.GetRoom = null;
        
        mainPack.Actioncode = ActionCode.PlayerList;

        foreach (PlayerPack player in GetplayerInfo())
        {
            mainPack.Playerpack.Add(player);
        }
        
        Boardcast(client , mainPack);
    }

    public ReturnCode StartGame(Client client)
    {
        //当前玩家不为房主
        if (client != _clients[0])
        {
            return ReturnCode.Fail;
        }
        
        Thread startTime = new Thread(Time);
        startTime.Start();
        return ReturnCode.Succeed;
    }

    private void Time()
    {
        MainPack pack = new MainPack();
        pack.Actioncode = ActionCode.Chat;
        pack.Str = "房主已启动游戏";
        
        Boardcast(null , pack);
        Thread.Sleep(1000);

        for (int i = 5; i > 0; i--)
        {
            pack.Str = i.ToString();
            Boardcast(null , pack);
            Thread.Sleep(1000);
        }

        pack.Actioncode = ActionCode.Starting;

        foreach (var client in _clients)
        {
            PlayerPack player = new PlayerPack();
            client.GetUserInfo.Hp = 100;
            player.Playername = client.GetUserInfo.UserName;
            player.Hp = client.GetUserInfo.Hp;
            pack.Playerpack.Add(player);
        }
        Console.WriteLine("广播游戏开始");
        pack.Str = "游戏开始";
        Boardcast(null , pack);
    }

    public void ExitGame(Client client)
    {
        MainPack pack = new MainPack();
        
        if (client == _clients[0])
        {
            //房主退出游戏
            pack.Actioncode = ActionCode.Exit;
            pack.Str = "r";
            Boardcast(client , pack);
            _server.RemoveRoom(this);
        }
        else
        {
            //其他成员退出游戏
            _clients.Remove(client);
            client.GetRoom = null;
            pack.Actioncode = ActionCode.UpCharacterList;

            foreach (var c in _clients)
            {
                PlayerPack playerPack = new PlayerPack();
                playerPack.Playername = c.GetUserInfo.UserName;
                playerPack.Hp = c.GetUserInfo.Hp;
                pack.Playerpack.Add(playerPack);
            }
            pack.Str = client.GetUserInfo.UserName;
            Boardcast(client , pack);
        }
    }
}