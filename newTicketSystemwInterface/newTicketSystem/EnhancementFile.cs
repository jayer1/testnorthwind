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
    class EnhancementFile
    {
        public string enhancementsPath { get; set; }

        protected Logger logger { get { return _logger; } }
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        public List<Records> ListEnhancements()
        {
            List<Records> records = new List<Records>();

            // read from the file
            // StreamReader sr = null;
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
                        + " ua.FirstName + ' ' + ua.LastName as AssignedTo, ea.Software, ea.Cost, ea.Reason, ea.Estimate, uw.FirstName + ' ' + uw.LastName as Watchers  "
                        + " from dbo.Tickets t "
                        + " join dbo.EnhancementAttributes ea on t.TicketID = ea.TicketID "
                        + " left join dbo.WatchingUsers wu on t.TicketID = wu.TicketID "
                        + " join dbo.Users uw on wu.UserID = uw.UserID "
                        + " join dbo.Users u on wu.UserID = u.UserID "
                        + " join dbo.Users ut on t.Submitter = ut.UserID "
                        + " join dbo.Users ua on t.Assigned = ua.UserID ";

                         using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            string prevEnhancement = "";
                            Enhancement myRecord = null;
                            while (dr.Read())
                            {
                                if (prevEnhancement != dr["TicketID"].ToString())
                                {
                                    if (prevEnhancement != "")
                                    {
                                        records.Add(myRecord);
                                    }
                                    string type = dr["TicketTypeID"].ToString();
                                    if (type == "2")
                                    {
                                        myRecord = new Enhancement();
                                    }
                                    myRecord.recordID = dr["TicketID"].ToString();
                                    myRecord.summary = dr["Summary"].ToString();
                                    myRecord.status = dr["Status"].ToString();
                                    myRecord.priority = dr["Priority"].ToString();
                                    myRecord.submitter = dr["SubmittedBy"].ToString();
                                    myRecord.assigned = dr["AssignedTo"].ToString();
                                    myRecord.watching = new List<string>();
                                    myRecord.software = dr["Software"].ToString();
                                    myRecord.cost = dr["Cost"].ToString();
                                    myRecord.reason = dr["Reason"].ToString();
                                    myRecord.estimate = dr["Estimate"].ToString();
                                    // Add the first watcher to the list
                                    myRecord.watching.Add(dr["Watchers"].ToString());
                                }
                                else
                                {
                                    myRecord.watching.Add(dr["Watchers"].ToString());

                                }
                                prevEnhancement = dr["TicketID"].ToString();

                            }
                            records.Add(myRecord);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                logger.Error(e, "Error reading enhancements from database.");
                Console.WriteLine("File Load Exception: '{0}'", e);
            }
            
            return records;
        }

        public List<Records> ListEnhancements(string searchterm)
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
                        + " ua.FirstName + ' ' + ua.LastName as AssignedTo, ea.Software, ea.Cost, ea.Reason, ea.Estimate, uw.FirstName + ' ' + uw.LastName as Watchers  "
                        + " from dbo.Tickets t "
                        + " join dbo.EnhancementAttributes ea on t.TicketID = ea.TicketID "
                        + " left join dbo.WatchingUsers wu on t.TicketID = wu.TicketID "
                        + " join dbo.Users uw on wu.UserID = uw.UserID "
                        + " join dbo.Users u on wu.UserID = u.UserID "
                        + " join dbo.Users ut on t.Submitter = ut.UserID "
                        + " join dbo.Users ua on t.Assigned = ua.UserID "
                        + " where t.Summary like @search; ";
                        cmd.Parameters.AddWithValue("@search", "%" + searchterm + "%");
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            string prevEnhancement = "";
                            Enhancement myRecord = null;
                            while (dr.Read())
                            {
                                if (prevEnhancement != dr["TicketID"].ToString())
                                {
                                    /*if (prevEnhancement != "")
                                    {
                                        records.Add(myRecord);
                                    }*/
                                    string type = dr["TicketTypeID"].ToString();
                                    if (type == "2")
                                    {
                                        myRecord = new Enhancement();
                                    }
                                    myRecord.recordID = dr["TicketID"].ToString();
                                    myRecord.summary = dr["Summary"].ToString();
                                    myRecord.status = dr["Status"].ToString();
                                    myRecord.priority = dr["Priority"].ToString();
                                    myRecord.submitter = dr["SubmittedBy"].ToString();
                                    myRecord.assigned = dr["AssignedTo"].ToString();
                                    myRecord.watching = new List<string>();
                                    myRecord.software = dr["Software"].ToString();
                                    myRecord.cost = dr["Cost"].ToString();
                                    myRecord.reason = dr["Reason"].ToString();
                                    myRecord.estimate = dr["Estimate"].ToString();
                                    // Add the first watcher to the list
                                    myRecord.watching.Add(dr["Watchers"].ToString());
                                    records.Add(myRecord);
                                
                                }
                                else
                                {
                                    myRecord.watching.Add(dr["Watchers"].ToString());

                                }
                                prevEnhancement = dr["TicketID"].ToString();

                            }
                           
                        }
                    }
                }
            }
            catch (IOException e)
            {
                logger.Error(e, "Error reading enhancements from database.");
            }

            return records;
        }



        public bool AddEnhancement(Enhancement enhancement)
        {
            logger.Info("connecting to DB for enhancement add");
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
                                "insert into enhancementattributes " +
                                "values(@ticketid, @software, @cost, @reason, @estimate ) " +
                                "insert into watchingusers (UserID, TicketID)" +
                                "select UserID, @ticketid " +
                                "from Users " +
                                "where UserID in ({0}) "; // use a string placehold for formatting the string


                        cmd.Parameters.AddWithValue("@summary", enhancement.summary);
                        cmd.Parameters.AddWithValue("@status", enhancement.status);
                        cmd.Parameters.AddWithValue("@priority", enhancement.priority);
                        cmd.Parameters.AddWithValue("@submitter", enhancement.submitter);
                        cmd.Parameters.AddWithValue("@assigned", enhancement.assigned);
                        cmd.Parameters.AddWithValue("@tickettypeid", 2);
                        cmd.Parameters.AddWithValue("@software", enhancement.software);
                        cmd.Parameters.AddWithValue("@cost", enhancement.cost);
                        cmd.Parameters.AddWithValue("@reason", enhancement.reason);
                        cmd.Parameters.AddWithValue("@estimate", enhancement.estimate);

                        var parms = new List<string>();
                        for (int i = 0; i < enhancement.watching.Count; i++)
                        {
                            parms.Add(String.Format("@p{0}", i));
                        }
                        var inclause = string.Join(",", parms);

                        insertSQL = String.Format(insertSQL, inclause);
                        for (int i = 0; i < enhancement.watching.Count; i++)
                        {
                            // add the unique parameter names with appropriate watching user values
                            cmd.Parameters.AddWithValue(parms[i], enhancement.watching[i]);
                            //Console.WriteLine("watcherslist" + enhancement.watching[i]);
                        }
                        insertSQL += "commit transaction ";
                        cmd.CommandText = insertSQL;
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;

            }
            catch (IOException e)
                {
                    logger.Error(e, "Error writing enhancement to database");
                    return false;
                }
        }


        public bool UpdateEnhancement(Records ticket)
        {

            try
            {
                logger.Info("connecting to DB for enhancement update");
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
                logger.Error(e, "Error writing to database for enhancement");
                return false;
            }

        }
    }
}
