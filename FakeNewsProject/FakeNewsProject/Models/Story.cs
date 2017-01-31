namespace FakeNewsProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Story")]
    public partial class Story
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Body { get; set; }

        [Column(TypeName = "text")]
        public string Summary { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PostDate { get; set; }

        public virtual User User { get; set; }
    }
}
