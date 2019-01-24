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
    class Program
    {

        static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Trace("The program has started.");

            string filePath = "tickets.csv";
            string enhancementsPath = "enhancements.csv";
            string tasksPath = "tasks.csv";


            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            TicketFile tickets = new TicketFile(filePath);
            EnhancementFile enhancements = new EnhancementFile(enhancementsPath);
            TaskFile tasks = new TaskFile(tasksPath);

            int selection = 0;
            string inputSelection = "";
            do
            {
                do // Check if input is 1 - 7
                {
                    Console.WriteLine("1) Display Defects\n2) Add Defects");
                    Console.WriteLine("3) Display Enhancements\n4) Add Enhancements");
                    Console.WriteLine("5) Display Tasks\n6) Add Tasks");
                    Console.WriteLine("7) Exit");

                    inputSelection = Console.ReadLine();
                }
                while (inputSelection != "1" && inputSelection != "2" && inputSelection != "3" && inputSelection != "4" && inputSelection != "5" && inputSelection != "6" && inputSelection != "7");
                selection = Convert.ToInt32(inputSelection);

                if (selection == 1)
                {
                    // Display enhancements
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
                    logger.Trace("Select 2 Add Tickets to File");
                    string finished = "n";
                    //String combinedRecord = "";
                    //String filePath = "tickets.csv";

                    while (finished != "y")
                    {
                        Console.Write("Enter ticket id: ");
                        string ticketID = Console.ReadLine();

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

                        Console.Write("Who submitted this ticket? ");
                        string submitting = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());

                        Console.Write("Who is the ticket assigned to? ");
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
                            watching = "Noone is watching";
                        }
                        else
                        {
                            watching = watching.Substring(0, watching.Length - 1);
                        }
                            string severity = "";
                        do
                        {
                            Console.Write("Enter ticket severity: ");
                            severity = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                        } while (severity != "Urgent" && severity != "High" && severity != "Normal" && severity != "Low");


                        // Build the new ticket object
                        Ticket newTicket = new Ticket();
                        newTicket.recordID = ticketID;  //To the left of the each is the vars from either Records or EnhancementFile, to the right is the ReadLine vars above
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

                        Console.Write("Are you finished?");
                        finished = Console.ReadLine();

                    } while (finished != "y") ;
                }
                else if (selection == 3)
                {
                    // Display enhancements
                    logger.Trace("Select 3 Add Tickets to File");
                    foreach (Records r in enhancements.ListEnhancements())
                    {
                        Console.WriteLine(r.Display());
                        Console.WriteLine("");
                        r.DisplayID();
                    }

                }
                else if (selection == 4)
                {
                    logger.Trace("Select 4 Add Enhancements to File");
                    Console.Write("Enter enhancement id: ");
                    string ticketID = Console.ReadLine();

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
                        watching = "Noone is watching";
                    }
                    else
                    {
                        watching = watching.Substring(0, watching.Length - 1);
                    }


                    Console.Write("What software does this pertain to? ");
                    string software = Console.ReadLine();

                    Console.Write("How much will this cost?");
                    double cost = Convert.ToDouble(Console.ReadLine());

                    Console.Write("What's the reason for the enhancement?");
                    string reason = Console.ReadLine();

                    Console.Write("What's the estimate?");
                    double estimate = Convert.ToDouble(Console.ReadLine());


                    // Build the new movie object
                    Enhancement newEnhancement = new Enhancement();
                    newEnhancement.recordID = ticketID;  //To the left of the each is the vars from either Records or EnhancementFile, to the right is the ReadLine vars above
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
                else if (selection == 5)
                {
                    logger.Trace("Select 5 Display Tasks from File");
                    // Display tasks
                    foreach (Records t in tasks.ListTasks())
                    {
                        Console.WriteLine(t.Display());
                        Console.WriteLine("");
                        t.DisplayID();
                    }

                }
                else if (selection == 6)
                {
                    logger.Trace("Select 6 Add Tasks to File");
                    Console.Write("Enter task id: ");
                    string ticketID = Console.ReadLine();

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
                        watching = "Noone is watching";
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
                            logger.Error(e, "Cannot convert to a date");
                            dateRetry++;
                        }
                    }
                



                // Build the new task object
                Task newTask = new Task();
                    newTask.recordID = ticketID;  //To the left of the each is the vars from either Records or EnhancementFile, to the right is the ReadLine vars above
                    newTask.summary = summary;
                    newTask.status = status;
                    newTask.priority = priority;
                    newTask.submitter = submitting;
                    newTask.assigned = assigned;
                    newTask.watching = watching.Split('|').ToList();
                    newTask.projectName = projectName;
                    newTask.dueDate = dueDate;

                    // ADD THE new task to the file.
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
            } while (selection != 7);

        }


        /*public class Tickett
        {
            public string tickettID { get; set; }
            public string tickettSummary { get; set; }
            public string tickettStatus { get; set; }
            public string tickettPriority { get; set; }
            public string tickettSubmitting { get; set; }
            public string tickettAssigned { get; set; }
            public string tickettWatching { get; set; }

            public string filePath { get; set; }
            protected Logger logger { get { return _logger; } }
            private static Logger _logger = LogManager.GetCurrentClassLogger();


            public static string TicketEntry(Tickett myNewTicket)
            {
                String finished = "n";
                //String combinedRecord = "";
                //String filePath = "tickets.csv";

                while (finished != "y")
                {
                    Console.Write("Enter ticket id: ");
                    myNewTicket.tickettID = Console.ReadLine();

                    Console.Write("Enter ticket summary: ");
                    myNewTicket.tickettSummary = Console.ReadLine();

                    do
                    {
                        Console.Write("Enter ticket status: ");
                        myNewTicket.tickettStatus = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                    } while (myNewTicket.tickettStatus != "New" && myNewTicket.tickettStatus != "Open" && myNewTicket.tickettStatus != "Assigned" && myNewTicket.tickettStatus != "Closed");

                    do
                    {
                        Console.Write("Enter ticket priority: ");
                        myNewTicket.tickettPriority = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                    } while (myNewTicket.tickettPriority != "High" && myNewTicket.tickettPriority != "Medium" && myNewTicket.tickettPriority != "Low");

                    Console.Write("Who submitted this ticket? ");
                    myNewTicket.tickettSubmitting = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());

                    Console.Write("Who is the ticket assigned to? ");
                    myNewTicket.tickettAssigned = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());

                    String inputWatching = "";
                    myNewTicket.tickettWatching = "";
                    do
                    {
                        Console.Write("Who is watching this ticket? (One or more people) Enter 'done' when done.");
                        inputWatching = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
                        if (inputWatching != "Done")
                        {
                            myNewTicket.tickettWatching += inputWatching + "|";
                        }
                    } while (inputWatching != "Done");
                    myNewTicket.tickettWatching = myNewTicket.tickettWatching.Substring(0, myNewTicket.tickettWatching.Length - 1);

                    Console.WriteLine(myNewTicket.tickettID + "\n" + myNewTicket.tickettSummary + "\n" + myNewTicket.tickettStatus + "\n" + myNewTicket.tickettPriority + "\n" + myNewTicket.tickettSubmitting + "\n" + myNewTicket.tickettAssigned + "\n" + myNewTicket.tickettWatching);

                    combinedRecord = myNewTicket.tickettID + "," + myNewTicket.tickettSummary + "," + myNewTicket.tickettStatus + "," + myNewTicket.tickettPriority + "," + myNewTicket.tickettSubmitting + "," + myNewTicket.tickettAssigned + "," + myNewTicket.tickettWatching;

                    bool result = false;
                    result = Tickett.TicketSave(myNewTicket);

                    Console.Write("Are you finished?");
                    finished = Console.ReadLine();

                } while (finished != "y") ;
                return combinedRecord;
            }

            public static bool TicketSave(Tickett myNewTicket)
            {
                // Start of writing file
                bool writeSuccess = false;
                int retryWrite = 0;
                String filePath = "tickets.csv";

                while (!writeSuccess && retryWrite < 3)
                {
                    try
                    {
                        StreamWriter JasonsFileStorage = new StreamWriter(filePath, true);
                        JasonsFileStorage.WriteLine("{0},{1},{2},{3},{4},{5},{6}", myNewTicket.tickettID, myNewTicket.tickettSummary, myNewTicket.tickettStatus, myNewTicket.tickettPriority, myNewTicket.tickettSubmitting, myNewTicket.tickettAssigned, myNewTicket.tickettWatching);
                        JasonsFileStorage.Close();
                        writeSuccess = true;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("File Load Exception: '{0}'", e);
                        _logger.Error("File Load Exception: {0}", e);
                        retryWrite++;
                        Console.WriteLine("Please make sure the {0} file is closed then hit Enter", filePath);
                        Console.ReadLine();
                    }
                }
                return writeSuccess;
            }
        }
            public class Listfile
            {
            protected Logger logger { get { return _logger; } }
            private static Logger _logger = LogManager.GetCurrentClassLogger();

            public static void ListTicketsFile()
                {
                    String filePath = "tickets.csv";
                    if (File.Exists(filePath))
                    {
                    bool writeSuccess = false;
                    int retryWrite = 0;
                    StreamReader sr = null;
                    while (!writeSuccess && retryWrite < 3)
                    {
                        try
                        {
                            _logger.Info("Reading movie File");
                            sr = new StreamReader(filePath);

                            // Read lines from file until no more lines to read
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] rowSplitByComma = line.Split(','); // split the line read at the double quote marks
                                string ticketId = rowSplitByComma[0]; // take the text inside the 2 double quote marks
                                string ticketSummary = rowSplitByComma[1];
                                string ticketStatus = rowSplitByComma[2];
                                string ticketPriority = rowSplitByComma[3];
                                string ticketSubmitter = rowSplitByComma[4];
                                string ticketAssigned = rowSplitByComma[5];
                                string ticketWatching = rowSplitByComma[6];
                                string ticketWatchingFormatted = ticketWatching.Replace("|", ", ");

                                Console.WriteLine("ID: " + ticketId);
                                Console.WriteLine("Summary: " + ticketSummary);
                                Console.WriteLine("Status: " + ticketStatus);
                                Console.WriteLine("Priority: " + ticketPriority);
                                Console.WriteLine("Submitter: " + ticketSubmitter);
                                Console.WriteLine("Assigned: " + ticketAssigned);
                                Console.WriteLine("Watching: " + ticketWatchingFormatted);
                                Console.WriteLine("");
                            }
                            writeSuccess = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Please make sure the {0} file is closed then hit Enter", filePath);
                            _logger.Error(e, "Error reading file.");
                            retryWrite++;
                        }
                        finally
                        {
                            sr.Dispose();
                        }
                    }
                    }
                    else
                    {
                        Console.WriteLine("File does not exist");
                    }
                    Console.ReadLine();
                }*/
    }
}

