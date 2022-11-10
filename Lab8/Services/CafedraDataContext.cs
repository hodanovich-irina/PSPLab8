using Lab8.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Services
{
    internal class CafedraDataContext : DbContext
    {
        public static string ConnectionString;

        public CafedraDataContext()
        {
        }

        public DbSet<Cafedra> Cafedras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
