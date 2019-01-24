using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;

namespace newTicketSystem
{
    class TaskFile
    {
        public string tasksPath { get; set; }

        protected Logger logger { get { return _logger; } }
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public TaskFile(string path)
        {
            tasksPath = path;
        }
        public List<Records> ListTasks()
        {
            List<Records> records = new List<Records>();

            // read from the file
            StreamReader sr = null;
            try
            {
                logger.Info("Reading task File");
                sr = new StreamReader(tasksPath);

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    Task task = new Task();

                    if (line.IndexOf("\"") > -1)
                    {
                        // we have a comma in the title
                        int idx = -1;
                        idx = line.IndexOf(",");
                        task.recordID = line.Substring(0, idx);
                        line = line.Substring(idx + 1);
                        idx = line.IndexOf("\"", 1);
                        task.summary = line.Substring(1, idx - 1);
                        string third = line.Substring(idx + 2);
                        var values1 = third.Split(',');
                        task.status = values1[0];
                        task.priority = values1[1];
                        task.submitter = values1[2];
                        task.assigned = values1[3];
                        task.watching = values1[4].Split('|').ToList();
                        task.projectName = values1[5];
                        task.dueDate = DateTime.Parse(values1[6]);
                    }
                    else
                    {

                        var values = line.Split(',');
                        task.recordID = values[0];
                        task.summary = values[1];
                        task.status = values[2];
                        task.priority = values[3];
                        task.submitter = values[4];
                        task.assigned = values[5];
                        task.watching = values[6].Split('|').ToList();
                        task.projectName = values[7];
                        task.dueDate = DateTime.Parse(values[8]);
                    }
                        records.Add(task);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Error reading task file.");
            }
            finally
            {
                sr.Dispose();
            }
            return records;
        }

        public bool AddTask(Task newTask)
        {
            Console.WriteLine("In AddTask method");
            Console.WriteLine(newTask.priority);
            // write to file
            StreamWriter sWriter = null;
            try
            {
                sWriter = new StreamWriter(tasksPath, true);
                Console.WriteLine("tasksPath = {0} ", tasksPath);
                string watchers = "";

                watchers = String.Join("|", newTask.watching);

                if (newTask.summary.IndexOf(",") > -1)
                {
                    sWriter.WriteLine("{0},\"{1}\",{2},{3},{4},{5},{6},{7},{8}", newTask.recordID, newTask.summary, newTask.status, newTask.priority, newTask.submitter, newTask.assigned, watchers, newTask.projectName, newTask.dueDate);

                }
                else
                {
                    sWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}", newTask.recordID, newTask.summary, newTask.status, newTask.priority, newTask.submitter, newTask.assigned, watchers, newTask.projectName, newTask.dueDate);
                }
                return true;

            }
            catch (IOException e)
            {
                Console.WriteLine("File Load Exception: '{0}'", e);
                logger.Error(e, "Error writing to file");
                return false;
            }
            finally
            {
                sWriter.Dispose();
            }

        }
    }
}
