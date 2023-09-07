using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<SubQuestion> SubQuestions { get; set; }
        public DbSet<Paragraph> Paragraph { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Unit> Unit{ get;}
    }
}
