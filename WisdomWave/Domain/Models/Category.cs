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

        public int? ParentCategoryId { get; set; } // Ссылка на родительскую категорию
        public IReadOnlyCollection<Category> ParentCategories { get; set; }
        public IReadOnlyCollection<Category> ChildCategories { get; set; }
        public IReadOnlyCollection<Course> Courses { get; set; }
    }
}