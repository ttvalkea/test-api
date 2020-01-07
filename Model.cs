using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Hero> Hero { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer("Data Source=tuomas-db-server.database.windows.net;Database=tuomas-test-db;Integrated Security=False;Persist Security Info=False;User ID=TODO:FROM-AZURE-KEY-VAULT;Password=TODO:FROM-AZURE-KEY-VAULT");
    }

    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}