using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DbSet<DbTheory> DbTheories { get; set; }

        public DbSet<DbQuestion> DbQuestions { get; set; }
        
        public DbSet<DbAnswer> DbAnswers { get; set; }

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
    }

    public class DbTheory
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string FilePath { get; set; }
    }

    public class DbQuestion
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string QuestionText { get; set; }

        /// <summary>
        /// Связь один ко многим
        /// </summary>
        public List<DbAnswer> DbAnswers { get; set; } = new List<DbAnswer>();              

        /// <summary>
        /// https://metanit.com/sharp/entityframeworkcore/3.1.php
        /// </summary>   
    }

    public class DbAnswer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int DbQuestionId { get; set; }

        public bool IsCorrect { get; set; }

        public DbQuestion DbQuestion { get; set; }
    }
}
