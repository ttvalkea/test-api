using Microsoft.EntityFrameworkCore;

namespace TestApi
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Hero> Hero { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
                        
            //Use username and password that have been fetched from Azure Key Vault
            options.UseSqlServer("Data Source=tuomas-db-server.database.windows.net;Database=tuomas-test-db;Integrated Security=False;Persist Security Info=False;User ID=" + Secrets.DbUserName + "; Password=" + Secrets.DbUserPassword);
        }
    }

    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}