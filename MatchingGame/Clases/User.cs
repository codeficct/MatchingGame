using System;
using System.Collections.Generic;
using System.Text;

namespace MatchingGame.Clases
{
    internal class User
    {
        public string _id { get; set; }
        public int score { get; set; }
        public int maxLevel { get; set; }
        public string photo { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string? password { get; set; }
        public string? createdAt { get; set; }
        public string? updatedAt { get; set; }
        public int? __v { get; set; }
        public string ?token { get; set; }
    }
}
