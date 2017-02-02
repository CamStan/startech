namespace FakeNewsProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FakeNewsContext : DbContext
    {
        public FakeNewsContext()
            : base("name=FakeNewsContext")
        {
        }

        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Story>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<Story>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Stories)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
