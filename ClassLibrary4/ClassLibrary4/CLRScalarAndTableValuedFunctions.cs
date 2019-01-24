using Microsoft.SqlServer.Server;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ClassLibrary4
{
    class CLRScalarAndTableValuedFunctions
    {
        private class User
        {
            public int UserID { get; set; }
            public string EmailAddress { get; set; }
            public User(int userId, string emailAddress)
            {
                this.UserID = userId;
                this.EmailAddress = emailAddress;
            }
        }

        [SqlFunction(DataAccess = DataAccessKind.Read)]
        public static int GetUserCountByEmail(string emailAddress)
        {
            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) FROM [dbo].[Users] WHERE EmailAddress=@EmailAddress", connection);
                SqlParameter modifiedSinceParam = cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar);
                modifiedSinceParam.Value = emailAddress;
                return (int)cmd.ExecuteScalar();
            }
        }

        [SqlFunction(DataAccess = DataAccessKind.Read,
            FillRowMethodName = "FillRow")]
        public static IEnumerable GetUsersByEmail(string emailAddress)
        {
            ArrayList users = new ArrayList();
            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT UserID,EmailAddress FROM [dbo].[Users] WHERE EmailAddress=@EmailAddress", connection);
                SqlParameter modifiedSinceParam = cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar);
                modifiedSinceParam.Value = emailAddress;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User((int)reader.GetSqlInt32(0), (string)reader.GetSqlString(1)));
                    }
                }
            }
            return users;
        }
        [SqlFunction(DataAccess = DataAccessKind.Read,
            FillRowMethodName = "FillRow",
            TableDefinition = "UserID int, EmailAddress nvarchar(150)")]
        public static IEnumerable GetUsersByEmailWithTableDefinition(string emailAddress)
        {
            ArrayList users = new ArrayList();
            using (SqlConnection connection = new SqlConnection("context connection=true"))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT UserID,EmailAddress FROM [dbo].[Users] WHERE EmailAddress=@EmailAddress", connection);
                SqlParameter modifiedSinceParam = cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar);
                modifiedSinceParam.Value = emailAddress;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User((int)reader.GetSqlInt32(0), (string)reader.GetSqlString(1)));
                    }
                }
            }
            return users;
        }
        public static void FillRow(object user, out SqlInt32 UserID, out string EmailAddress)
        {
            User _user = (User)user;
            UserID = _user.UserID;
            EmailAddress = _user.EmailAddress;

        }
    }
}
