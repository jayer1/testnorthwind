using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;
using System.Data.SqlClient;

namespace newTicketSystem
{
    class TicketFile
    {
        public string filePath { get; set; }

        protected Logger logger { get { return _logger; } }
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public List<Records> ListTickets()
        {
            List<Records> records = new List<Records>();

            // read from the file
            //StreamReader sr = null;

            try
            {
                logger.Info("Reading database records");
                // Connect to database
                using (SqlConnection conn = new SqlConnection(newTicketSystem.Properties.Settings.Default.BITSQL))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Select t.TicketID, t.TicketTypeID, t.Summary, t.Status, t.Priority, ut.FirstName + ' ' + ut.LastName as SubmittedBy, "
                        + "ua.FirstName + ' ' + ua.LastName as AssignedTo, ba.Severity, uw.FirstName + ' ' + uw.LastName as Watchers "
                        + "from dbo.Tickets t "
                        + "join dbo.BugAttributes ba on t.TicketID = ba.TicketID "
                        + "left join dbo.WatchingUsers wu on t.TicketID = wu.TicketID "
                        + "join dbo.Users uw on wu.UserID = uw.UserID "
                        + "join dbo.Users ut on t.Submitter = ut.UserID "
                        + "join dbo.Users ua on t.Assigned = ua.UserID ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            string prevTicket = "";
                            Ticket myRecord = null;
                            while (dr.Read())
                            {
                                if (prevTicket != dr["TicketID"].ToString())
                                {
                                    if (prevTicket != "")
                                    {
                                        records.Add(myRecord);
                                    }
                                    string type = dr["TicketTypeID"].ToString();
                                    if (type == "1")
                                    {
                                        myRecord = new Ticket();
                                    }
                                    myRecord.recordID = dr["TicketID"].ToString();
                                    myRecord.summary = dr["Summary"].ToString();
                                    myRecord.status = dr["Status"].ToString();
                                    myRecord.priority = dr["Priority"].ToString();
                                    myRecord.submitter = dr["SubmittedBy"].ToString();
                                    myRecord.assigned = dr["AssignedTo"].ToString();
                                    myRecord.severity = dr["Severity"].ToString();
                                    myRecord.watching = new List<string>();
                                    // Add the first watcher to the list
                                    myRecord.watching.Add(dr["Watchers"].ToString());
                                }
                                else
                                {
                                    myRecord.watching.Add(dr["Watchers"].ToString());
                                   
                                }
                                prevTicket = dr["TicketID"].ToString();

                            }
                            records.Add(myRecord);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                logger.Error(e, "Error reading task file.");
            }
            return records;
        }


        public List<Records> ListTickets(string searchterm)
        {
            List<Records> records = new List<Records>();
            try
            {
                logger.Info("connecting to DB");
                // Connect to database
                using (SqlConnection conn = new SqlConnection(newTicketSystem.Properties.Settings.Default.BITSQL))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Select t.TicketID, t.TicketTypeID, t.Summary, t.Status, t.Priority, ut.FirstName + ' ' + ut.LastName as SubmittedBy, "
                        + "ua.FirstName + ' ' + ua.LastName as AssignedTo, ba.Severity, uw.FirstName + ' ' + uw.LastName as Watchers "
                        + "from dbo.Tickets t "
                        + "join dbo.BugAttributes ba on t.TicketID = ba.TicketID "
                        + "left join dbo.WatchingUsers wu on t.TicketID = wu.TicketID "
                        + "join dbo.Users uw on wu.UserID = uw.UserID "
                        + "join dbo.Users ut on t.Submitter = ut.UserID "
                        + "join dbo.Users ua on t.Assigned = ua.UserID "
                        + "where t.Summary like @search; ";
                        cmd.Parameters.AddWithValue("@search", "%" + searchterm + "%");

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            string prevTicket = "";
                            Ticket myRecord = null;
                            while (dr.Read())
                            {
                                if (prevTicket != dr["TicketID"].ToString())
                                {
                                    /*if (prevTicket != "")
                                    {
                                        records.Add(myRecord);
                                    }*/
                                    string type = dr["TicketTypeID"].ToString();
                                    if (type == "1")
                                    {
                                        myRecord = new Ticket();
                                    }
                                    myRecord.recordID = dr["TicketID"].ToString();
                                    myRecord.summary = dr["Summary"].ToString();
                                    myRecord.status = dr["Status"].ToString();
                                    myRecord.priority = dr["Priority"].ToString();
                                    myRecord.submitter = dr["SubmittedBy"].ToString();
                                    myRecord.assigned = dr["AssignedTo"].ToString();
                                    myRecord.severity = dr["Severity"].ToString();
                                    myRecord.watching = new List<string>();
                                    // Add the first watcher to the list
                                    myRecord.watching.Add(dr["Watchers"].ToString());
                                    /*
                                    if (myRecord.summary.ToUpper().Contains(search.ToUpper()))
                                    {*/
                                        records.Add(myRecord);
                                    /*}*/
                                }
                                else
                                {
                                    myRecord.watching.Add(dr["Watchers"].ToString());

                                }
                                prevTicket = dr["TicketID"].ToString(); //keeps the watchers in same record
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Error reading database.");
            }
            return records;
        }
        public bool AddTicket(Ticket ticket)
        {
            logger.Info("connecting to DB for defect add");
            try
            {
                using (SqlConnection conn = new SqlConnection(newTicketSystem.Properties.Settings.Default.BITSQL))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        string insertSQL = "declare @ticketid int " +
                                "begin transaction " +
                                "insert into tickets " +
                                "values(@summary, @status, @priority, @submitter,@assigned, @tickettypeid ) " +
                                "select @ticketid = scope_identity() " +
                                "insert into bugattributes " +
                                "values(@ticketid, @severity ) " +
                                "insert into watchingusers (UserID, TicketID)" +
                                "select UserID, @ticketid " +
                                "from Users " +
                                "where UserID in ({0}) "; // use a string placehold for formatting the string


                        cmd.Parameters.AddWithValue("@summary", ticket.summary);
                        cmd.Parameters.AddWithValue("@status", ticket.status);
                        cmd.Parameters.AddWithValue("@priority", ticket.priority);
                        cmd.Parameters.AddWithValue("@submitter", ticket.submitter);
                        cmd.Parameters.AddWithValue("@assigned", ticket.assigned);
                        cmd.Parameters.AddWithValue("@tickettypeid", 1);
                        cmd.Parameters.AddWithValue("@severity", ticket.severity);

                        var parms = new List<string>();
                            for (int i = 0; i < ticket.watching.Count; i++)
                            {
                                parms.Add(String.Format("@p{0}", i));
                            }
                            var inclause = string.Join(",", parms);
             
                            insertSQL = String.Format(insertSQL, inclause);
                            for (int i = 0; i < ticket.watching.Count; i++)
                            {
                            // add the unique parameter names with appropriate watching user values
                                cmd.Parameters.AddWithValue(parms[i], ticket.watching[i]);
                                // Console.WriteLine("watcherslist" + ticket.watching[i]);
                            }
                        insertSQL += "commit transaction ";
                        cmd.CommandText = insertSQL;
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;

            }
            catch (Exception e)
            {
                logger.Error(e, "Error writing to database");
                return false;
            }
        }

        public bool UpdateTicket(Records ticket)
        {

            try
            {
                logger.Info("connecting to DB for update");
                // Connect to database
                using (SqlConnection conn = new SqlConnection(newTicketSystem.Properties.Settings.Default.BITSQL))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        string updateSQL = "UPDATE tickets " +
                            "SET summary = @summary " +
                            "WHERE ticketid = @ticketid ";
                        cmd.Parameters.AddWithValue("@summary", ticket.summary);
                        cmd.Parameters.AddWithValue("@ticketid", ticket.recordID);
                        cmd.CommandText = updateSQL;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e, "Error writing to database for bugs");
                return false;
            }
           
        }
                internal IEnumerable<Records> ListEnhancements(string searchTerm)
        {
            throw new NotImplementedException();
        }

    }
}
