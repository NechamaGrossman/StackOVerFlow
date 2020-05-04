using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverFlow.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<QuestionTags> QuestionsTags { get; set; }
    }
}
