using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverFlow.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<Likes> Likes { get; set; }
    }
}
