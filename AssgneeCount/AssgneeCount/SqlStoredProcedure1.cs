using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public class TicketCount
{
    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static int ReturnOrderCount()
    {
        using (SqlConnection connection = new SqlConnection("context connection=true"))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT COUNT(*) AS 'Ticket Count' From Tickets");
            return (int)command.ExecuteScalar();

        }
    }
}

