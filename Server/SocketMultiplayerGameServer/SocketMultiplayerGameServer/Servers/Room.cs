using System.Diagnostics;
using Google.Protobuf.Collections;
using SocketGameProtocol;

namespace SocketMultiplayerGameServer.Servers;

public class Room
{
    private RoomPack _roomPack;

    List<Client> _clients = new List<Client>();
    public RoomPack GetRoomInfo
    {
        get
        {
            _roomPack.Curnum = _clients.Count;
            return _roomPack;
        }
    }
    
    public Room(Client client , RoomPack pack)
    {
        _roomPack = pack;
        _clients.Add(client);
        client.GetRoom = this;
    }

    public RepeatedField<PlayerPack> GetplayerInfo()
    {
        RepeatedField<PlayerPack> playerPacks = new RepeatedField<PlayerPack>();
        foreach (var c in _clients)
        {
            PlayerPack player = new PlayerPack();
            player.Playername = c.UserName;
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
        client.GetRoom = this;
        MainPack pack = new MainPack();
        pack.Actioncode = ActionCode.PlayerList;

        foreach (PlayerPack player in GetplayerInfo())
        {
            pack.Playerpack.Add(player);
        }
        
        Boardcast(client , pack);
    }

    public void Exit(Client client)
    {
        _clients.Remove(client);
        client.GetRoom = null;
        MainPack mainPack = new MainPack();
        mainPack.Actioncode = ActionCode.PlayerList;

        foreach (PlayerPack player in GetplayerInfo())
        {
            mainPack.Playerpack.Add(player);
        }
        
        Boardcast(client , mainPack);
    }
}