using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESPN3Library.Models
{
    public class League
    {
        private int _id = 0;
        private string _name = string.Empty;
        private string _picture = string.Empty;
        private List<Match> _matches = new List<Match>();

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        public List<Match> Matches
        {
            get { return _matches; }
            set { _matches = value; }
        }
    }
}
