namespace IPGMMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Contacts = new HashSet<Contact>();
            MemberCertifications = new HashSet<MemberCertification>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Membership_Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Membership_SignupDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Membership_ExpirationDate { get; set; }

        public int MemberLevel { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(256)]
        public string BusinessName { get; set; }

        [StringLength(256)]
        public string Website { get; set; }

        [StringLength(128)]
        public string Identity_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contact> Contacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberCertification> MemberCertifications { get; set; }

        public virtual MemberLevel MemberLevel1 { get; set; }
    }
}
