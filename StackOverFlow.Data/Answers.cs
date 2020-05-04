using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverFlow.Data
{
    public class Answers
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public DateTime DatePosted { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
    }
}