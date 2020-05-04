using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverFlow.Data
{
    public class QuestionTags
    {
        public int QuestionId { get; set; }
        public int TagId { get; set; }
        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }
}
