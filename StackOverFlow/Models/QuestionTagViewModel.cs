using StackOverFlow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlow.Models
{
    public class QuestionTagViewModel
    {
        public Question Question { get; set; }
        public List<Tag> Tags { get; set; }
        public bool AlreadyLiked { get; set; }
        public bool LoggedOn { get; set; }
    }
}
