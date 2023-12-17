using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }


        public IReadOnlyCollection<Category>? ParentCategories { get; set;}
        public IReadOnlyCollection<Category>? ChildCategories { get;set;}
        public IReadOnlyCollection<Course>? Courses { get; set;}
    }
}
