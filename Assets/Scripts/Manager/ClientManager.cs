using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SocketGameProtocol;
using UnityEngine;

namespace Manager
{
    public class ClientManager : BaseManager
    {
        private Socket _socket;
        private Message _message;

        private Socket _udpClient;
        private IPEndPoint _ipEndPoint;
        private EndPoint _endPoint;
        private Thread _aucThread;
        private Byte[] _buffer = new Byte[1024];
        private const string ip = "127.0.0.1";
        
        public ClientManager(GameFace face) : base(face)
        {
        }

        public override void OnInit()
        {
            base.OnInit();
            _message = new Message();
            InitSocket();

            InitUDP();
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
                GameFace.Instance.ShowMessage("连接服务器成功");
                StartRecieve();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                GameFace.Instance.ShowMessage("连接服务器失败");
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
        
        private void InitUDP()
        {
            _udpClient = new Socket(AddressFamily.InterNetwork , SocketType.Dgram , ProtocolType.Udp);
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), 6667);
            _endPoint = (EndPoint)_ipEndPoint;
            try
            {
                _udpClient.Connect(_endPoint);
            }
            catch
            {
                Debug.Log("连接失败");
                return;
            }

            _aucThread = new Thread(RecieveMsg);
            _aucThread.Start();
        }

        private void RecieveMsg()
        {
            Debug.Log("UDP开始接收");
            while (true)
            {
                int len = _udpClient.ReceiveFrom(_buffer , ref _endPoint);
                MainPack pack = (MainPack) MainPack.Descriptor.Parser.ParseFrom(_buffer , 0 , len);
                HandleResponse(pack);
            }
        }

        public void SendTo(MainPack pack)
        {
            byte[] sendBuffer = Message.PackDataUDP(pack);
            _udpClient.Send(sendBuffer , sendBuffer.Length ,SocketFlags.None);
        }
    }
}