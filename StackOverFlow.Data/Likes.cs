using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverFlow.Data
{
    public class Likes
    {
        public User User { get; set; }
        public Question Question { get; set; }
        public bool Like { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
