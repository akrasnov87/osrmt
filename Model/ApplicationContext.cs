using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRTXamlToolkit.IO.Serialization;

namespace Osmrt.Model
{
    public class ApplicationContext : DbContext
    {
        private static string connectionString;
        public static void setConnectionString(string host, int port, string dbName, string login, string password)
        {
            connectionString = "Server=" + host + ";Port=" + port + ";Database=" + dbName + ";Username=" + login + ";Password=" + password;
        }

        public static string getConnectionLine()
        {
            return connectionString;
        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<ClientError> ClientErrors { get; set; }
    }
}
