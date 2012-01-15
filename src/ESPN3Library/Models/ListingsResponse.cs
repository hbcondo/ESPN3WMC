using System;
using System.Collections.Generic;

namespace ESPN3Library.Models
{
    public class ListingsResponse
    {
        #region Fields
        
        private ListingsStatus _status;
        private string _message;
        private List<Sport> _sports;

        #endregion Fields

        #region Constructor

        public ListingsResponse()
        {
            this._status = ListingsStatus.FAILURE;
            this._message = string.Empty;
            this._sports = new List<Sport>();
        }

        #endregion

        #region Properties

        public ListingsStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public List<Sport> Sports
        {
            get { return _sports; }
            set { _sports = value; }
        }

        #endregion
    }
}
