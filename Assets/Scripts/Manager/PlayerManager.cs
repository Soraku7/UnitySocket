using System.Collections.Generic;
using Player;
using Request;
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
        private GameObject _bullet;
        
        public string CurPlayerId
        {
            get;
            set;
        }

        public override void OnInit()
        {
            base.OnInit();
            _character = Resources.Load("Prefabs/Character") as GameObject;
            _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        }

        public void AddPlayer(MainPack pack)
        {
            _spawnPos = GameObject.Find("SpawnPosition").transform;
            foreach (var p in pack.Playerpack)
            {
                GameObject character = GameObject.Instantiate(_character, _spawnPos.position, Quaternion.identity);
                if (p.Playername.Equals(GameFace.Instance.userName))
                {
                    //创建本地角色
                    character.AddComponent<Rigidbody2D>().gravityScale = 3;
                    character.AddComponent<UpPosRequest>();
                    character.AddComponent<UpPos>();
                    character.AddComponent<PlayerController>();
                    character.transform.Find("HandGun").gameObject.AddComponent<FireRequest>();
                    character.transform.Find("HandGun").gameObject.AddComponent<GunController>();
                }
                else
                {
                    //创建其他客户端角色
                }
                
                
                _players.Add(p.Playername , character);
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

        public void GameExit()
        {
            foreach (var player in _players.Values)
            {
                GameObject.Destroy(player);
            }
            
            _players.Clear();
        }

        public void UpPos(MainPack pack)
        {
            PosPack posPack = pack.Playerpack[0].Pospack;
            if (_players.TryGetValue(pack.Playerpack[0].Playername, out GameObject go))
            {
                Vector2 pos = new Vector2(posPack.PosX , posPack.PosY);
                float characterRot = posPack.RotZ;
                float gunRot = posPack.GunRotZ;

                go.transform.position = pos;
                go.transform.eulerAngles = new Vector3(0 , 0 , characterRot);
                go.transform.Find("HandGun").eulerAngles = new Vector3(0, 0, gunRot);
            }
        }

        public void SpawnBullet(MainPack pack)
        {
            Vector3 pos = new Vector3(pack.Bulletpack.PosX , pack.Bulletpack.PosY , 0);
            float rot = pack.Bulletpack.RotZ;
            Vector3 mousePos = new Vector3(pack.Bulletpack.MousePosX , pack.Bulletpack.MousePosY , 0);
            Vector3 velocity = (mousePos - pos).normalized * 20;
            
            GameObject go = GameObject.Instantiate(_bullet, pos, Quaternion.identity);
            go.transform.eulerAngles = new Vector3(0, 0, rot);
            go.GetComponent<Rigidbody2D>().linearVelocity = velocity;
        }
    }
}