using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newTicketSystem
{
    class Ticket : Records
    {
        public string severity;

        public override string Display()
        {
            string output = "";

            output = String.Format("TicketID: {0}\nSummary: {1}\nStatus: {2}\nPriority: {3}\nSubmitted By: {4}\nAssigned To: {5}\n",
                recordID, summary, status, priority, submitter, assigned);
            //Console.WriteLine(watching.Count); // This is counting one less than there should be

            string watchingList = "";
            for (int i = 0; i < watching.Count; i++)
            {
                watchingList += watching[i] + ",";
            }
            if (watchingList.Length > 0)
            {
                watchingList = watchingList.Substring(0, watchingList.Length - 1);
            }

            output = output + "Watchers: " + watchingList;
            output += String.Format("\nSeverity: {0}", severity);

            return output;

        }

    }
}

