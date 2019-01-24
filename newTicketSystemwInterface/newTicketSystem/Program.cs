using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.IO;
using System.Globalization;
using System.Threading;

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

            TicketFile tickets = new TicketFile();
            EnhancementFile enhancements = new EnhancementFile();
            TaskFile tasks = new TaskFile();

            int selection = 0;
            string inputSelection = "";
            do
            {
                do // Check if input is 1 - 7
                {
                    Console.WriteLine("Defects");
                    Console.WriteLine("   1) Display Defects\n   2) Search for Defects\n   3) Add a Defect\n   4) Update A Defect");
                    Console.WriteLine("Enhancements");   
                    Console.WriteLine("   5) Display Enhancements\n   6) Search for Enhancements\n   7) Add a Enhancements\n   8) Update an Enhancement");
                    Console.WriteLine("Tasks");
                    Console.WriteLine("   9) Display Tasks\n   10) Search for Tasks\n   11) Add a Task\n   12) Update a Task\n   13) Exit");
                    inputSelection = Console.ReadLine();
                }
                while (inputSelection != "1" && inputSelection != "2" && inputSelection != "3" 
                && inputSelection != "4" && inputSelection != "5" && inputSelection != "6" 
                && inputSelection != "7" && inputSelection != "8" && inputSelection != "9"
                && inputSelection != "10" && inputSelection != "11" && inputSelection != "12"
                && inputSelection != "13");
                selection = Convert.ToInt32(inputSelection);

                if (selection == 1)
                {
                    // Display defects
                    logger.Trace("Select 1 Display Defects from File");
                    foreach (Records t in tickets.ListTickets())
                    {
                        Console.WriteLine(t.Display());
                        Console.WriteLine("");
                        t.DisplayID();
                    }
                }

                else if (selection == 2)
                {
                    // Search for Defects
                    logger.Trace("Select 2 Search for Defects");
                    Console.WriteLine("Enter search term for title search");
                    string searchTerm = Console.ReadLine();
                    foreach (Records t in tickets.ListTickets(searchTerm))
                    {
                        Console.WriteLine(t.Display());
                        t.DisplayID();
                    }
                }
                else if (selection == 3)
                {
                    //Add defects
                    logger.Trace("Select 3 Add Defects to File");

                    //Console.Write("Enter ticket id: ");
                    //string ticketID = Console.ReadLine();

                    Console.Write("Enter ticket summary: ");
                    string summary = Console.ReadLine();

                    string status = "";
                    do
                    {
                        Console.Write("Enter ticket status: ");
                        status = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                    } while (status != "New" && status != "Open" && status != "Assigned" && status != "Closed");

                    string priority = "";
                    do
                    {
                        Console.Write("Enter ticket priority: ");
                        priority = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                    } while (priority != "High" && priority != "Medium" && priority != "Low");

                    string submitting = "";
                    do
                    {
                        Console.WriteLine("For the next 3 questions, enter a number corresponding to these names: \n1) Faye Nevinski\n2) Jerry Gradisnik\n3) David Stern \n4) Mike Bolton\n5) Jay Bryce\n6) Hannah Smith \n7) Bob Jordan \n8) Debbie Boone \n9) Lawrence Millwood");
                        Console.Write("Who submitted this ticket? ");

                        submitting = Console.ReadLine();
                    } while (submitting != "1" && submitting != "2" && submitting != "3" && submitting != "4" && submitting != "5" && submitting != "6" && submitting != "7" && submitting != "8" && submitting != "9");

                    Console.Write("Who is the ticket assigned to? ");
                    string assigned = Console.ReadLine();

                    String inputWatching = "";
                    string watching = "";
                    do
                    {
                        Console.Write("Who is watching this ticket? (One or more people) Enter 'done' when done.");
                        inputWatching = Console.ReadLine();
                        if (inputWatching.ToUpper() != "DONE")
                        {
                            watching += inputWatching + "|";
                        }
                    } while (inputWatching.ToUpper() != "DONE");

                    if (watching.Length == 0)
                    {
                        watching = "";
                    }
                    else
                    {
                        watching = watching.Substring(0, watching.Length - 1);
                        //Console.WriteLine("watchers " + watching);
                    }
                    string severity = "";
                    do
                    {
                        Console.Write("Enter ticket severity: ");
                        severity = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                    } while (severity != "Urgent" && severity != "High" && severity != "Normal" && severity != "Low");


                    // Build the new ticket object
                    Ticket newTicket = new Ticket();
                    //newTicket.recordID = ticketID;  //To the left of the each is the vars from either Records or EnhancementFile, to the right is the ReadLine vars above
                    newTicket.summary = summary;
                    newTicket.status = status;
                    newTicket.priority = priority;
                    newTicket.submitter = submitting;
                    newTicket.assigned = assigned;
                    newTicket.watching = watching.Split('|').ToList();
                    newTicket.severity = severity;

                    // ADD THE new movie to the file.
                    try
                    {
                        tickets.AddTicket(newTicket);
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "Error adding enhancement");
                    }

                }
                else if (selection == 4)
                {
                    // Update Existing Defects
                    logger.Trace("Select 4 Update a Defect");
                    Console.WriteLine("Enter search term for ticket summary search");
                    string searchTerm = Console.ReadLine();
                    List<Records> updateList = tickets.ListTickets(searchTerm);
                    foreach (Records r in updateList)
                    {
                        //Console.WriteLine(r.Display());
                        r.DisplayID();
                    }
                    Console.WriteLine("Which ticket ID do you want to update?");
                    string ticketUpdate = Console.ReadLine();

                    // Find the first record in the list that matched the search with the record id the user provided
                    Records updateRecord = updateList.FirstOrDefault(r => r.recordID == ticketUpdate);

                    Console.WriteLine("Enter the new summary");
                    updateRecord.summary = Console.ReadLine();

                    tickets.UpdateTicket(updateRecord);
                }
                else if (selection == 5)
                {
                    // Display Enhancements
                    logger.Trace("Select 5 Display Enhancements from File");
                    foreach (Records r in enhancements.ListEnhancements())
                    {
                        Console.WriteLine(r.Display());
                        Console.WriteLine("");
                        r.DisplayID();
                    }

                }
                else if (selection == 6)
                {
                    logger.Trace("Select 6 Search for Enhancements");
                    Console.WriteLine("Enter search term for summary search");
                    string searchTerm = Console.ReadLine();
                    foreach (Records t in enhancements.ListEnhancements(searchTerm))
                    {
                        Console.WriteLine(t.Display());
                        t.DisplayID();
                    }
                }
                else if (selection == 7)
                {
                    // Add enhancements to file
                    logger.Trace("Select 7 Add Enhancements to File");
                    //Console.Write("Enter enhancement id: ");
                    //string ticketID = Console.ReadLine();

                    Console.Write("Enter enhancement summary: ");
                    string summary = Console.ReadLine();

                    string status = "";
                    do
                    {
                        Console.Write("Enter enhancement status: ");
                        status = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                    } while (status != "New" && status != "Open" && status != "Assigned" && status != "Closed");

                    string priority;
                    do
                    {
                        Console.Write("Enter enhancement priority: ");
                        priority = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                    } while (priority != "High" && priority != "Medium" && priority != "Low");

                    Console.WriteLine("For the next 3 questions, enter a number corresponding to these names: \n1) Faye Nevinski\n2) "
                        + "Jerry Gradisnik\n3) David Stern \n4) Mike Bolton\n5) Jay Bryce\n6) Hannah Smith \n7) Bob Jordan \n8) Debbie Boone \n9) Lawrence Millwood");
                    Console.Write("Who submitted this enhancement? ");
                    string submitting = Console.ReadLine();

                    Console.Write("Who is the enhancement assigned to? ");
                    string assigned = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());

                    String inputWatching = "";
                    string watching = "";
                    do
                    {
                        Console.Write("Who is watching this ticket? (One or more people) Enter 'done' when done.");
                        inputWatching = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                        if (inputWatching != "Done")
                        {
                            watching += inputWatching + "|";
                        }
                    } while (inputWatching != "Done");

                    if (watching.Length == 0)
                    {
                        watching = "";
                    }
                    else
                    {
                        watching = watching.Substring(0, watching.Length - 1);
                    }


                    Console.Write("What software does this pertain to? ");
                    string software = Console.ReadLine();

                    Console.Write("How much will this cost? ");
                    string cost = Console.ReadLine();

                    Console.Write("What's the reason for the enhancement? ");
                    string reason = Console.ReadLine();

                    Console.Write("What's the estimate? ");
                    string estimate = Console.ReadLine();


                    // Build the new movie object
                    Enhancement newEnhancement = new Enhancement();
                    //newEnhancement.recordID = ticketID;  //To the left of the each is the vars from either Records or EnhancementFile, to the right is the ReadLine vars above
                    newEnhancement.summary = summary;
                    newEnhancement.status = status;
                    newEnhancement.priority = priority;
                    newEnhancement.submitter = submitting;
                    newEnhancement.assigned = assigned;
                    newEnhancement.watching = watching.Split('|').ToList();
                    newEnhancement.software = software;
                    newEnhancement.cost = cost;
                    newEnhancement.reason = reason;
                    newEnhancement.estimate = estimate;

                    // ADD THE new movie to the file.
                    try
                    {
                        enhancements.AddEnhancement(newEnhancement);
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "Error adding enhancement");
                    }

                }
                else if (selection == 8)
                {
                    // Update Existing Enhancements
                    logger.Trace("Select 8 Update an Enhancement");
                    Console.WriteLine("Enter search term for enhancement summary search");
                    string searchTerm = Console.ReadLine();
                    List<Records> updateList = enhancements.ListEnhancements(searchTerm);
                    foreach (Records r in updateList)
                    {
                        //Console.WriteLine(r.Display());
                        r.DisplayID();
                    }
                    Console.WriteLine("Which enhancement ID do you want to update?");
                    string ticketUpdate = Console.ReadLine();

                    // Find the first record in the list that matched the search with the record id the user provided
                    Records updateRecord = updateList.FirstOrDefault(r => r.recordID == ticketUpdate);

                    Console.WriteLine("Enter the new summary");
                    updateRecord.summary = Console.ReadLine();

                    enhancements.UpdateEnhancement(updateRecord);
                }
                else if (selection == 9)
                {
                    // Display tasks from file
                    logger.Trace("Select 9 Display Tasks from File");
                    
                    foreach (Records t in tasks.ListTasks())
                    {
                        Console.WriteLine(t.Display());
                        Console.WriteLine("");
                        t.DisplayID();
                    }

                }
                else if (selection == 10)
                {
                    // Search for tasks
                    logger.Trace("Select 10 Search for Tasks");
                    Console.WriteLine("Enter search term for title search");
                    string searchTerm = Console.ReadLine();
                    foreach (Records t in tasks.ListTasks(searchTerm))
                    {
                        Console.WriteLine(t.Display());
                        Console.WriteLine("");
                        t.DisplayID();
                    }
                }
                else if (selection == 11)
                {
                    // Add tasks to file
                    logger.Trace("Select 11 Add Tasks to File");
                    //Console.Write("Enter task id: ");
                    //string ticketID = Console.ReadLine();

                    Console.Write("Enter task summary: ");
                    string summary = Console.ReadLine();

                    string status = "";
                    do
                    {
                        Console.Write("Enter task status: ");
                        status = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                    } while (status != "New" && status != "Open" && status != "Assigned" && status != "Closed");

                    string priority;
                    do
                    {
                        Console.Write("Enter task priority: ");
                        priority = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                    } while (priority != "High" && priority != "Medium" && priority != "Low");

                    Console.WriteLine("For the next 3 questions, enter a number corresponding to these names: \n1) Faye Nevinski\n2) "
                        + "Jerry Gradisnik\n3) David Stern \n4) Mike Bolton\n5) Jay Bryce\n6) Hannah Smith \n7) Bob Jordan \n8) Debbie Boone \n9) Lawrence Millwood");
                    Console.Write("Who submitted this task? ");
                    string submitting = status = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());

                    Console.Write("Who is the task assigned to? ");
                    string assigned = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());

                    String inputWatching = "";
                    string watching = "";
                    do
                    {
                        Console.Write("Who is watching this task? (One or more people) Enter 'done' when done.");
                        inputWatching = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                        if (inputWatching != "Done")
                        {
                            watching += inputWatching + "|";
                        }
                    } while (inputWatching != "Done");
                    if (watching.Length == 0)
                    {
                        watching = "";
                    }
                    else
                    {
                        watching = watching.Substring(0, watching.Length - 1);
                    }

                    Console.Write("What's the project name for this task? ");
                    string projectName = Console.ReadLine();

                    bool dateSuccess = false;
                    int dateRetry = 0;
                    string getDueDate = "";
                    DateTime dueDate = new DateTime();

                    while (!dateSuccess && dateRetry < 3)
                    {
                        try
                        {

                            Console.Write("What's the due date?");
                            dueDate = Convert.ToDateTime(Console.ReadLine());
                            dateSuccess = true;

                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Cannot convert the due date you entered to a date format" + dueDate);
                            logger.Warn(e, "Cannot convert to a date");
                            dateRetry++;
                        }
                    }

                    // Build the new task object
                    Task newTask = new Task();
                    //newTask.recordID = ticketID;  //To the left of the each is the vars from either Records or EnhancementFile, to the right is the ReadLine vars above
                    newTask.summary = summary;
                    newTask.status = status;
                    newTask.priority = priority;
                    newTask.submitter = submitting;
                    newTask.assigned = assigned;
                    newTask.watching = watching.Split('|').ToList();
                    newTask.projectName = projectName;
                    newTask.dueDate = dueDate;

                    // ADD THE new task to the database
                    try
                    {
                        tasks.AddTask(newTask);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("File Load Exception: '{0}'", e);
                        logger.Error(e, "Error adding task");
                    }
                }
                else if (selection == 12)
                {
                    // Update Existing Tasks
                    logger.Trace("Select 12 Update a Task");
                    Console.WriteLine("Enter search term for task summary search");
                    string searchTerm = Console.ReadLine();
                    List<Records> updateList = tasks.ListTasks(searchTerm);
                    foreach (Records r in updateList)
                    {
                        //Console.WriteLine(r.Display());
                        r.DisplayID();
                    }
                    Console.WriteLine("Which task ID do you want to update?");
                    string ticketUpdate = Console.ReadLine();

                    // Find the first record in the list that matched the search with the record id the user provided
                    Records updateRecord = updateList.FirstOrDefault(r => r.recordID == ticketUpdate);

                    Console.WriteLine("Enter the new summary");
                    updateRecord.summary = Console.ReadLine();

                    tasks.UpdateTask(updateRecord);
                }
            } while (selection != 13);
        }
    }
}

