using System;
using System.Collections.Generic;
using System.Text;

namespace MatchingGame.Clases
{
    public class Avatar
    {
        public string PhotoUrl { get; set; }
        public string Id { get; set; }

        public string PhotoUrl1 { get; set; }
        public string Id1 { get; set; }

        public override string ToString()
        {
            return PhotoUrl;
        }
    }
}
