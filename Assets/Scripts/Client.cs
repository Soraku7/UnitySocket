using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Socket socket;
    private byte[] buffer = new byte[1024];

    private void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect("127.0.0.1", 6666);//连接服务器
        StartRecieve();
        Send();
    }

    private void StartRecieve()
    {
        socket.BeginReceive(buffer , 0 , buffer.Length , SocketFlags.None, RecevieCallback, null);
    }

    //异步接收消息
    private void RecevieCallback(IAsyncResult iar)
    {
        int len = socket.EndReceive(iar);

        if (len == 0)
        {
            return;
        }
        
        string str = Encoding.UTF8.GetString(buffer, 0, len);
        Debug.Log(str);
        StartRecieve();
    }

    private void Send()
    {
        socket.Send(Encoding.UTF8.GetBytes("Hello Server!"));
    }
}
