using Models;

namespace Services.ViewModels
{
    public class AnswerVm
    {
        public int Id { get; set; }
        public string TextAnswer { get; set; }
        public int? McqAnswerId { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
