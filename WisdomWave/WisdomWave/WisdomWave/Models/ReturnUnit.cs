using Domain.Models;

namespace WisdomWave.Models
{
    public class ReturnUnit
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string UnitName { get; set; }
        public string DateOfCreate { get; set; }
        public int? courseId { get; set; }
        public IReadOnlyCollection<Test>? Tests { get; set; }
        public IReadOnlyCollection<Page>? Pages { get; set; }
    }
}
