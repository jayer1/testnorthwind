using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace GetAssignedCount
{
    public partial class UserDefinedFunctions
    {
        [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read)]
        public static SqlString AssignedCount()
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {           
                   string sql =  "select u.FirstName, u.LastName, Count(t.Assigned) as Assigned from Users u "
                    + "join Tickets t on u.UserID = t.Assigned"
                    + "group by t.Assigned, u.FirstName, u.LastName"
                    + "order by u.FirstName, u.LastName ";
                    connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = connection;
                result += cmd.ExecuteReader().ToString();

            }
            return result;
        }
    }
}