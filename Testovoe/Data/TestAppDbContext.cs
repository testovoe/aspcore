using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Testovoe.Model;

namespace Testovoe.Data
{
    public class TestAppDbContext : DbContext
    {
        
        public TestAppDbContext(DbContextOptions<TestAppDbContext> options) : base(options)
        {

        }

        public DbSet<UserTime> UserTimes { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory)  //tie-up DbContext with LoggerFactory object
            .EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTime>().Property(x => x.DateRegistration).IsRequired();
        }
    }
}
