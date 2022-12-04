using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DbLab2_Individual.Models.SecondDatabase
{
    public partial class DataBaseFirst_Ex2Context : DbContext
    {
        private DataBaseFirst_Ex2Context()
        {
        }

        public DataBaseFirst_Ex2Context(DbContextOptions<DataBaseFirst_Ex2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Auth> Auths { get; set; } = null!;

        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=D:\StudyProjects\DbLab2_Individual\DataBaseFirst_Ex2.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auth>(entity =>
            {
                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasMany(d => d.Books)
                    .WithMany(p => p.Auths)
                    .UsingEntity<Dictionary<string, object>>(
                        "AuthBook",
                        l => l.HasOne<Book>().WithMany().HasForeignKey("BooksId").OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Auth>().WithMany().HasForeignKey("AuthsId").OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("AuthsId", "BooksId");

                            j.ToTable("AuthBooks");

                            j.IndexerProperty<long>("AuthsId").HasColumnName("auths_id");

                            j.IndexerProperty<long>("BooksId").HasColumnName("books_id");
                        });
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.CountPage).HasColumnName("count_page");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
