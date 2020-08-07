using Db;
using Models;
using System;
using AutoMapper;
using System.Linq;
using Services.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Services
{
    public class QuizService : IQuizService
    {
        private IMapper Mapper { get; }
        private IDataStore Data { get; }

        public QuizService(IMapper mapper, IDataStore data)
        {
            Mapper = mapper;
            Data = data;
        }

        public async Task<IEnumerable<QuestionAnswerVm>> GetAllQuestionsAnswers()
        {
            var qna = new List<QuestionAnswerVm>();
            (await Data.AllQuestionAnswer()).ToList().ForEach(qa => qna.Add(Mapper.Map<QuestionAnswerVm>(qa)));

            return qna;
        }

        public async Task<QuestionVm> GetQuestionById(int id)
        {
            var qstn = Mapper.Map<QuestionVm>((await Data.AllQuestionAnswer()).FirstOrDefault(q => q.Id == id));
            qstn.IsLastQuestion = qstn.Id == (await Data.AllQuestionAnswer()).Last().Id;
            return qstn;
        }


        public async Task PatchAnswer(AnswerVm answer)
        {
            switch (answer.QuestionType)
            {
                case QuestionType.Mcq:
                {
                    if ((await Data.AllQuestionAnswer()).FirstOrDefault(q => q.Id == answer.Id) is McqQuestion question)
                        question.McqAnswer = question.Options.FirstOrDefault(a => a.Item1 == answer.McqAnswerId);
                    break;
                }
                case QuestionType.Text:
                {
                    if ((await Data.AllQuestionAnswer()).FirstOrDefault(q => q.Id == answer.Id) is TextQuestion question
                    )
                        question.TextAnswer = answer.TextAnswer;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}