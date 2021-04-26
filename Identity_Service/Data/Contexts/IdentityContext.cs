using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class IdentityContext :
        IdentityDbContext<
            ApplicationUser,
            ApplicationRole,
            string,
            IdentityUserClaim<string>,
            ApplicationUserRole,
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>,
            IdentityUserToken<string>>
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
                entity.HasOne(p => p.User).WithMany(p => p.UserRoles).HasForeignKey(p => p.UserId);
                entity.HasOne(p => p.Role).WithMany(p => p.UserRoles).HasForeignKey(p => p.RoleId);
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasOne(current => current.User)
                .WithMany(current => current.RefreshTokens)
                .HasForeignKey(current => current.UserId)
                .IsRequired();
            });
        }
    }
}
