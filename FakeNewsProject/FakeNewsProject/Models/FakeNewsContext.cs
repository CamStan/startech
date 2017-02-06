namespace FakeNewsProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FakeNewsContext : DbContext
    {
        public FakeNewsContext()
            : base("name=FakeNewsContextRemote")
        {
        }

        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<StoryTag> StoryTags { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserKey> UserKeys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Story>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<Story>()
                .Property(e => e.Summary)
                .IsUnicode(false);

            modelBuilder.Entity<Story>()
                .HasMany(e => e.Favorites)
                .WithRequired(e => e.Story)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Story>()
                .HasMany(e => e.StoryTags)
                .WithRequired(e => e.Story)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.StoryTags)
                .WithRequired(e => e.Tag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Favorites)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Stories)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserKeys)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
