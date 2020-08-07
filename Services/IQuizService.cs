using Models;
using System;
using Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IQuizService
    {
        Task<IEnumerable<QuestionAnswerVm>> GetAllQuestionsAnswers();
        Task<QuestionVm> GetQuestionById(int id);
        Task PatchAnswer(AnswerVm answer);
    }
}
