using MySql.Data.MySqlClient;
using SocketGameProtocol;

namespace SocketMultiplayerGameServer.DAO;

public class UserData
{
    private MySqlConnection mysqlConnection;

    private string connectStr =
        "database=sys ; data source=127.0.0.1 ; password = 123456789ddd ; pooling = false ; charset = utf8 ; port = 3306";
    
    public UserData()
    {
        ConnectMysql();
    }

    private void ConnectMysql()
    {
        try
        {
            mysqlConnection = new MySqlConnection(connectStr);
            mysqlConnection.Open();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message + "连接数据库失败");
        }
    }

    public bool Logon(MainPack pack)
    {
        string username = pack.Loginpack.Username;
        string password = pack.Loginpack.Password;
        
        string sqlSelect = "SELECT * FROM 'sys' . 'userdata' WHERE username = '@username';";
        MySqlCommand comd = new MySqlCommand(sqlSelect, mysqlConnection);
        MySqlDataReader reader = comd.ExecuteReader();
        if(reader.Read())
        {
            return false;
        }
        
        string sqlIn = "INSERT INTO 'sys' . 'userdata' ('username', 'password') VALUES ('@username', '@password');";
        comd = new MySqlCommand(sqlIn , mysqlConnection);

        try
        {
            comd.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    } 
}