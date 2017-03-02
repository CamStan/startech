namespace IPGMMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserNameBridge
    {
        public int ID { get; set; }

        public int Member_ID { get; set; }

        [Required]
        [StringLength(128)]
        public string AspNet_ID { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Member Member { get; set; }
    }
}
