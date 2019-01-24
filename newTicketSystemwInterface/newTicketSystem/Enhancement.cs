using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newTicketSystem
{
    class Enhancement : Records
    {
        public string software;
        public string cost;
        public string reason;
        public string estimate;

        public override string Display()
        {
            string output = "";

            output = String.Format("EnhancementID: {0}\nSummary: {1}\nStatus: {2}\nPriority: {3}\nSubmitted By: {4}\nAssigned To: {5}\n", 
                recordID, summary, status, priority, submitter, assigned);
            string watchingList = "";

            watchingList = String.Join(",", watching);
            output = output + "Watchers: " + watchingList;
            output += String.Format("\nSoftware: {0}\nCost: {1}\nReason: {2}\nEstimate: {3}", software, cost, reason, estimate);
                
            return output;

        }

    }
}
