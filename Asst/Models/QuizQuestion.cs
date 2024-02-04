namespace Asst.Models
{
    public class QuizQuestion
    {
        public int id { get; set; }
        public string content { get; set; }
        public string image { get; set; }
        public string category { get; set; }
        public List<string> answers { get; set; }
    }
}
