using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverFlow.Data
{
    public class Question
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int Id { get; set; }
        public DateTime DatePosted { get; set; }
        public List<QuestionTags> QuestionTags { get; set; }
        public List<Likes> Likes { get; set; }
        public List<Answers> Answers { get; set; }
    }
}
