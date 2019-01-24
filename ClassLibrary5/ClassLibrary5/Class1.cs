using Microsoft.SqlServer.Server;
using System.Data.SqlClient;

public class T
{
    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static int ReturnOrderCount()
    {
        using (SqlConnection conn
            = new SqlConnection("context connection=true"))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(*) AS 'Order Count' FROM Tickets", conn);
            return (int)cmd.ExecuteScalar();
        }
    }
}