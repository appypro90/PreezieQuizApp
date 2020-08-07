using System;
using System.Collections.Generic;

namespace Models
{
    public class McqQuestion : Question
    {
        public Tuple<int, string> McqAnswer { get; set; }
        public List<Tuple<int, string>> Options { get; set; }
    }
}
