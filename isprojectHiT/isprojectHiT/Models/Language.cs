﻿using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class Language
    {
        public Language()
        {
            PatientLanguage = new HashSet<PatientLanguage>();
        }

        public short LanguageId { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PatientLanguage> PatientLanguage { get; set; }
    }
}
