using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newTicketSystem
{
    interface IFiles
    {
        string filePath { get; set; }
        string enhancementsPath { get; set; }
        string tasksPath { get; set; }
    }
}
