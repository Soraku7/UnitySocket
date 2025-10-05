using System.Collections.Generic;
using SocketGameProtocol;
using UnityEngine;

namespace Manager
{
    public class PlayerManager : BaseManager
    {
        public PlayerManager(GameFace face) : base(face)
        {
        }

        private Dictionary<string, GameObject> _players = new Dictionary<string, GameObject>();

        private GameObject _character;
        private Transform _spawnPos;
        
        public string CurPlayerId
        {
            get;
            set;
        }

        public override void OnInit()
        {
            base.OnInit();
            _character = Resources.Load("Prefab/Character") as GameObject;
        }

        public void AddPlayer(List<PlayerPack> pack)
        {
            _spawnPos = GameObject.Find("SpawnPos").transform;
            foreach (var p in pack)
            {
                GameObject gameObject = GameObject.Instantiate(_character, _spawnPos.position, Quaternion.identity);
                if (p.Playername.Equals(CurPlayerId))
                {
                    //创建本地角色
                }
                //创建其他客户端角色
                
                _players.Add(p.Playername , gameObject);
            }
        }

        public void RemovePlayer(string id)
        {
            if (_players.TryGetValue(id, out GameObject go))
            {
                GameObject.Destroy(go);
                _players.Remove(id);
            }
            else
            {
                Debug.Log("移除玩家失败");
            }
            
        }
    }
}