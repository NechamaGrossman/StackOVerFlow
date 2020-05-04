using Microsoft.EntityFrameworkCore;
using StackOverFlow.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverFlow.Data
{
    public class QuestionContext : DbContext
    {
        private readonly string _connectionString;
        public QuestionContext(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<User> User { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTags> QuestionsTags { get; set; }
        public DbSet<Answers> Answers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set up composite primary key
            modelBuilder.Entity<QuestionTags>()
                .HasKey(qt => new { qt.QuestionId, qt.TagId });

            //set up foreign key from QuestionsTags to Questions
            modelBuilder.Entity<QuestionTags>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionTags)
                .HasForeignKey(q => q.QuestionId);

            //set up foreign key from QuestionsTags to Tags
            modelBuilder.Entity<QuestionTags>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionsTags)
                .HasForeignKey(q => q.TagId);
            //set up composite primary key
            modelBuilder.Entity<QuestionTags>()
                .HasKey(qt => new { qt.QuestionId, qt.TagId });

            //set up foreign key from Likes to User
            modelBuilder.Entity<Likes>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(u=>u.UserId);
            //set up composite primary key
            modelBuilder.Entity<Likes>()
                .HasKey(l => new { l.QuestionId, l.UserId });

            //set up foreign key from Likes to Questions
            modelBuilder.Entity<Likes>()
                .HasOne(L => L.Question)
                .WithMany(q => q.Likes)
                .HasForeignKey(q => q.QuestionId);
        }
    }
}
    
