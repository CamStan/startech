namespace FakeNewsProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserKey")]
    public partial class UserKey
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int UKey { get; set; }

        public virtual User User { get; set; }
    }
}
