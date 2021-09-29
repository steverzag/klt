using FloraYFaunaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FloraYFaunaAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<Carousel> Carousel { get; set; }
        public virtual DbSet<BlogPost> BlogPosts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<IpPost> IpPosts { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }

        public int SaveChanges(TokenUser user)
        {
            if (user != null)
            {
                var entries = this.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).Where(
                        x => x.Entity.GetType().GetProperties().Select(y => y.PropertyType).ToList()
                            .Contains(typeof(Metadata)));

                foreach (var entityEntry in entries)
                {

                    var metadataPropertyInfo = entityEntry.Entity.GetType().GetProperty("Metadata", typeof(Metadata));
                    var metadata = (Metadata) metadataPropertyInfo.GetValue(entityEntry.Entity, null);

                    metadata.UpdatedBy = user.UserId;
                    if (entityEntry.State == EntityState.Added) metadata.CreatedBy = user.UserId;
                }
            }

            return this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().HasOne(b => b.Category).WithMany(c => c.BlogPost);
            modelBuilder.Entity<IpPost>().HasOne(i => i.BlogPost).WithMany(b => b.IpPost);
            modelBuilder.Entity<RefreshToken>().HasOne(r => r.User).WithMany(u => u.RefreshToken);

            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Newsletter>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
