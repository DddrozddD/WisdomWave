using Domain.Models;

namespace WisdomWave.Models
{
    public class CreateCourseForm
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string Knowledge { get; set; }
        public string Education { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }
    }
}
