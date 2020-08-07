using Models;
using System;
using System.Collections.Generic;

namespace Db
{
    public static class Data
    {
        public static readonly IList<Question> AllQuestionAnswers = new List<Question>()
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
                    new Tuple<int, string>(3, "cow"),
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
    };
}
