using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;

namespace newTicketSystem
{
    class TicketFile
    {
        public string filePath { get; set; }

        protected Logger logger { get { return _logger; } }
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public TicketFile(string path)
        {
            filePath = path;
        }
        public List<Records> ListTickets()
        {
            List<Records> records = new List<Records>();

            // read from the file
            StreamReader sr = null;
            try
            {
                logger.Info("Reading ticket File");
                sr = new StreamReader(filePath);

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    Ticket ticket = new Ticket();

                    if (line.IndexOf("\"") > -1)
                    {
                        // we have a comma in the title
                        int idx = -1;
                        idx = line.IndexOf(",");
                        ticket.recordID = line.Substring(0, idx);
                        line = line.Substring(idx + 1);
                        idx = line.IndexOf("\"", 1);
                        ticket.summary = line.Substring(1, idx - 1);
                        string third = line.Substring(idx + 2);
                        var values1 = third.Split(',');
                        ticket.status = values1[0];
                        ticket.priority = values1[1];
                        ticket.submitter = values1[2];
                        ticket.assigned = values1[3];
                        ticket.watching = values1[4].Split('|').ToList();
                        ticket.severity = values1[5];
                    }
                    else
                    {

                        var values = line.Split(',');
                        ticket.recordID = values[0];
                        ticket.summary = values[1];
                        ticket.status = values[2];
                        ticket.priority = values[3];
                        ticket.submitter = values[4];
                        ticket.assigned = values[5];
                        ticket.watching = values[6].Split('|').ToList();
                        ticket.severity = values[7];
                    }
                    records.Add(ticket);
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

        public bool AddTicket(Ticket ticket)
        {
            // write to file
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(filePath, true);

                string watchers = "";

                watchers = String.Join("|", ticket.watching);

                if (ticket.summary.IndexOf(",") > -1)
                {
                    sw.WriteLine("{0},\"{1}\",{2},{3},{4},{5},{6},{7}", ticket.recordID, ticket.summary, ticket.status, ticket.priority, ticket.submitter, ticket.assigned, watchers, ticket.severity);
                }
                else
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", ticket.recordID, ticket.summary, ticket.status, ticket.priority, ticket.submitter, ticket.assigned, watchers, ticket.severity);
                }

                return true;

            }
            catch (Exception e)
            {
                logger.Error(e, "Error writing to file");
                return false;
            }
            finally
            {
                sw.Dispose();
            }

        }

    }
}
