using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice4
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<DbUser> DbUsers { get; set; }

        public ApplicationContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ApplicationContext>(modelBuilder, true);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }

    public class DbUser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DbUser()
        {
        }
    }

    public class DbTheory
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string FilePath { get; set; }

        public DbTheory()
        {
        }
    }

    public class DbQuestion
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Header { get; set; }

        public List<string> AnswerVariants { get; set; }

        public DbQuestion()
        {
            AnswerVariants = new List<string>();
        }
    }
}
