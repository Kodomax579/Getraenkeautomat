using Microsoft.EntityFrameworkCore;
using User.Models;

namespace User.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
        :base(options){ }

        public DbSet<UserModel> Users { get; set; }
    }
}
