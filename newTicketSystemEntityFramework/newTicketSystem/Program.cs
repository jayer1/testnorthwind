using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Data.Entity;

namespace newTicketSystem
{
    class Program : IFiles
    {
        string IFiles.filePath { get; set; }
        string IFiles.enhancementsPath { get; set; }
        string IFiles.tasksPath { get; set; }

        public static string filePath = "tickets.csv";
        public static string enhancementsPath = "enhancements.csv";
        public static string tasksPath = "tasks.csv";

        static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Trace("The program has started.");




            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            TicketFile_old tickets = new TicketFile_old();
            EnhancementFile_old enhancements = new EnhancementFile_old();
            TaskFile_old tasks = new TaskFile_old();

            int selection = 0;
            string inputSelection = "";
            do
            {
                do // Check if input is 1 - 7
                {
                    Console.WriteLine("Main Menu");
                    Console.WriteLine("   1) Display List of Tickets\n   2) Search for Tickets\n   3) Add a Ticket\n   4) Update Tickets");
                    Console.WriteLine("Maintenance");
                    Console.WriteLine("   5) Add Users\n   6) List Users\n   7) Exit");
                    inputSelection = Console.ReadLine();

                    if (inputSelection != "1" && inputSelection != "2" && inputSelection != "3"
                        && inputSelection != "4" && inputSelection != "5" && inputSelection != "6"
                        && inputSelection != "7")
                    {
                        logger.Warn("Entered invalid selection on main menu, loop caught it.");
                    }
                }
                while (inputSelection != "1" && inputSelection != "2" && inputSelection != "3" 
                && inputSelection != "4" && inputSelection != "5" && inputSelection != "6"
                && inputSelection != "7");
                selection = Convert.ToInt32(inputSelection);

                if (selection == 1)
                {
                    // Display tickets
                    logger.Trace("Select 1 Display All Tickets");
                    using (var db = new EFTicketing())
                    {
                        try
                        {
                            logger.Info("Reading all tickets from database");

                            var results = db.Tickets
                                .Include(ba => ba.BugAttributes)
                                .Include(ea => ea.EnhancementAttributes)
                                .Include(ta => ta.TaskAttributes)
                                .Include(wu => wu.WatchingUsers)
                                .Include(tt => tt.TicketType)
                                .Select(m => new
                                {
                                    m.TicketID,
                                    m.Summary,
                                    m.Priority,
                                    m.Status,
                                    m.BugAttributes,
                                    m.EnhancementAttributes,
                                    m.TaskAttributes,
                                    m.SubmittedUser,
                                    m.AssignedUser,
                                    m.WatchingUsers,
                                    m.TicketType
                                });

                            foreach (var record in results)
                            {
                                Console.WriteLine("TicketID: {0}\nTicketType: {1}\nSummary {2}\nPriority {3}\nStatus {4}", record.TicketID, record.TicketType.Description, record.Summary, record.Priority, record.Status);
                                var submittedby = record.SubmittedUser;
                                var assignee = record.AssignedUser;


                                Console.WriteLine("Submitter: {0} ", submittedby.FirstName + " " + submittedby.LastName);
                                Console.WriteLine("Assignee: {0} ", assignee.FirstName + " " + assignee.LastName);

                                // Add appropriate Attribute records if they exist for display
                                Console.WriteLine("Watchers:");
                                foreach (var user in record.WatchingUsers.Select(g => g.User))
                                {
                                    Console.WriteLine(user.FirstName + " " + user.LastName);
                                }

                                // Count for Users watching the ticket
                                Console.WriteLine("Number of watchers: {0}", record.WatchingUsers.Count());

                                if (record.TicketType.Description == "Bug")
                                {
                                    Console.WriteLine("Severity: {0} ", record.BugAttributes.Select(ba => ba.Severity).SingleOrDefault());
                                }

                                if (record.TicketType.Description == "Enhancement")
                                {
                                    Console.WriteLine("Software: {0}\nCost: {1}\nReason: {2}\nEstimate: {3}",
                                    record.EnhancementAttributes.Select(ea => ea.Software).SingleOrDefault(),
                                    record.EnhancementAttributes.Select(ea => ea.Cost).SingleOrDefault(),
                                    record.EnhancementAttributes.Select(ea => ea.Reason).SingleOrDefault(),
                                    record.EnhancementAttributes.Select(ea => ea.Estimate).SingleOrDefault());
                                }

                                if (record.TicketType.Description == "Task")
                                {
                                    Console.WriteLine("Project Name: {0}\nDue Date: {1}",
                                    record.TaskAttributes.Select(ea => ea.ProjectName).SingleOrDefault(),
                                    record.TaskAttributes.Select(ea => ea.DueDate).SingleOrDefault());
                                }
                                Console.WriteLine("");
                            }
                        }
                        catch (Exception e)
                        {
                            logger.Error(e, "Error reading data from database");
                        }
                    }
                        }
                else if (selection == 2)
                {
                    using (var db = new EFTicketing())
                    {
                        // Search for Tickets
                        logger.Trace("Select 2 Search for All Types of Tickets");
                        Console.WriteLine("Enter search term for summary search");
                        string searchTerm = Console.ReadLine();
                        ///var results = db.Tickets.Where(m => m.Summary.Contains(searchTerm) && m.TicketTypeID.Equals(1));
                        ///

                        //var results = db.Tickets.Where(m => m.Summary.Contains(searchTerm));

                        try
                        {
                            var results = db.Tickets
                                .Include(ba => ba.BugAttributes)
                                .Include(ea => ea.EnhancementAttributes)
                                .Include(ta => ta.TaskAttributes)
                                .Include(wu => wu.WatchingUsers)
                                .Include(tt => tt.TicketType)
                                .Where(m => m.Summary.Contains(searchTerm))
                                .Select(m => new
                                {
                                    m.TicketID,
                                    m.Summary,
                                    m.Priority,
                                    m.Status,
                                    m.BugAttributes,
                                    m.EnhancementAttributes,
                                    m.TaskAttributes,
                                    m.SubmittedUser,
                                    m.AssignedUser,
                                    m.WatchingUsers,
                                    m.TicketType
                                });

                            foreach (var record in results)
                            {
                                Console.WriteLine("TicketID: {0}\nTicket Type: {1}\nSummary {2}\nPriority {3}\nStatus {4}", record.TicketID, record.TicketType.Description, record.Summary, record.Priority, record.Status);
                                var submittedby = record.SubmittedUser;
                                var assignee = record.AssignedUser;


                                Console.WriteLine("Submitter: {0} ", submittedby.FirstName + " " + submittedby.LastName);
                                Console.WriteLine("Assignee: {0} ", assignee.FirstName + " " + assignee.LastName);

                                // Add appropriate Attribute records if they exist for display
                                Console.WriteLine("Watchers:");
                                foreach (var user in record.WatchingUsers.Select(g => g.User))
                                {
                                    Console.WriteLine(user.FirstName + " " + user.LastName);
                                }

                                // Count for Users watching the ticket
                                Console.WriteLine("Number of watchers: {0}", record.WatchingUsers.Count());

                                if (record.TicketType.Description == "Bug")
                                {
                                    Console.WriteLine("Severity: {0} ", record.BugAttributes.Select(ba => ba.Severity).SingleOrDefault());
                                }

                                if (record.TicketType.Description == "Enhancement")
                                {
                                    Console.WriteLine("Software: {0}\nCost: {1}\nReason: {2}\nEstimate: {3}",
                                    record.EnhancementAttributes.Select(ea => ea.Software).SingleOrDefault(),
                                    record.EnhancementAttributes.Select(ea => ea.Cost).SingleOrDefault(),
                                    record.EnhancementAttributes.Select(ea => ea.Reason).SingleOrDefault(),
                                    record.EnhancementAttributes.Select(ea => ea.Estimate).SingleOrDefault());
                                }

                                if (record.TicketType.Description == "Task")
                                {
                                    Console.WriteLine("Project Name: {0}\nDue Date: {1}",
                                    record.TaskAttributes.Select(ea => ea.ProjectName).SingleOrDefault(),
                                    record.TaskAttributes.Select(ea => ea.DueDate).SingleOrDefault());
                                }
                                Console.WriteLine("");
                            }
                        }
                        catch (Exception e)
                        {
                            logger.Error(e, "Error reading file.");
                        }
                    }

                }
                else if (selection == 3)
                {
                    //Add ticket
                    logger.Trace("Select 3 Add Ticket");

                    using (var db = new EFTicketing())
                    {
                        Console.WriteLine("What type of media item to add? (1- Bug, 2- Enhancement, 3- Task)");
                        int type = Convert.ToInt32(Console.ReadLine());
                        
                        var mType = db.TicketTypes.Where(tt => tt.TicketTypeID == type).SingleOrDefault();

                        if (mType != null)
                        {

                        
                        Console.Write("Enter ticket summary: ");
                        string summary = Console.ReadLine();

                        string status = "";
                        do
                        {
                        Console.Write("Enter ticket status (Valid values are New, Open, Assigned or Closed): ");
                        status = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                                if (status != "New" && status != "Open" && status != "Assigned" && status != "Closed")
                                {
                                    logger.Warn("Entered invalid selection on status field, loop caught it.");
                                }
                            } while (status != "New" && status != "Open" && status != "Assigned" && status != "Closed");

                        string priority = "";
                        do
                        {
                            Console.Write("Enter ticket priority (Valid values are High, Medium, Low): ");
                            priority = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                                if (priority != "High" && priority != "Medium" && priority != "Low")
                                {
                                    logger.Warn("Entered invalid selection on priority field, loop caught it.");
                                }
                            } while (priority != "High" && priority != "Medium" && priority != "Low");

                        int submitting = 0;
                        Console.WriteLine("For the next 2 questions, enter a user id number corresponding to these names below:");
                            foreach (var u in db.Users)
                            {
                                Console.WriteLine("UserID: {0} Name: {1} {2} Department: {3}", u.UserID, u.FirstName, u.LastName, u.Department);
                            }
                            Console.Write("Who submitted this ticket? ");

                        submitting = Convert.ToInt32(Console.ReadLine());


                        Console.Write("Who is the ticket assigned to? ");
                        int assigned = Convert.ToInt32(Console.ReadLine());

                            Ticket newRecord = new Ticket();

                            newRecord.Summary = summary;
                            newRecord.Priority = priority;
                            newRecord.Status = status;
                            newRecord.Submitter = submitting;
                            newRecord.Assigned = assigned;
                            newRecord.TicketType = mType;

                            

                            if (mType.Description == "Bug")
                            {
                                BugAttribute ba = new BugAttribute();
                                string severity = "";
                                do
                                {
                                    Console.Write("Enter ticket severity (Valid values include Urgent, High, Medium or Low): ");
                                    severity = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                                    if (severity != "Urgent" && severity != "High" && severity != "Medium" && severity != "Low")
                                    {
                                        logger.Warn("Entered invalid selection on severity field, loop caught it.");
                                    }
                                } while (severity != "Urgent" && severity != "High" && severity != "Medium" && severity != "Low");

                                ba.Severity = severity;
                                newRecord.BugAttributes.Add(ba);
                            }

                            if (mType.Description == "Enhancement")
                            {
                                EnhancementAttribute ea = new EnhancementAttribute();
                                string inputSoftware = "";
                                string inputCost = "";
                                string inputReason = "";
                                string inputEstimate = "";

                                Console.Write("Enter software involved in this enhancement: ");
                                inputSoftware = Console.ReadLine();

                                Console.Write("Enter the cost of this enhancement: ");
                                inputCost = Console.ReadLine();

                                Console.Write("Enter the reason for this enhancement: ");
                                inputReason = Console.ReadLine();

                                Console.Write("Enter the estimate for this enhancement: ");
                                inputEstimate = Console.ReadLine();

                                ea.Software = inputSoftware;
                                ea.Cost = inputCost;
                                ea.Reason = inputReason;
                                ea.Estimate = inputEstimate;
                                newRecord.EnhancementAttributes.Add(ea);
                            }

                            if (mType.Description == "Task")
                            {
                                TaskAttribute ta = new TaskAttribute();
                                string inputProjectName = "";
                                string inputDueDate = "";

                                Console.Write("Enter a project name for in this task: ");
                                inputProjectName = Console.ReadLine();

                                Console.Write("Enter the due date of this task: ");
                                inputDueDate = Console.ReadLine();


                                ta.ProjectName = inputProjectName;
                                ta.DueDate = inputDueDate;
                                newRecord.TaskAttributes.Add(ta);
                            }

                            int inputWatchingUserID = 0;
                            //moved to here
                            foreach (var u in db.Users)
                            {
                                Console.WriteLine("UserID: {0} Name: {1} {2} Department: {3}", u.UserID, u.FirstName, u.LastName, u.Department);
                            }
                            do
                        {
                                // moved from here
                                Console.Write("Enter id(s) of who is watching this ticket. (One or more id) Enter '-99' when done. Above is a list of current users:");
                                inputWatchingUserID = Convert.ToInt32(Console.ReadLine());
                            
                             if (inputWatchingUserID != -99)
                            {
                                    // Check the database for a user object that matches
                                    var wuUserID = db.Users.Where(w => w.UserID == inputWatchingUserID).SingleOrDefault();
                                    if (wuUserID != null) 
                                    {
                                        WatchingUser watchers = new WatchingUser();
                                        watchers.UserID = inputWatchingUserID;
                                        newRecord.WatchingUsers.Add(watchers);
                                    }
                            }
                        } while (inputWatchingUserID != -99);

                            db.Tickets.Add(newRecord);
                            try
                            {
                                logger.Trace("Attempting to add the record to the database");
                                db.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                logger.Error(e, "Error saving data to the database.");
                            }
                    }
                    }

                    
                    
                }
                else if (selection == 4)
                {

                    // update existing record
                    logger.Trace("Updating record");
                    Console.WriteLine("Enter search term for summary search");
                    string searchTerm = Console.ReadLine();

                    using (var db = new EFTicketing())
                    {
                        logger.Trace("Starting the Update Record option");
                        //var updateMyRecord = db.Tickets.Where(m => m.Summary == searchTerm.Trim()).SingleOrDefault();
                        try
                        {
                            logger.Trace("Querying database for search term in the Update Record option");

                            var updateMyRecord = db.Tickets.Where(m => m.Summary.Contains(searchTerm)).SingleOrDefault();
                            if (updateMyRecord != null)
                            {


                                Console.WriteLine("Enter the new summary for the record ({0})", updateMyRecord.Summary);
                                updateMyRecord.Summary = Console.ReadLine();

                                do {
                                    Console.WriteLine("Enter the new priority for the record (Valid values are High, Medium, Low): (Current value: {0})", updateMyRecord.Priority);
                                    updateMyRecord.Priority = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                                    if (updateMyRecord.Priority != "High" && updateMyRecord.Priority != "Medium" && updateMyRecord.Priority != "Low")
                                    {
                                        logger.Warn("Entered invalid selection on priority field when updating record, loop caught it.");
                                    }

                                } while (updateMyRecord.Priority != "High" && updateMyRecord.Priority != "Medium" && updateMyRecord.Priority != "Low");

                                do
                                {
                                    Console.WriteLine("Enter ticket status (Valid values are New, Open, Assigned or Closed): (Current value: {0})", updateMyRecord.Status);
                                    updateMyRecord.Status = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                                    if (updateMyRecord.Status != "New" && updateMyRecord.Status != "Open" && updateMyRecord.Status != "Assigned" && updateMyRecord.Status != "Closed")
                                    {
                                        logger.Warn("Entered invalid selection on status field when updating record, loop caught it.");
                                    }

                                } while (updateMyRecord.Status != "New" && updateMyRecord.Status != "Open" && updateMyRecord.Status != "Assigned" && updateMyRecord.Status != "Closed");


                                foreach (var u in db.Users)
                                {
                                    Console.WriteLine("UserID: {0} Name: {1} {2} Department: {3}", u.UserID, u.FirstName, u.LastName, u.Department);

                                }
                                Console.WriteLine("Enter the UserID from above for the submitter, assigned to and watchers");
                                Console.WriteLine("Enter the new submitted by user ID for the record ({0})", updateMyRecord.Submitter);
                                updateMyRecord.Submitter = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Enter the new assigned to user ID for the record ({0})", updateMyRecord.Assigned);
                                updateMyRecord.Assigned = Convert.ToInt32(Console.ReadLine());

                                if (updateMyRecord.TicketType.Description == "Bug")
                                {
                                    // Update the bug attributes
                                    BugAttribute updateBA = updateMyRecord.BugAttributes.Single();

                                    do
                                    {
                                        Console.WriteLine("Enter ticket severity (Valid values include Urgent, High, Medium or Low): (Current value: {0})", updateBA.Severity);
                                        updateBA.Severity = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                                        if (updateBA.Severity != "Urgent" && updateBA.Severity != "High" && updateBA.Severity != "Medium" && updateBA.Severity != "Low")
                                        {
                                            logger.Warn("Entered invalid selection on severity field when updating record, loop caught it.");
                                        }

                                    } while (updateBA.Severity != "Urgent" && updateBA.Severity != "High" && updateBA.Severity != "Medium" && updateBA.Severity != "Low");

                                }

                                if (updateMyRecord.TicketType.Description == "Enhancement")
                                {
                                    // Update the movie attributes
                                    EnhancementAttribute updateEA = updateMyRecord.EnhancementAttributes.Single();

                                    Console.WriteLine("Update the Software ({0})", updateEA.Software);
                                    updateEA.Software = Console.ReadLine();

                                    Console.WriteLine("Update the Cost ({0})", updateEA.Cost);
                                    updateEA.Cost = Console.ReadLine();

                                    Console.WriteLine("Update the Reason ({0})", updateEA.Reason);
                                    updateEA.Reason = Console.ReadLine();

                                    Console.WriteLine("Update the Estimate ({0})", updateEA.Estimate);
                                    updateEA.Estimate = Console.ReadLine();

                                }

                                if (updateMyRecord.TicketType.Description == "Task")
                                {
                                    // Update the movie attributes
                                    TaskAttribute updateTA = updateMyRecord.TaskAttributes.Single();

                                    Console.WriteLine("Update the Project Name ({0})", updateTA.ProjectName);
                                    updateTA.ProjectName = Console.ReadLine();

                                    Console.WriteLine("Update the Due Date ({0})", updateTA.DueDate);
                                    updateTA.DueDate = Console.ReadLine();
                                }

                                var users = db.WatchingUsers.Where(wu => wu.TicketID == updateMyRecord.TicketID).ToList();

                                // For each watchinguser row - remove it from the DB Context
                                users.ForEach(u => db.WatchingUsers.Remove(u));

                                int addWatchingUser = 0;
                                do
                                {
                                    Console.WriteLine("Enter a watcher - when finished enter '-99'");
                                    addWatchingUser = Convert.ToInt32(Console.ReadLine());

                                    if (addWatchingUser != -99)
                                    {
                                        var user = db.Users.Where(u => u.UserID == addWatchingUser).FirstOrDefault();

                                        if (user != null)
                                        {
                                            WatchingUser wu = new WatchingUser();
                                            wu.User = user;
                                            updateMyRecord.WatchingUsers.Add(wu);
                                        }

                                    }
                                } while (addWatchingUser != -99);

                                // Update the record
                                try
                                {
                                    logger.Trace("Attempting to save data to database in Update Record option");
                                    db.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    logger.Error(e, "Error saving data to database in Update Record option.");
                                }

                            }
                        }
                        catch (Exception e)
                        {
                            logger.Error(e, "Error quering database for search term in Update function.");
                        }
                    }

                }
                else if (selection == 5)
                {
                    // Allow adding of a new user to the database
                    // Get a  from the user
                    logger.Trace("Add a User menu option selected");
                    Console.WriteLine("Enter the First Name of the new user");
                    string newUserFirstName = Console.ReadLine();

                    Console.WriteLine("Enter the Last Name of the new user");
                    string newUserLastName = Console.ReadLine();

                    string newUserDept = "";
                    while (newUserDept != "IT" && newUserDept != "HR" && newUserDept != "Purchasing")
                    {
                        Console.WriteLine("Enter the Department of the new user. (Valid depts are IT, HR and Purchasing)");
                        newUserDept = Console.ReadLine();
                        if(newUserDept != "IT" && newUserDept != "HR" && newUserDept != "Purchasing")
                        {
                            logger.Warn("Wrong department was entered when adding user, while loop caught it");
                        }
                    }
                    using (var db = new EFTicketing())
                    {
                        // Check if User exists or not
                        // loop through users and compare

                        var validate = db.Users.Where(g => g.FirstName.Trim() == newUserFirstName.Trim()).Count();
                        var validate2 = db.Users.Where(g => g.LastName.Trim() == newUserLastName.Trim()).Count();

                        Console.WriteLine("First Name Count: {0}\nLast Name Count: {1}", validate, validate2);
                        logger.Debug("Validate user " + newUserFirstName + " and found: " + validate.ToString());
                        logger.Debug("Validate user " + newUserLastName + " and found: " + validate2.ToString());

                        if (validate == 0 || validate2 == 0) // if entered first name AND last name do not match a record...
                        {
                            // Add to the Users table
                            User newU = new User();

                            newU.FirstName = newUserFirstName;
                            newU.LastName = newUserLastName;
                            newU.Department = newUserDept;
                            newU.Enabled = 1;

                            db.Users.Add(newU);
                            try
                            {
                                logger.Trace("Attempting to save user to database");
                                db.SaveChanges();
                                Console.WriteLine("User added: {0} {1}", newUserFirstName, newUserLastName);
                            }
                            catch (Exception e)
                            {
                                logger.Error(e, "Error saving User record to the database.");
                            }
                            

                        }
                        else
                        {
                            Console.WriteLine("{0} {1} user already exists", newUserFirstName, newUserLastName);
                            logger.Warn("Entered user that already exists when adding user");
                        }
                    }
                }
                else if (selection == 6)
                {
                    logger.Trace("List Users");
                    using (var db = new EFTicketing())
                    {
                        logger.Trace("Attempting to list users from database");
                        try
                        {
                            var results = db.Users
                                .Include(m => m.Tickets)
                                .Select(u => new {
                                    u.UserID,
                                    u.FirstName,
                                    u.LastName,
                                    u.Department,
                                    u.Tickets
      
                                });



                            foreach (var result in results)
                            {
                                Console.WriteLine("UserID: {0} Name: {1} {2} Department: {3} Ticket(s) Assigned: {4}", result.UserID, result.FirstName, result.LastName, result.Department, result.Tickets.Count);
                                //Console.WriteLine("");
                            }
                        }
                        catch (Exception e)
                        {
                            logger.Error(e, "Error reading User data from the database.");
                        }

                    }
                }
                } while (selection != 7);
        }
    }
}

