using System;
using Models;
using AutoMapper;
using System.Collections.Generic;
using Services.Mappers;

namespace Services.ViewModels
{
    public class QuestionVm: ICustomAutoMapper
    {
        public int Id { get; set; }

        public string Qu { get; set; }

        public QuestionType QuestionType { get; set; }
        public List<Tuple<int, string>> Options { get; set; }
        public string TextAnswer { get; set; }
        public bool IsLastQuestion { get; set; }
        public Tuple<int, string> McqAnswer { get; set; }

        public void CreateMappings(Profile profile)
        {
            profile.CreateMap<McqQuestion, QuestionVm>().ReverseMap();
            profile.CreateMap<TextQuestion, QuestionVm>().ReverseMap();
        }
    }
}
