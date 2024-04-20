using CourseManagementAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAPI.Database
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }


    }
}
