namespace IPGMMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MemberCertification
    {
        public int ID { get; set; }

        public int Member_ID { get; set; }

        public int Certificate_ID { get; set; }

        public virtual Certificate Certificate { get; set; }

        public virtual Member Member { get; set; }
    }
}
