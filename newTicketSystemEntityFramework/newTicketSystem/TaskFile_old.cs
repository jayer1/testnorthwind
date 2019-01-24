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
    class TaskFile_old
    {
        public string tasksPath { get; set; }

        protected Logger logger { get { return _logger; } }
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /*
        public TaskFile(string path)
        {
            tasksPath = path;
        }*/
        public List<Records_old> ListTasks()
        {
            List<Records_old> records = new List<Records_old>();

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
                        + "ua.FirstName + ' ' + ua.LastName as AssignedTo, uw.FirstName + ' ' + uw.LastName as Watchers, ta.ProjectName, ta.DueDate "
                        + "from dbo.Tickets t "
                        + "join dbo.TaskAttributes ta on t.TicketID = ta.TicketID "
                        + "left join dbo.WatchingUsers wu on t.TicketID = wu.TicketID "
                        + "join dbo.Users uw on wu.UserID = uw.UserID "
                        + "join dbo.Users u on wu.UserID = u.UserID "
                        + "join dbo.Users ut on t.Submitter = ut.UserID "
                        + "join dbo.Users ua on t.Assigned = ua.UserID ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            string prevTask = "";
                            Task_old myRecord = null;
                            while (dr.Read())
                            {
                                if (prevTask != dr["TicketID"].ToString())
                                {
                                    if (prevTask != "")
                                    {
                                        records.Add(myRecord);
                                    }
                                    string type = dr["TicketTypeID"].ToString();
                                    if (type == "3")
                                    {
                                        myRecord = new Task_old();
                                    }
                                    myRecord.recordID = dr["TicketID"].ToString();
                                    myRecord.summary = dr["Summary"].ToString();
                                    myRecord.status = dr["Status"].ToString();
                                    myRecord.priority = dr["Priority"].ToString();
                                    myRecord.submitter = dr["SubmittedBy"].ToString();
                                    myRecord.assigned = dr["AssignedTo"].ToString();
                                    myRecord.watching = new List<string>();
                                    myRecord.projectName = dr["ProjectName"].ToString();
                                    myRecord.dueDate = Convert.ToDateTime(dr["DueDate"]);
                                    // Add the first watcher to the list
                                    myRecord.watching.Add(dr["Watchers"].ToString());
                                }
                                else
                                {
                                    myRecord.watching.Add(dr["Watchers"].ToString());

                                }
                                prevTask = dr["TicketID"].ToString();

                            }
                            records.Add(myRecord);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                logger.Error(e, "Error reading tasks from database.");
            }
            return records;
        }

        public List<Records_old> ListTasks(string searchterm)
        {
            List<Records_old> records = new List<Records_old>();
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
                        + " ua.FirstName + ' ' + ua.LastName as AssignedTo, ta.ProjectName, ta.DueDate, uw.FirstName + ' ' + uw.LastName as Watchers  "
                        + " from dbo.Tickets t "
                        + " join dbo.TaskAttributes ta on t.TicketID = ta.TicketID "
                        + " left join dbo.WatchingUsers wu on t.TicketID = wu.TicketID "
                        + " join dbo.Users uw on wu.UserID = uw.UserID "
                        + " join dbo.Users u on wu.UserID = u.UserID "
                        + " join dbo.Users ut on t.Submitter = ut.UserID "
                        + " join dbo.Users ua on t.Assigned = ua.UserID "
                        + " where t.Summary like @search; ";
                        cmd.Parameters.AddWithValue("@search", "%" + searchterm + "%");
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            string prevTask = "";
                            Task_old myRecord = null;
                            while (dr.Read())
                            {
                                if (prevTask != dr["TicketID"].ToString())
                                {
                                    
                                    string type = dr["TicketTypeID"].ToString();
                                    if (type == "3")
                                    {
                                        myRecord = new Task_old();
                                    }
                                    myRecord.recordID = dr["TicketID"].ToString();
                                    myRecord.summary = dr["Summary"].ToString();
                                    myRecord.status = dr["Status"].ToString();
                                    myRecord.priority = dr["Priority"].ToString();
                                    myRecord.submitter = dr["SubmittedBy"].ToString();
                                    myRecord.assigned = dr["AssignedTo"].ToString();
                                    myRecord.watching = new List<string>();
                                    myRecord.projectName = dr["ProjectName"].ToString();
                                    myRecord.dueDate = Convert.ToDateTime(dr["DueDate"]);
                                    // Add the first watcher to the list
                                    myRecord.watching.Add(dr["Watchers"].ToString());
                                    records.Add(myRecord);
                                }
                                else
                                {
                                    myRecord.watching.Add(dr["Watchers"].ToString());

                                }
                                prevTask = dr["TicketID"].ToString();

                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Error reading tasks from database.");
            }

            return records;
        }

        public bool AddTask(Task_old newTask)
        { 
            logger.Info("connecting to DB for task add");
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
                                "insert into taskattributes " +
                                "values(@ticketid, @projectname, @duedate ) " +
                                "insert into watchingusers (UserID, TicketID)" +
                                "select UserID, @ticketid " +
                                "from Users " +
                                "where UserID in ({0}) "; // use a string placehold for formatting the string


                        cmd.Parameters.AddWithValue("@summary", newTask.summary);
                        cmd.Parameters.AddWithValue("@status", newTask.status);
                        cmd.Parameters.AddWithValue("@priority", newTask.priority);
                        cmd.Parameters.AddWithValue("@submitter", newTask.submitter);
                        cmd.Parameters.AddWithValue("@assigned", newTask.assigned);
                        cmd.Parameters.AddWithValue("@tickettypeid", 3);
                        cmd.Parameters.AddWithValue("@projectname", newTask.projectName);
                        cmd.Parameters.AddWithValue("@duedate", newTask.dueDate);

                        var parms = new List<string>();
                        for (int i = 0; i<newTask.watching.Count; i++)
                        {
                            parms.Add(String.Format("@p{0}", i));
                        }
                        var inclause = string.Join(",", parms);

                        insertSQL = String.Format(insertSQL, inclause);
                        for (int i = 0; i< newTask.watching.Count; i++)
                        {
                            // add the unique parameter names with appropriate watching user values
                            cmd.Parameters.AddWithValue(parms[i], newTask.watching[i]);
                            // Console.WriteLine("watcherslist" + newTask.watching[i]);
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

        public bool UpdateTask(Records_old ticket)
        {

            try
            {
                logger.Info("connecting to DB for task update");
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
                logger.Error(e, "Error writing to database for tasks");
                return false;
            }

        }
    }
    }
