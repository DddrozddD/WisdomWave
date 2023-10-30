using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? Telephone { get; set; }
        public string? About { get; set; }
        public DateTime? DateOfBorn { get; set; }
        public int? Age { get; set; }
        public string? Town { get; set; }
        public string? Country { get; set; }
        public string? UserRating { get; set; }
        public string? ImagePhotoUserLink { get; set; }
        public int? subscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public IReadOnlyCollection<LearnerUserToCourse> LearningCourses { get; set; }
        public IReadOnlyCollection<Course> CreatedCourses { get; set; }
        public IReadOnlyCollection<Review> Reviews { get; set; }
        public IReadOnlyCollection<LikeDislike> LikesDislikes { get; set;}
    }
}
