using System;
using System.Net.Sockets;
using SocketGameProtocol;
using UnityEngine;

namespace Manager
{
    public class ClientManager : BaseManager
    {
        private Socket _socket;
        private Message _message;
        
        public ClientManager(GameFace face) : base(face)
        {
        }

        public override void OnInit()
        {
            base.OnInit();
            _message = new Message();
            InitSocket();
        }
        
        public override void OnDestroy()
        {
            base.OnDestroy();
            _message = null;
            CloseSocket();
        }

        private void InitSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _socket.Connect("127.0.0.1", 6666);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        private void CloseSocket()
        {
            if(_socket.Connected && _socket != null) _socket.Close();
            
        }
        
        private void StartRecieve()
        {
            _socket.BeginReceive(_message.Buffer, _message.StartIndex, _message.RemSize, SocketFlags.None, ReceiveCallback , null);
        }
        
        private void ReceiveCallback(IAsyncResult iar)
        {
            try
            {
                if (_socket == null || _socket.Connected == false) return;
                int len = _socket.EndReceive(iar);
                if (len <= 0)
                {
                    CloseSocket();
                    return;
                }

                _message.ReadBuffer(len, HandleResponse);
                StartRecieve();
            }
            catch
            {
                
            }
        }
        
        public void HandleResponse(MainPack pack)
        {
            GameFace.Instance.HandleResponse(pack);
        }

        public void Send(MainPack pack)
        {
            _socket.Send(Message.PackData(pack));
        }
    }
}