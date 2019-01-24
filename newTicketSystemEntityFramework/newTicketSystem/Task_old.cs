using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newTicketSystem
{
    class Task_old : Records_old
    {
        public string projectName;
        public DateTime dueDate;

        public override string Display()
        {
            string output = "";

            output = String.Format("TicketID: {0}\nSummary: {1}\nStatus: {2}\nPriority: {3}\nSubmitted By: {4}\nAssigned To: {5}\n",
                recordID, summary, status, priority, submitter, assigned);
            string watchingList = "";

            watchingList = String.Join(",", watching);
            output = output + "Watchers: " + watchingList;

            string convertDueDate = dueDate.ToString("MM/dd/yyyy");

            output += String.Format("\nProject Name: {0}\nDue Date: {1}", projectName, convertDueDate);

            return output;
        }
    }
}
