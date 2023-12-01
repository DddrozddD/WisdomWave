

namespace WisdomWave.Models
{
    public class PushQuestion
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int CountOfPoints { get; set; }
        public int? testId { get; set; }
    }
}
