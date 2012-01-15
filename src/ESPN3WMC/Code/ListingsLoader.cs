using System;
using System.Diagnostics;
using ESPN3WMC.wsESPN3;         // web service reference
using Microsoft.MediaCenter.UI;

namespace ESPN3WMC
{
    /// <summary>
    /// ModelItem class that is responsible for obtaining and returning listing data
    /// </summary>
    public sealed class ListingsLoader : ModelItem
    {
        #region Fields

        /// <summary>
        /// Whether loading is complete.
        /// </summary>
        private bool _isComplete = false;

        /// <summary>
        /// Authorization key required to consume web service
        /// </summary>
        private readonly string authKey = "3ce51f90-f6eb-437e-9680-802f20e0160e";

        /// <summary>
        /// Listing data container
        /// </summary>
        private ListingsResponse _listings = new ListingsResponse();

        #endregion

        #region Constructors

        /// <summary>
        /// Starts loading and parsing the listings, asynchronously. 
        /// IsComplete will be set to true upon completion.
        /// </summary>
        public ListingsLoader()
        {
            // Runs LoadFeeds on a background thread.  Upon completion, sets IsComplete on the UI thread.
            Application.DeferredInvokeOnWorkerThread(
                delegate { LoadListings(); },
                delegate { IsComplete = true; },
                null);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether data loading is complete.
        /// </summary>
        [MarkupVisible]
        private bool IsComplete
        {
            get { return _isComplete; }
            set
            {
                if (_isComplete != value)
                {
                    _isComplete = value;
                    FirePropertyChanged("IsComplete");
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the listings data.
        /// </summary>
        [MarkupVisible]
        private void LoadListings()
        {
            // attempt to retrieve listing data from web service
            try
            {
                wsESPN3.Service service = new Service();

                // set authentication key for service
                UserCredentials uc = new UserCredentials();
                uc.AuthenticationKey = authKey;
                service.UserCredentialsValue = uc;
                service.PreAuthenticate = true;

                // obtain screen resolution
                int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width; //System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
                int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height; //System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;

                // call web service to get listing information
                this._listings = service.GetListingsForWMCV2(screenWidth, screenHeight);
            }

            // oops!
            catch (Exception ex)
            {
                this._listings.Status = ListingsStatus.FAILURE;
                this._listings.Message = "ESPN3WMC is unable to retrieve listing data at this time. Please try again later.";

                // log error
                Helper.WriteToLogFile(string.Concat(ex.Message, " - ", ex.StackTrace));
            }
        }

        /// <summary>
        /// Returns the listings data
        /// </summary>
        /// <returns><see cref="Homepage"/></returns>
        [MarkupVisible]
        private Homepage GetListings()
        {
            if (!IsComplete) throw new InvalidOperationException();
            return new Homepage(this._listings);
        }

        #endregion
    }
}
