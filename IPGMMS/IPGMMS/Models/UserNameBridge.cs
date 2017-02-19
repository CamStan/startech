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

        [Required]
        [StringLength(256)]
        public string Member_UserName { get; set; }

        [Required]
        [StringLength(256)]
        public string AspNet_UserName { get; set; }
    }
}
