namespace FakeNewsProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StoryTag")]
    public partial class StoryTag
    {
        public int ID { get; set; }

        public int StoryID { get; set; }

        public int TagID { get; set; }

        public virtual Story Story { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
