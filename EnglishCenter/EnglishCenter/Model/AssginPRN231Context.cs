using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnglishCenter.Model
{
    public partial class assginPRN231Context : DbContext
    {
        public assginPRN231Context()
        {
        }

        public assginPRN231Context(DbContextOptions<assginPRN231Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Mark> Marks { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=assginPRN231;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnsContent)
                    .HasMaxLength(50)
                    .HasColumnName("ansContent");

                entity.Property(e => e.IsTrue).HasColumnName("isTrue");

                entity.Property(e => e.QuestId).HasColumnName("questId");

                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestId)
                    .HasConstraintName("FK_Answer_Questions");
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("createDate");

                entity.Property(e => e.Mark1).HasColumnName("mark");

                entity.Property(e => e.TestId).HasColumnName("testId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK_Marks_Test");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Marks_Users");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.QuestionContent)
                    .HasMaxLength(50)
                    .HasColumnName("questionContent");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnswerId).HasColumnName("answerId");

                entity.Property(e => e.MarkId).HasColumnName("markId");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK__Review__answerId__6754599E");

                entity.HasOne(d => d.Mark)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.MarkId)
                    .HasConstraintName("FK__Review__markId__68487DD7");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Review__question__66603565");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasColumnName("code");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("createDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubjectId).HasColumnName("subjectId");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Test_Subject");

                entity.HasMany(d => d.Questions)
                    .WithMany(p => p.Tests)
                    .UsingEntity<Dictionary<string, object>>(
                        "TestQuestion",
                        l => l.HasOne<Question>().WithMany().HasForeignKey("QuestionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Test_Ques__quest__5FB337D6"),
                        r => r.HasOne<Test>().WithMany().HasForeignKey("TestId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Test_Ques__testI__5EBF139D"),
                        j =>
                        {
                            j.HasKey("TestId", "QuestionId");

                            j.ToTable("Test_Question");

                            j.IndexerProperty<int>("TestId").HasColumnName("testId");

                            j.IndexerProperty<int>("QuestionId").HasColumnName("questionId");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
