using Dotnetvue.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnetvue.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FinanceRequest> FinanceRequests { get; set; }
    }
}