﻿using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class Sex
    {
        public Sex()
        {
            Patient = new HashSet<Patient>();
        }

        public byte SexId { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
    }
}
