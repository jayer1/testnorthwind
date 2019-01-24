using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;

namespace newTicketSystem
{
    class EnhancementFile
    {
        public string enhancementsPath { get; set; }

        protected Logger logger { get { return _logger; } }
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public EnhancementFile(string path)
        {
            enhancementsPath = path;
        }
        public List<Records> ListEnhancements()
        {
            List<Records> records = new List<Records>();

            // read from the file
            StreamReader sr = null;
            try
            {
                logger.Info("Reading movie File");
                sr = new StreamReader(enhancementsPath);

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    Enhancement enhancement = new Enhancement();
                    
                    if (line.IndexOf("\"") > -1)
                    {
                        // we have a comma in the title
                        int idx = -1;
                        idx = line.IndexOf(",");
                        enhancement.recordID = line.Substring(0, idx);
                        //Console.WriteLine("record id: " + line.Substring(0, idx));
                        line = line.Substring(idx + 1);
                        idx = line.IndexOf("\"", 1);
                        enhancement.summary = line.Substring(1, idx - 1);
                        string third = line.Substring(idx + 2);
                        var values1 = third.Split(',');
                        enhancement.status = values1[0];
                        enhancement.priority = values1[1];
                        enhancement.submitter = values1[2];
                        enhancement.assigned = values1[3];
                        enhancement.watching = values1[4].Split('|').ToList();
                        enhancement.software = values1[5];
                        enhancement.cost = Convert.ToDouble(values1[6]);
                        enhancement.reason = values1[7];
                        enhancement.estimate = Convert.ToDouble(values1[8]);

                    }
                    else
                    {

                        var values = line.Split(',');
                        enhancement.recordID = values[0];
                        enhancement.summary = values[1];
                        enhancement.status = values[2];
                        enhancement.priority = values[3];
                        enhancement.submitter = values[4];
                        enhancement.assigned = values[5];
                        enhancement.watching = values[6].Split('|').ToList();
                        enhancement.software = values[7];
                        enhancement.cost = Convert.ToDouble(values[8]);
                        enhancement.reason = values[9];
                        enhancement.estimate = Convert.ToDouble(values[10]);
                    }

                    records.Add(enhancement);
                }
            }
            catch (IOException e)
            {
                logger.Error(e, "Error reading file.");
            }
            finally
            {
                sr.Dispose();
            }
            return records;
        }
        
        public bool AddEnhancement(Enhancement enhancement)
        {
                // write to file
                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(enhancementsPath, true);

                    string watchers = "";

                    watchers = String.Join("|", enhancement.watching);

                    if (enhancement.summary.IndexOf(",") > -1)
                    {
                        sw.WriteLine("{0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10}", enhancement.recordID, enhancement.summary, enhancement.status, enhancement.priority, enhancement.submitter, enhancement.assigned, watchers, enhancement.software, enhancement.cost, enhancement.reason, enhancement.estimate);
                    }
                    else
                    {
                        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", enhancement.recordID, enhancement.summary, enhancement.status, enhancement.priority, enhancement.submitter, enhancement.assigned, watchers, enhancement.software, enhancement.cost, enhancement.reason, enhancement.estimate);
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
