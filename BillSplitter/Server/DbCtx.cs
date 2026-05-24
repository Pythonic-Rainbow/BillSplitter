using BillSplitter.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Server
{
    public class DbCtx(DbContextOptions<DbCtx> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
    }

}
