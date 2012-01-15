using System;
using System.Collections.Generic;
using System.Collections;

namespace ESPN3Library.Models
{
    public class Match
    {
        #region Fields

        private string _date = string.Empty;
        private string _time = string.Empty;
        private DateTime _occurrence;
        private string _league = string.Empty;
        private string _category = string.Empty;
        private string _description = string.Empty;
        private string _name = string.Empty;
        private string _url = string.Empty;
        private ListingType _occurrenceType = ListingType.UNKNOWN;

        #endregion

        #region Properties

        internal string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        
        internal string Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public DateTime Occurrence
        {
            get
            {
                return _occurrence;
                //return DateTime.ParseExact(string.Concat(_date, _time), "MMddhh:mm tt", null);
                //return DateTime.ParseExact(string.Concat(_date, _time), "MMddyyyyhh:mm tt", null);
                //return DateTime.ParseExact(string.Concat(_date, _time), "MMddyyyyhh:mm tt zzz", null);
            }

            set { _occurrence = value;  }
        }

        public ListingType OccurrenceType
        {
            get { return _occurrenceType; }
            set { _occurrenceType = value; }
        }

        public string League
        {
            get { return _league; }
            set { _league = value; }
        }

        public string Category
        {
            get { return _category; }
            set { this._category = value; }
        }
        
        public string Name
        {
            get { return _name; }
            set { this._name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        
        public string VideoUrl
        {
            get { return _url; }
            set { _url = value; }
        }

        #endregion
    }
}
