using EmployeeManagement.Identity;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EmployeeManagement.Dtos;

namespace EmployeeManagement.Data
{
    public class DataBaseDBContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataBaseDBContext(DbContextOptions<DataBaseDBContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Office> Offices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId).IsRequired();

                userRole.HasOne(ur => ur.User).WithMany(r => r.UserRoles)
                .HasForeignKey(u => u.UserId).IsRequired();
            });

            builder.Entity<Employee>()
            .HasOne(o => o.Office)
            .WithMany(e => e.Employees).HasForeignKey(k => k.OfficeId);
           
        }
    }
}
