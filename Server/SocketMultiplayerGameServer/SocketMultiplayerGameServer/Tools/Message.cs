using Google.Protobuf;
using SocketGameProtocol;

namespace SocketMultiplayerGameServer.Tools;

public class Message
{
    private byte[] buffer = new byte[1024];
    
    private int startIndex = 0;

    public byte[] Buffer
    {
        get { return buffer; }
    }
    
    public int StartIndex
    {
        get { return startIndex; }
    }
    
    public int RemSize
    {
        get { return buffer.Length - startIndex; }
    }

    public void ReadBuffer(int len , Action<MainPack> HandleRequest)
    {
        startIndex += len;
        
        //包头4字节 小于4则没有正确的包
        if(startIndex <= 4) return;
        
        int count = BitConverter.ToInt32(buffer, 0);

        while (true)
        {
            if (startIndex >= count + 4)
            {
                MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, count);
                HandleRequest(pack);
                Array.Copy(buffer , count + 4 , buffer , 0 , startIndex - 4 - count);
                startIndex -= 4 + count;
            }
            else break;
        }
    }
    
    public static byte[] PackData(MainPack pack)
    {
        byte[] data = pack.ToByteArray(); //包体
        byte[] head = BitConverter.GetBytes(data.Length); //包头

        return head.Concat(data).ToArray();
    }
    
    public static Byte[] PackDataUDP(MainPack pack)
    {
        return pack.ToByteArray();
    }
}