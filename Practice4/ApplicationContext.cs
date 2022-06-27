using SQLite.CodeFirst;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace Practice4
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DbUser> DbUsers { get; set; }

        public DbSet<DbTheory> DbTheories { get; set; }

        public DbSet<DbQuestion> DbQuestions { get; set; }
        
        public DbSet<DbAnswer> DbAnswers { get; set; }
        
        public DbSet<DbTest> DbTests { get; set; }

        public DbSet<DbStatistic> DbStatistics { get; set; }


        private static readonly string DbPath = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.Split('=')[1];

        public ApplicationContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (!File.Exists(DbPath))
            {
                var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ApplicationContext>(modelBuilder);
                Database.SetInitializer(sqliteConnectionInitializer);
            }
        }

        public void Load()
        {
            if (!File.Exists(DbPath))
            {
                SaveChanges();
                AddTheory();
                SaveChanges();
                AddTests();
                SaveChanges();
            }
        }

        private void AddTheory()
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
                    Topic = Path.GetFileNameWithoutExtension(dirTheory.GetFiles()[i].ToString().Remove(0, 1)),
                    Description = descriptions[i],
                    FilePath = dirTheory.GetFiles()[i].FullName
                });
            }
        }

        private void AddTests()
        {
            DirectoryInfo dirTest = new DirectoryInfo("Tests");

            foreach (FileInfo test in dirTest.GetFiles())
            {
                DbTest dbTest = new DbTest();
                List<string> questionBlock = File.ReadAllText(test.FullName)
                    .Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                if (int.TryParse(test.Name.Split('.')[0], out int id))
                {                
                    dbTest.DbTheories.Add(DbTheories.FirstOrDefault(x => x.Id == id));
                }

                for (int i = 0; i < questionBlock.Count; i++)
                {
                    List<string> linesInBlock = questionBlock[i]
                        .Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                    
                    DbQuestion q = new DbQuestion()
                    {
                        Type = linesInBlock[0][0].ToString(),
                        QuestionText = linesInBlock[0].Remove(0, 1),
                    };

                    DbQuestions.Add(q);
                    dbTest.Questions.Add(q);

                    foreach (string line in linesInBlock.Skip(1))
                    {
                        DbAnswer a = new DbAnswer()
                        {
                            IsCorrect = line.Last() == '!',
                            Text = line.Replace("!", ""),
                            DbQuestion = q
                        };
                        DbAnswers.Add(a); 
                    }
                }

                DbTests.Add(dbTest);
            }
        }
    }

    public class DbUser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<DbStatistic> DbStatistics { get; set; }

        public DbUser()
        {
            DbStatistics = new List<DbStatistic>();
        }
    }

    public class DbStatistic
    {
        public int Id { get; set; }

        public int DbUserId { get; set; }

        public int DbTestId { get; set; }

        public int Score { get; set; }

        public DbUser DbUser { get; set; }

        public DbTest DbTest  { get; set; }
    }

    public class DbTest
    {
        public int Id { get; set; }

        [NotMapped]
        public string Name => DbTheories.Any() ? DbTheories.First().Topic : "Итоговый тест";

        public List<DbTheory> DbTheories { get; set; }

        public List<DbQuestion> Questions { get; set; }

        public List<DbStatistic> DbStatistics { get; set; }

        public DbTest()
        {
            DbTheories = new List<DbTheory>();
            Questions = new List<DbQuestion>();
            DbStatistics = new List<DbStatistic>();
        }
    }

    public class DbTheory
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }

        public string FilePath { get; set; }   

        public int DbTestId { get; set; }

        public DbTest DbTest { get; set; }
    }

    public class DbQuestion
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string QuestionText { get; set; }
      
        public List<DbAnswer> DbAnswers { get; set; }
        
        public DbTheory DbTheory { get; set; }

        public int DbTheoryId { get; set; }

        public DbQuestion()
        {
            DbAnswers = new List<DbAnswer>();
        }
    }

    public class DbAnswer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int DbQuestionId { get; set; }

        public bool IsCorrect { get; set; }

        [NotMapped]
        public bool IsUserSelected { get; set; }

        public DbQuestion DbQuestion { get; set; }
        public DbAnswer()
        {

        }

        public DbAnswer(string text)
        {
            Text = text;
        }
    }
}