using Microsoft.EntityFrameworkCore;
using OpenService.Models;

namespace OpenService.Services.DbService
{
    public class SQLiteDBContext : DbContext
    {
        public DbSet<OrderRecord> OrderRecords { get; set; }
        private readonly IConfiguration _configuration;

        public SQLiteDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

}
