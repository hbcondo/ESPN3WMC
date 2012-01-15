using System;
using System.Collections.Generic;

namespace ESPN3Library.Models
{
    public class Sport
    {
        private int _id = 0;
        private string _name = string.Empty;
        private string _picture = string.Empty;
        private string _keyword = string.Empty;
        private List<League> _leagues = new List<League>();

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

        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }

        public List<League> Leagues
        {
            get { return _leagues; }
            set { _leagues = value; }
        }
    }
}
