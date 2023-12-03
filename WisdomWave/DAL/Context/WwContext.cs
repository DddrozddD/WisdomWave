﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DAL.Context
{
    public class WwContext : IdentityDbContext
    {
        public WwContext(DbContextOptions<WwContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<Subscription>().HasOne(s => s.User).WithOne(u => u.Subscription).HasPrincipalKey<Subscription>(s => s.userId);
            modelBuilder.Entity<Course>().HasOne(c => c.CreatorUser).WithMany(u => u.CreatedCourses).HasForeignKey(c => c.creatorUserId);
            modelBuilder.Entity<Course>().HasMany(c => c.Categories).WithMany(c => c.Courses);
            modelBuilder.Entity<Review>().HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.userId);
            modelBuilder.Entity<Review>().HasOne(r=>r.Course).WithMany(c=>c.Reviews).HasForeignKey(r => r.courseId);
            modelBuilder.Entity<LikeDislike>().HasOne(l => l.User).WithMany(u=>u.LikesDislikes).HasForeignKey(l => l.userId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<LikeDislike>().HasOne(l => l.Review).WithMany(r => r.LikesDislikes).HasForeignKey(l => l.reviewId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<LearnerUserToCourse>().HasOne(l => l.User).WithMany(u => u.LearningCourses).HasForeignKey(l=>l.userId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<LearnerUserToCourse>().HasOne(l => l.Course).WithMany(c => c.LearnerUsers).HasForeignKey(l => l.courseId).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Category>().HasMany(c => c.ParentCategories).WithMany(c => c.ChildCategories);
            modelBuilder.Entity<WwUser>().HasMany(u => u.CompletedPages).WithMany(p => p.PassedPageUsers);
            modelBuilder.Entity<WwUser>().HasMany(u => u.CompletedTests).WithMany(t => t.PassedTestUsers);
            modelBuilder.Entity<WwUser>().HasMany(u => u.CompletedUnits).WithMany(u => u.PassedUnitUsers);
            modelBuilder.Entity<Rating>().HasOne(r => r.Course).WithMany(u => u.Ratings).HasForeignKey(r => r.courseId).OnDelete(DeleteBehavior.ClientSetNull); ;
            modelBuilder.Entity<Rating>().HasOne(r => r.User).WithMany(c => c.Ratings).HasForeignKey(r => r.userId).OnDelete(DeleteBehavior.ClientSetNull); ;


            modelBuilder.Entity<LearnerUserToCourse>().Property(uc=>uc.isCompleted).HasDefaultValue(false);

            modelBuilder.Entity<Unit>().HasOne(u => u.Course).WithMany(c => c.Units).HasForeignKey(u => u.courseId);
            modelBuilder.Entity<Page>().HasOne(p => p.Unit).WithMany(u => u.Pages).HasForeignKey(p=>p.unitId);
            modelBuilder.Entity<Paragraph>().HasOne(p => p.Page).WithMany(u => u.Paragraphs).HasForeignKey(p => p.pageId);
            modelBuilder.Entity<Test>().HasOne(t => t.Unit).WithMany(u => u.Tests).HasForeignKey(t => t.unitId);
            modelBuilder.Entity<Question>().HasOne(q => q.Test).WithMany(t => t.Questions).HasForeignKey(q => q.testId);
            modelBuilder.Entity<Answer>().HasOne(a => a.Question).WithMany(q => q.Answers).HasForeignKey(a => a.questionId);
            modelBuilder.Entity<Answer>().HasOne(a => a.SubQuestion).WithMany(sq => sq.Answers).HasForeignKey(a => a.subQuestionId);
            modelBuilder.Entity<SubQuestion>().HasOne(sq => sq.Question).WithMany(q => q.SubQuestions).HasForeignKey(sq => sq.questionId);
            modelBuilder.Entity<LearnerUserToCourse>().HasKey(l => new { l.userId, l.courseId });
            modelBuilder.Entity<LikeDislike>().HasKey(l => new { l.userId, l.reviewId });
            modelBuilder.Entity<Rating>().HasKey(r => new { r.userId, r.courseId });

            modelBuilder.Entity<Review>().Property(r => r.courseId).IsRequired(false);
            modelBuilder.Entity<WwUser>().Property(u=>u.subscriptionId).IsRequired(false);
            modelBuilder.Entity<Answer>().Property(a => a.questionId).IsRequired(false);
            modelBuilder.Entity<Answer>().Property(a => a.subQuestionId).IsRequired(false);
            modelBuilder.Entity<Page>().Property(p => p.unitId).IsRequired(false);
            modelBuilder.Entity<Test>().Property(t => t.unitId).IsRequired(false);
            modelBuilder.Entity<Unit>().Property(u => u.courseId).IsRequired(false);
            modelBuilder.Entity<Question>().Property(q => q.testId).IsRequired(false);
            modelBuilder.Entity<SubQuestion>().Property(sq => sq.questionId).IsRequired(false);
            modelBuilder.Entity<Question>().Property(q => q.QuestionType).IsRequired(false);
            modelBuilder.Entity<WwUser>().Property(u => u.Country).IsRequired(false);
            modelBuilder.Entity<WwUser>().Property(u => u.ImagePhotoUserLink).IsRequired(false);
            modelBuilder.Entity<WwUser>().Property(u => u.UserRating).IsRequired(false);
            modelBuilder.Entity<WwUser>().Property(u => u.subscriptionId).IsRequired(false);
            modelBuilder.Entity<WwUser>().Property(u => u.Town).IsRequired(false);
            modelBuilder.Entity<Course>().Property(c => c.ImageLinkCourse).IsRequired(false);
            modelBuilder.Entity<Course>().Property(c => c.RatingCourse).IsRequired(false);
            


        }

        public DbSet<WwUser> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<LearnerUserToCourse> LearnerUserToCourse { get; set;}
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<LikeDislike> LikesDislikes { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<SubQuestion> SubQuestions { get; set; }
        public DbSet<Paragraph> Paragraph { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Page> Page { get; set; }
    }
}
