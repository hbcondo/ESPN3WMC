using System;
using System.Collections.Generic;

namespace ESPN3Library.Models
{
    /// <summary>
    /// All the listings for a specific sport
    /// </summary>
    public class Listings
    {
        public string Sport = string.Empty;
        public List<Match> Live = new List<Match>();
        public List<Match> Upcoming = new List<Match>();
        public List<Match> Replay = new List<Match>();
    }
}
