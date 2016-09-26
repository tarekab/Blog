namespace Blog.Web.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BlogModel : DbContext
    {
        public BlogModel()
            : base("name=BlogModelEntity")
        {
        }

        public virtual DbSet<Administrators> Administrators { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrators>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.Administrators)
                .HasForeignKey(e => e.AdministratorId);

            modelBuilder.Entity<Posts>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Posts)
                .HasForeignKey(e => e.PostID);

            modelBuilder.Entity<Posts>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("PostsTags").MapLeftKey("PostID").MapRightKey("TagID"));
        }
    }
}
