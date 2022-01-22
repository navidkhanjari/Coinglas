using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Article;

using Academy.Domain.Entities.Course;
using Academy.Domain.Entities.Order;
using Academy.Domain.Entities.Permissions;
using Academy.Domain.Entities.Subscribe;
using Academy.Domain.Entities.Wallet;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Academy.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }

        #endregion

        #region Wallet
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        #endregion

        #region Permissions
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        #endregion

        #region Article
        public DbSet<Article> Articles { get; set; }
        #endregion

        #region Courses
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }

        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        #endregion

        #region Subscribe
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<UserSubscribes> UserSubscribes { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
              .SelectMany(t => t.GetForeignKeys())
              .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            // Query Filter 
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Article>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<CourseGroup>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Course>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Course>()

            .HasOne<CourseGroup>(f => f.CourseGroup)
            .WithMany(g => g.Courses)
            .HasForeignKey(f => f.GroupId);

            modelBuilder.Entity<Course>()

               .HasOne<CourseGroup>(f => f.Group)
                .WithMany(g => g.SubGroup)
               .HasForeignKey(f => f.SubGroup);

            base.OnModelCreating(modelBuilder);





        }
    }
}
