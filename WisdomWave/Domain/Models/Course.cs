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
        public int RatingCourse { get; set; }
        public string ImageLinkCourse { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; }
        public IReadOnlyCollection<LearnerUserToCourse> LearnerUsers { get; set; }
        public string creatorUserId { get; set; }
        public User CreatorUser { get; set; }
        public IReadOnlyCollection<Review> Reviews { get; set; }
        public IReadOnlyCollection<Unit> Units { get; set; }
    }
}
