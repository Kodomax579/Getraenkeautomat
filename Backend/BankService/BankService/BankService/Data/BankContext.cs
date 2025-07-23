using BankService.Model;
using Microsoft.EntityFrameworkCore;

namespace BankService.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }
        
        public DbSet<BankAccountModel> BankAccounts => Set<BankAccountModel>();
    }
}
