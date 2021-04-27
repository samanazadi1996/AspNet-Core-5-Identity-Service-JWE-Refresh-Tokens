using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class IdentityContext :
        IdentityDbContext<
            ApplicationUser,
            ApplicationRole,
            string,
            ApplicationUserClaim,
            ApplicationUserRole,
            ApplicationUserLogin,
            ApplicationRoleClaim,
            ApplicationUserToken>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
                entity.Property(x => x.FirstName).HasMaxLength(30);
                entity.Property(x => x.LastName).HasMaxLength(30);
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            builder.Entity<ApplicationUserRole>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.HasOne(p => p.User).WithMany(p => p.UserRoles).HasForeignKey(p => p.UserId).IsRequired();
                entity.HasOne(p => p.Role).WithMany(p => p.UserRoles).HasForeignKey(p => p.RoleId).IsRequired();
            });

            builder.Entity<ApplicationUserClaim>(entity =>
            {
                entity.ToTable("UserClaims");
                entity.HasOne(p => p.User).WithMany(p => p.UserClaims).HasForeignKey(p => p.UserId).IsRequired();
            });

            builder.Entity<ApplicationUserLogin>(entity =>
            {
                entity.ToTable("UserLogins");
                entity.HasOne(p => p.User).WithMany(p => p.UserLogins).HasForeignKey(p => p.UserId).IsRequired();
            });

            builder.Entity<ApplicationRoleClaim>(entity =>
            {
                entity.ToTable("RoleClaims");
                entity.HasOne(p => p.Role).WithMany(p => p.RoleClaims).HasForeignKey(p => p.RoleId).IsRequired();
            });

            builder.Entity<ApplicationUserToken>(entity =>
            {
                entity.ToTable("UserTokens");
                entity.HasOne(p => p.User).WithMany(p => p.UserTokens).HasForeignKey(p => p.UserId).IsRequired();
            });

            builder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");
                entity.HasOne(current => current.User).WithMany(current => current.RefreshTokens).HasForeignKey(current => current.UserId).IsRequired();
            });
        }
    }
}
