using Models;
using FluentValidation;
using Services.ViewModels;

namespace Services.Validators
{
    public class AnswerVmValidator: AbstractValidator<AnswerVm>
    {
        public AnswerVmValidator()
        {
            RuleFor(x => x.McqAnswerId).NotEmpty().When(x => x.QuestionType == QuestionType.Mcq);
            RuleFor(x => x.TextAnswer).NotEmpty().When(x => x.QuestionType == QuestionType.Text);
        }
    }
}