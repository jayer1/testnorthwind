﻿using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class PcacommentType
    {
        public PcacommentType()
        {
            Pcacomment = new HashSet<Pcacomment>();
        }

        public int PcacommentTypeId { get; set; }
        public string PcacommentTypeName { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Pcacomment> Pcacomment { get; set; }
    }
}
