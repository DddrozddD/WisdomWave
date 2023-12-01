using Domain.Models;

namespace WisdomWave.Models
{
    public class PushAnswer
    {
        public bool IsCorrect { get; set; }
        public string AnswerText { get; set; }
        public int? questionId { get; set; }
    }
}
