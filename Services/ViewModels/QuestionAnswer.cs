using Models;
using AutoMapper;
using Services.Mappers;

namespace Services.ViewModels
{
    public class QuestionAnswerVm : ICustomAutoMapper
    {
        public string Qu { get; set; }
        public string Answer { get; set; }

        public void CreateMappings(Profile profile)
        {
            profile.CreateMap<McqQuestion, QuestionAnswerVm>().ForMember(qna => qna.Answer, map =>
            map.MapFrom(mcq => mcq.McqAnswer.Item2))
                .ReverseMap();
            profile.CreateMap<TextQuestion, QuestionAnswerVm>().ForMember(qna => qna.Answer, map =>
            map.MapFrom(txt => txt.TextAnswer)).ReverseMap();
        }
    }
}
