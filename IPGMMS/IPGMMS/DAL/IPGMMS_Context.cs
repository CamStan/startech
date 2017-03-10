namespace IPGMMS.DAL
{
    using IPGMMS.Models;
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class IPGMMS_Context : DbContext
    {
        public IPGMMS_Context()
            : base("name=IPGMMS_Context")
        {
        }

        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<ContactInfo> ContactInfoes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<MemberCertification> MemberCertifications { get; set; }
        public virtual DbSet<MemberLevel> MemberLevels { get; set; }
        public virtual DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificate>()
                .HasMany(e => e.MemberCertifications)
                .WithRequired(e => e.Certificate)
                .HasForeignKey(e => e.Certificate_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContactInfo>()
                .HasMany(e => e.Contacts)
                .WithRequired(e => e.ContactInfo)
                .HasForeignKey(e => new { e.ContactInfo_ID, e.Member_ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContactType>()
                .HasMany(e => e.Contacts)
                .WithRequired(e => e.ContactType)
                .HasForeignKey(e => e.ContactType_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MemberLevel>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.MemberLevel1)
                .HasForeignKey(e => e.MemberLevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Contacts)
                .WithRequired(e => e.Member)
                .HasForeignKey(e => e.Member_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.MemberCertifications)
                .WithRequired(e => e.Member)
                .HasForeignKey(e => e.Member_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
