using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Identity
{
    public class Role : IdentityRole<int>
    {
        public User? User { get; set; }
        public virtual List<UserRole>? UserRoles { get; set; }
    }
}
