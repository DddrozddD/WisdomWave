using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int? RatingCourse { get; set; }
        public string? ImageLinkCourse { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public double OverallRating { get; set; }
        public DateTime DateOfCreate { get; set; }
        public string Language { get; set; }
        public IReadOnlyCollection<WwUser> LearnerUsers { get; set; }
        public IReadOnlyCollection<WwUser> CompletedUsers { get; set; }
        public string creatorUserId { get; set; }
        public string creatorUserName { get; set; }
        public WwUser CreatorUser { get; set; }
        public IReadOnlyCollection<Review> Reviews { get; set; }
        public IReadOnlyCollection<Rating> Ratings { get; set; }
        public IReadOnlyCollection<Unit> Units { get; set; }
        public IReadOnlyCollection<Category> Categories { get; set; }
    }
}
