//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskAttribute
    {
        public int TaskAttrID { get; set; }
        public int TicketID { get; set; }
        public string ProjectName { get; set; }
        public string DueDate { get; set; }
    
        public virtual Ticket Ticket { get; set; }
    }
}
