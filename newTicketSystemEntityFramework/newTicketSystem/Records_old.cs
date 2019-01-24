using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newTicketSystem
{
    abstract class Records_old
    {
        public string recordID;
        public string summary;
        public string status;
        public string priority;
        public string submitter;
        public string assigned;
        public List<string> watching;

        public void DisplayID()
        {
            Console.WriteLine("Record ID value: {0}", recordID);
        }
        public abstract string Display();

    }
}
