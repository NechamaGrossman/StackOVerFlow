using StackOverFlow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlow.Models
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }
        public List<string> Tags { get; set; }
    }
}
