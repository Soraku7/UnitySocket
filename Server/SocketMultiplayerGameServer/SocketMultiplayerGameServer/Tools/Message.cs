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
}