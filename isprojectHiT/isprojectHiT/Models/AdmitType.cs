﻿using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class AdmitType
    {
        public AdmitType()
        {
            Encounter = new HashSet<Encounter>();
        }

        public int AdmitTypeId { get; set; }
        public string WiPopCode { get; set; }
        public string Description { get; set; }
        public string Explaination { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Encounter> Encounter { get; set; }
    }
}
