using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.MediaCenter.UI;
using Microsoft.MediaCenter.Hosting;

using ESPN3WMC.wsESPN3; // web service reference

namespace ESPN3WMC
{
    /// <summary>
    /// Class used to render default.mcml
    /// </summary>
    public class Homepage : ModelItem
    {
        #region Fields

        private ListingsResponse _listings = new ListingsResponse();
        private Sport _sport = new Sport();
        private Match _match = new Match();
        private Choice _sportsChoices = new Choice();
        private string _statusMessage = string.Empty;
        private string _url = string.Empty;

        #endregion

        #region Constructors

        public Homepage(ListingsResponse listings)
        {
            this._listings = listings;
            this._statusMessage = _listings.Message;

            // if listings were retrieved successfully, render data
            if (this._listings.Status == ListingsStatus.SUCCESS)
            {
                SetSport(0);
                SetSportsChoices();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        /// <value>The status message.</value>
        [MarkupVisible]
        private string StatusMessage
        {
            get { return this._statusMessage; }
            set { this._statusMessage = value; }
        }

        /// <summary>
        /// Gets the homepage status.
        /// </summary>
        /// <value>The homepage status.</value>
        [MarkupVisible]
        private ListingsStatus HomepageStatus
        {
            get { return this._listings.Status; }
        }

        /// <summary>
        /// Gets the sport.
        /// </summary>
        /// <value>The sport.</value>
        [MarkupVisible]
        private Sport Sport
        {
            get { return _sport; }
        }

        /// <summary>
        /// Gets the leagues for the current sport
        /// </summary>
        /// <value>The leagues.</value>
        [MarkupVisible]
        private League[] Leagues
        {
            get { return this._sport.Leagues; }
        }

        /// <summary>
        /// Gets or sets the page. This correlates to the url for video playback occurs
        /// </summary>
        /// <remarks>
        /// I should have probably called this property Webpage :P
        /// </remarks>
        /// <value>The page.</value>
        [MarkupVisible]
        private string Page
        {
            get { return this._url; }
            set { this._url = value; }
        }

        /// <summary>
        /// Gets or sets the sports choices pivot data
        /// </summary>
        /// <value>The sports choices.</value>
        [MarkupVisible]
        private Choice SportsChoices
        {
            get { return _sportsChoices; }
            set { if (_sportsChoices != value) { _sportsChoices = value; base.FirePropertyChanged("SportChoices"); } }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the sport. Called when pivot control value changes
        /// </summary>
        /// <param name="index">The index.</param>
        [MarkupVisible]
        private void SetSport(int index)
        {
            _sport = _listings.Sports[index];
        }

        /// <summary>
        /// Gets the leagues for a particular sport
        /// </summary>
        /// <param name="index">The index value in the pivot control that correlates to the array index where data is stored</param>
        /// <returns></returns>
        [MarkupVisible]
        private League[] GetLeagues(int index)
        {
            return _listings.Sports[index].Leagues;
        }

        /// <summary>
        /// Sets the sports choices pivot control
        /// </summary>
        private void SetSportsChoices()
        {
            ArrayListDataSet list = new ArrayListDataSet();

            // iterate through Sports collection and build list of sport names to be used in pivot control
            foreach (Sport s in _listings.Sports)
            {
                list.Add(s.Name);
            }

            Choice c = new Choice(this.Owner, "Sports", list);
            _sportsChoices = c;
        } 

        [Obsolete("Use MCML mark-up for NavigateToPage functionality")]
        private void ShowVideo()
        {
            AddInHost.Current.MediaCenterEnvironment.NavigateToPage(Microsoft.MediaCenter.PageId.ExtensibilityUrl, _url);
        }

        #endregion
    }
}
