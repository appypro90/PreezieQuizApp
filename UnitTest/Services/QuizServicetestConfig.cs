using AutoMapper;
using Db;
using Models;
using Moq;
using NUnit.Framework;
using Services;
using Services.Mappers;
using System;
using System.Collections.Generic;

namespace UnitTest.Services
{
    public class QuizServicetestConfig : BaseTest
    {
        public static List<Question> TestQuestions = new List<Question>()
        {
            new TextQuestion
            {
                Id = 1,
                Qu = "Your Hobbies",
                QuestionType = QuestionType.Text
            },
            new McqQuestion
            {
                Id = 2,
                Qu = "Favourite Animal",
                QuestionType = QuestionType.Mcq,
                Options = new List<Tuple<int, string>>()
                {
                    new Tuple<int, string>(1, "Cat"),
                    new Tuple<int, string>(2, "Dog"),
                    new Tuple<int, string>(3, "Cow"),
                    new Tuple<int, string>(4, "kangaroo")
                }
            },

            new TextQuestion
            {
                Id = 3,
                QuestionType = QuestionType.Text,
                Qu = "Your favourite destination"
            },

            new McqQuestion
            {
                Id = 4,
                Qu = "Favourite Color",
                QuestionType = QuestionType.Mcq,
                Options = new List<Tuple<int, string>>()
                {
                    new Tuple<int, string>(1, "Red"),
                    new Tuple<int, string>(2, "Blue"),
                    new Tuple<int, string>(3, "Black"),
                    new Tuple<int, string>(4, "Green")
                }
            }
        };

        protected IQuizService SystemUnderTest { get; set; }

        [SetUp]
        public void Setup()
        {
            base.SetUp();

            var myProfile = new StartupAutoMapper();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);
            var dataStore = new Mock<IDataStore>();
            dataStore.Setup(p => p.AllQuestionAnswer()).ReturnsAsync(TestQuestions);
            SystemUnderTest = new QuizService(mapper, dataStore.Object);
        }
    }
}
