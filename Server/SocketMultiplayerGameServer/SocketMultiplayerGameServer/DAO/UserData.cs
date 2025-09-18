using MySql.Data.MySqlClient;
using SocketGameProtocol;

namespace SocketMultiplayerGameServer.DAO;

public class UserData
{
    private MySqlConnection mysqlConnection;

    public bool Logon(MainPack pack , MySqlConnection mysqlConnection)
    {
        string username = pack.Loginpack.Username;
        string password = pack.Loginpack.Password;
        
        Console.WriteLine("添加用户" + username + password);
        
        string sqlSelect = "SELECT * FROM 'sys' . 'userdata' WHERE username = '@username';";
        MySqlCommand comd = new MySqlCommand(sqlSelect, mysqlConnection);
        // MySqlDataReader reader = comd.ExecuteReader();
        // if(reader.Read())
        // {
        //     return false;
        // }

        try
        {
            string sqlIn = "INSERT INTO `sys`.`userdata` (`username`, `password`) VALUES ('" + username + "', '" + password + "')";
            comd = new MySqlCommand(sqlIn , mysqlConnection);
            comd.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    } 
    
    public bool Login(MainPack pack , MySqlConnection mysqlConnection)
    {
        string username = pack.Loginpack.Username;
        string password = pack.Loginpack.Password;
        
        string sql = "SELECT * FROM `sys`.`userdata` WHERE username = '" + username + "' AND password = '" + password + "';";
        MySqlCommand cmd = new MySqlCommand(sql , mysqlConnection);
        MySqlDataReader read = cmd.ExecuteReader();

        bool result = read.HasRows;
        read.Close();
        
        return result;
    }
}