using SQLite.CodeFirst;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

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

        public void Load()
        {
            string path = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.Split('=')[1];

            if (!File.Exists(path))
            {
                SaveChanges();
                Theory();
                SaveChanges();
            }
        }

        public void Theory()
        {
            DirectoryInfo dirTheory = new DirectoryInfo("Theory");
            List<string> descriptions = new List<string>()
            {
                "Блокчейн – выстроенная по определённым правилам непрерывная последовательная цепочка блоков (связный список), содержащих информацию.",
                "Существует три общих класса современной криптографии на основе программного обеспечения: хэш-функции, симметричное шифрование и асимметричное шифрование.",
                "Основополагащие элементы блокчейна: децентрализация, узлы, консенсус, смарт-контракты и оракулы.",
                "Контейнером для информации выступает прозрачная структура - блок, который проверяется на достоверность. Блоки соединяются в цепь."
            };

            for (int i = 0; i < dirTheory.GetFiles().Length; i++)
            {
                DbTheories.Add(new DbTheory()
                {
                    Topic = System.IO.Path.GetFileNameWithoutExtension(dirTheory.GetFiles()[i].ToString().Remove(0, 1)),
                    Description = descriptions[i],
                    FilePath = $"Theory\\{dirTheory.GetFiles()[i].Name}"
                });
            }
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

        public string Description { get; set; }

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
