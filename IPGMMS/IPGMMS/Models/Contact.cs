namespace IPGMMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact
    {
        public int ID { get; set; }

        public int Member_ID { get; set; }

        public int ContactInfo_ID { get; set; }

        public int ContactType_ID { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual Member Member { get; set; }
    }
}
