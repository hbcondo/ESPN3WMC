using System;
using System.Linq;
using System.Collections.Generic;

using HtmlAgilityPack;

using ESPN3Library.Models;
using ESPN3Library.Properties;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Net.Cache;

namespace ESPN3Library
{
    /// <summary>
    /// Class containing methods to obtain and return ESPN3 sport listing data
    /// </summary>
    public class ESPN3Service
    {
        #region Fields

        /// <summary>
        /// Matches container
        /// </summary>
        private List<Match> _matches = new List<Match>();

        /// <summary>
        /// Listings container
        /// </summary>
        private Listings _listings = new Listings();

        /// <summary>
        /// Web page for video playback
        /// </summary>
        private readonly string baseVideoUrl = Settings.Default.BaseVideoUrl;

        /// <summary>
        /// Homepage of espn3
        /// </summary>
        private readonly string baseUrl = Settings.Default.BaseDataUrl;

        /// <summary>
        /// Additional url directory where specific sport listing data resides
        /// </summary>
        private readonly string baseUrlAddition = Settings.Default.BaseDataUrlAddition;

        private int _screenResolutionHeight = 432;

        private int _screenResolutionWidth = 768;

        #endregion

        #region Properties

        public string AuthorizationKeyForWMC
        {
            get { return Settings.Default.WMCAuthorizationKey; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a collection of all available sports and their leagues
        /// </summary>
        /// <returns></returns>
        public List<Sport> GetSports()
        {
            return GetSports(_screenResolutionWidth, _screenResolutionHeight);
        }

		/// <summary>
		/// Returns a collection of all available sports and their leagues
		/// </summary>
		/// <param name="screenWidth">Width of the screen.</param>
		/// <param name="screenHeight">Height of the screen.</param>
		/// <returns></returns>
        public List<Sport> GetSports(int screenWidth, int screenHeight)
        {
            // set resolution widths
            this._screenResolutionWidth = screenWidth;
            this._screenResolutionHeight = screenHeight;
            
            List<Sport> sportsCollection = new List<Sport>();
            List<Match> listings = GetESPN3Events(); //GetListings(string.Empty);

            #region Live Matches

            // add new category containing all live matches (if there are any)
            IEnumerable<Match> liveMatches = (from m in listings
                                              where m.OccurrenceType == ListingType.LIVE
                                              select m);

            if (liveMatches.Count() > 0)
            {
                Sport sport = new Sport() { Name = "ON NOW" };
                League league = new League() { Name = "All Sports", Matches = liveMatches.ToList() };

                sport.Leagues.Add(league);
                sportsCollection.Add(sport);
            }

            #endregion

            // get all sports
            List<string> categories = (from c in listings
                                       select c.Category).Distinct().ToList();

            // build sport grouped by league
            foreach (string c in categories)
            {
                Sport sport = new Sport();
                sport.Name = c;

                // get all leagues for this sport
                List<string> leagues = (from l in listings
                                        where l.Category == c
                                        select l.League).Distinct().ToList();

                // get all matches for this league
                foreach (string l in leagues)
                {
                    League league = new League();
                    league.Name = l;
                    league.Matches = (from m in listings
                                      where m.League == l && m.Category == c
                                      select m).ToList();

                    // add match to league
                    sport.Leagues.Add(league);
                }

                // add league to sport
                sportsCollection.Add(sport);
            }

            // added 7/5/2011 - there are times where espn3 website is inaccessible or displays an error page and sports data cannot be retrieved
            if (sportsCollection.Count == 0)
                throw new Exception("No sports retrieved. ESPN3 must be unavailable.");

            return sportsCollection;
        }

        /// <summary>
        /// Gets the events from ESPN3 feed data urls
        /// </summary>
        /// <returns></returns>
        public List<Match> GetESPN3Events()
        {
            // define screen resolution value to be passed into VideoUrl property of Match object
            string playerResolution = string.Concat(this._screenResolutionWidth, "x", this._screenResolutionHeight);

            // define urls where event data will be obtained from
            string replyEventsUrl = string.Format(baseUrl, "replay", GetRandomNumber());
            string liveEventsUrl = string.Format(baseUrl, "live", GetRandomNumber());
            string[] dataUrls = { replyEventsUrl, liveEventsUrl };

            foreach (string dataUrl in dataUrls)
            {
                String espn3RawResponse = string.Empty;
                /*
                 * XML Header is not returned in response so xml parsers can't parse it :P
                 * Therefore, save this XML to a string and load it up in a XmlDocument object
                 */
                try
                {
					WebRequest request = WebRequest.Create(dataUrl);
					request.PreAuthenticate = false;
					request.Timeout = 120000;	// 2 minutes
					request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);	// try to prevent caching of data

					using (WebResponse webResponse = request.GetResponse())
					{
						using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
						{
							espn3RawResponse = sr.ReadToEnd().Trim();
						}
					}

					// arg, funky values in the response
					string espn3RawResponse2 = espn3RawResponse.Replace("&", string.Empty);
					
					// load up the xml string into a xmldocument object so we can read the element data
					XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(espn3RawResponse2);

                    #region Raw XML returned from ESPN

                    /*
                    <!-- http://espn.go.com/watchespn/feeds/startup?action=live -->
                    <events>
                      <event id="226183" type="live" bamContentId="18757451" bamEventId="176-12504" mediaState="MEDIA_ON">
                        <gameId>null</gameId>
                        <ttl>0</ttl>
                        <name>Live@ Deutsche Bank Championship (PGA Tour)</name>
                        <league>PGA Tour</league>
                        <sport code="GO">GOLF</sport>
                        <sportDisplayValue>GOLF</sportDisplayValue>
                        <showId>4556</showId>
                        <program code="PR"/>
                        <networkId>n360</networkId>
                        <caption/>
                        <site/>
                        <summary/>
                        <espnSportId>1106</espnSportId>
                        <gameStatus/>
                        <startTime>201109031300</startTime>
                        <endTime>201109031800</endTime>
                        <startTimeGmtMs>1315069200000</startTimeGmtMs>
                        <endTimeGmtMs>1315087200000</endTimeGmtMs>
                        <checkBlackout>false</checkBlackout>
                        <thumbnail>
                          <large>http://a.espncdn.com/espn360/images/go/pga_tour/226183.jpg</large>
                          <small>http://a.espncdn.com/espn360/images/go/pga_tour/226183_small.jpg</small>
                        </thumbnail>
                        <aspectRatio>16:9</aspectRatio>
                        <language>English</language>
                        <cc>false</cc>
                        <ccPosition>bottomLeft</ccPosition>
                        <multilanguageEvents/>
                      </event>
                    </events>

                    <!-- http://espn.go.com/watchespn/feeds/startup?action=replay -->
                    <events>
                      <event id="226187" type="replay" bamContentId="18757547" bamEventId="176-12506" mediaState="MEDIA_ARCHIVE">
                        <gameId>null</gameId>
                        <ttl>0</ttl>
                        <name>Croatia vs. Montenegro (Preliminary Round)</name>
                        <league>FIBA EuroBasket</league>
                        <sport code="BK">BASKETBALL</sport>
                        <sportDisplayValue>BASKETBALL</sportDisplayValue>
                        <showId>832</showId>
                        <program code="WS"/>
                        <networkId>n360</networkId>
                        <caption/>
                        <site>Alytus Arena Alytus,  Lithuania</site>
                        <summary/>
                        <espnSportId>null</espnSportId>
                        <gameStatus/>
                        <startTime>201109031400</startTime>
                        <endTime>201109031600</endTime>
                        <startTimeGmtMs>1315072800000</startTimeGmtMs>
                        <endTimeGmtMs>1315080000000</endTimeGmtMs>
                        <checkBlackout>false</checkBlackout>
                        <thumbnail>
                          <large>http://a.espncdn.com/espn360/images/bk/fiba_eurobasket/226187.jpg</large>
                          <small>http://a.espncdn.com/espn360/images/bk/fiba_eurobasket/226187_small.jpg</small>
                        </thumbnail>
                        <aspectRatio>16:9</aspectRatio>
                        <language>English</language>
                        <cc>False</cc>
                        <ccPosition>bottomLeft</ccPosition>
                        <multilanguageEvents/>
                      </event>
                    </events>
                 */
                    #endregion

                    XmlNodeList xmlnodeList = xdoc.DocumentElement.SelectNodes("event");

                    foreach (XmlNode x in xmlnodeList)
                    {
						try
						{
							Match m = new Match();
							m.OccurrenceType = (ListingType)Enum.Parse(typeof(ListingType), x.Attributes["type"].InnerText.ToUpper());
							m.Occurrence = DateTime.ParseExact(x.SelectSingleNode("startTime").InnerText, "yyyyMMddHHmm", null);
							m.Category = x.SelectSingleNode("sportDisplayValue").InnerText;
							m.League = x.SelectSingleNode("league").InnerText;
							m.Name = GetShortName(x.SelectSingleNode("name").InnerText);
							m.Description = x.SelectSingleNode("name").InnerText;
							m.VideoUrl = string.Format(baseVideoUrl, playerResolution, x.Attributes["id"].InnerText);

							_matches.Add(m);
						}
						catch
						{
							// do nothing if there is an error attempting to parse an individual match
						}
                    }
                }

                catch (Exception ex)
                {
                    throw new Exception(string.Concat(ex.Message, "\n - ", ex.StackTrace));
                }
            }
            return _matches;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a random number.
        /// </summary>
        /// <remarks>
        /// Used when creating the url to obtain event data. 
        /// Seems there is a caching issue on ESPN's side so they add a 11-digit random number to each event url request
        /// ex. http://espn.go.com/espn3/feeds/featuredEvents?rand=91168437829
        /// 
        /// Both DateTime.Now and Random works but it returns the same value every time the method is called
        /// http://weblogs.asp.net/smehaffie/archive/2009/03/15/random-number-in-c-be-careful-of-some-of-the-samples-you-find.aspx
        /// </remarks>
        /// <returns></returns>
        internal string GetRandomNumber()
        {
            return DateTime.Now.Ticks.ToString().Substring(0, 11);
            /*int r1 = new Random().Next(100000000, 999999999);
            int r2 = new Random().Next(10, 99);

            return string.Concat(r1, r2);*/
        }

        /// <summary>
        /// Formats the date.
        /// </summary>
        /// <param name="rawHtml">The raw HTML.</param>
        /// <remarks>
        /// No year is returned
        /// </remarks>
        /// <example>
        /// 8/05
        /// </example>
        /// <returns></returns>
        private string FormatDate(string rawHtml)
        {
            string[] dateParts = rawHtml.Split('/');

            if (dateParts[0].Length == 1)
                return string.Concat("0", dateParts[0], dateParts[1]);
            else
                return string.Concat(dateParts[0], dateParts[1]);
        }

        /// <summary>
        /// Formats the time.
        /// </summary>
        /// <param name="rawHtml">The raw HTML.</param>
        /// <remarks>
        /// Timezone is removed b/c can't get datetime object to support it
        /// </remarks>
        /// <example>
        /// 3:15 PM EDT
        /// 12:00 PM EDT
        /// </example>
        /// <returns></returns>
        private string FormatTime(string rawHtml)
        {
            if (rawHtml.Length == 11)
                return string.Concat("0", rawHtml).Remove(rawHtml.Length-3);
            else
                return rawHtml.Remove(rawHtml.Length-4);
        }

        /// <summary>
        /// Sets the listings container.
        /// </summary>
        /// <param name="rawHtml">The raw HTML.</param>
        /// <param name="listingType">Type of listing.</param>
        private void SetListingsContainer(HtmlDocument rawHtml, ListingType listingType)
        {
            HtmlNodeCollection rawMatches = rawHtml.DocumentNode.SelectNodes("//div");

            foreach (HtmlNode rawMatch in rawMatches)
            {
                //HtmlNodeCollection matchData = rawMatch.SelectNodes("/div[1]/table[1]/tr[2]/td"); // try to use xpath to navigate to individual match data
                HtmlNodeCollection matchData = rawMatch.ChildNodes[1].ChildNodes[2].ChildNodes;

                #region Raw HTML Example
                /*
                 * Index Values 2, 4 contain empty text when parser extracts HTML
                    <div id="127" class="03312011xxo ATP" style="border-bottom:1px solid #D0D0D0;">
                    <table>
                    <tr>
                    <th class="time">Time</th>
                    <th class="mod-cat">Sport</th>
                    <th class="title">League</th>
                    <th class="sub">Description</th>
                    <tr>
                    <td class="time">07:00 PM</td>                          // index = 1
                    <td class="mod-cat">TENNIS</td>                         // index = 3
                    <td class="title">ATP</td>                              // index = 5
                    <td class="sub">Sony Ericsson Open (Quarterfinal)</td>  // index = 7
                    </tr>
                    </table>
                 */
                #endregion

                // data source includes html space character for some reason :P
                string rawDescription = matchData[7].InnerText.Replace("&nbsp;", string.Empty);

                Match m = new Match()
                {
                    OccurrenceType = listingType,
                    Date = GetMatchDate(rawMatch.Attributes["class"].Value),
                    Time = matchData[1].InnerText,
                    Category = matchData[3].InnerText,
                    League = matchData[5].InnerText,
                    Name = GetShortName(rawDescription),
                    Description = rawDescription,
                    VideoUrl = GetMatchVideoURL(matchData[7].InnerHtml)
                };
                
                _matches.Add(m);
            }
        }

        /// <summary>
        /// Removes text contained within brackets. Do this to shorten length of description for display purposes
        /// </summary>
        /// <param name="description">Text to shorten</param>
        /// <returns>string</returns>
        private string GetShortName(string description)
        {
            // 7/5/2011 - added trim b/c some tennis title begin with seed value (ex. (3) Roger Federer) and that leaves a space in the beginning of the name which is apparent in the ui
            return System.Text.RegularExpressions.Regex.Replace(description, @"\s*?(?:\(.*?\)|\[.*?\]|\{.*?\})", String.Empty).Trim();
        }

        /// <summary>
        /// Attempts to parse out and return the match date from a raw HTML snippet
        /// </summary>
        /// <param name="rawHtml">The raw HTML.</param>
        /// <returns></returns>
        private string GetMatchDate(string rawHtml)
        {
            string matchDate = string.Empty;
            char[] delimiters = { ' ' };
            string[] attrs = rawHtml.Split(delimiters);

            // ensure we can parse class attribute value
            if (attrs != null)
            {
                /*
                 * No date is present in the class value if match if of type live
                 * Therefore, use current date but format as if date is provided
                 */
                switch (attrs.Length)
                {
                    case 1: matchDate = DateTime.Now.Date.ToString("MMddyyyy"); break;  // class="NCAAM"
                    case 2: matchDate = attrs[0].Remove(8, 3); break;                   // class="02262011xxx MLS" class="NCAAM"
                }
            }
            
            return matchDate;
        }

        /// <summary>
        /// Attempts to parse out and return the web page url from a raw HTML snippet
        /// </summary>
        /// <param name="rawHtml">The raw HTML.</param>
        /// <returns></returns>
        private string GetMatchVideoURL(string rawHtml)
        {
            // no hyperlink if match is labeled as upcoming
            if (rawHtml.IndexOf("</a>") > -1)
            {
                try
                {
                    HtmlDocument html = new HtmlDocument();
                    html.LoadHtml(rawHtml);

                    /*
                     * attempt to parse onclick attribute to obtain video id
                     * onClick="launchPlayer(118870,'Caribbean Twenty20')
                     */
                    HtmlNode hyperlink = html.DocumentNode.SelectNodes("//a")[0];
                    string onClick = hyperlink.Attributes["onClick"].Value;
                    char[] delimiters = { '(', ',' };
                    string[] launchPlayerValues = onClick.Split(delimiters);
                    string videoId = launchPlayerValues[1];

                    return string.Concat(baseVideoUrl, videoId);
                }

                catch
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        #endregion

		#region Obsolete Methods

		[Obsolete]
		public string GetVideoPageHTML()
		{
			HtmlWeb doc = new HtmlWeb();
			doc.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; MediaCenter 6.1.7601.17514)";
			HtmlDocument rawData = doc.Load("http://espn.go.com/espn3/player?id=20315");
			return rawData.DocumentNode.InnerHtml;
		}

		/// <summary>
		/// Returns all the listings for a particular sport
		/// </summary>
		/// <param name="sport">Sport to get listings for. If empty, defaults to all sports</param>
		/// <returns>Listings</returns>
		[Obsolete]
		public List<Match> GetListings(string sport)
		{
			string dataUrl = baseUrl;

#if (DEBUG)
            // specify sport url value if sport is specified 
            if (!string.IsNullOrEmpty(sport))
                dataUrl = string.Concat(baseUrl, baseUrlAddition, sport);
#endif

			HtmlWeb doc = new HtmlWeb();
			HtmlNodeCollection rawData = doc.Load(dataUrl).DocumentNode.SelectNodes("//div[@class='container listing']");

			if (rawData != null)
			{
				for (int i = 0; i < rawData.Count; i++)
				{
					HtmlDocument rawListings = new HtmlDocument();
					rawListings.LoadHtml(rawData[i].InnerHtml);

					if (rawListings.DocumentNode.ChildNodes.Count > 1)  // if equal to 1 or less, then there are no matches
					{
						_listings.Sport = sport;

						switch (i)
						{
							case 0: SetListingsContainer(rawListings, ListingType.LIVE); break;
							//case 1: SetListingsContainer(rawListings, ListingType.UPCOMING); break;
							case 2: SetListingsContainer(rawListings, ListingType.REPLAY); break;
						}
					}
				}
			}

			return _matches;
		}

		/// <summary>
		/// Gets the events from watchespn
		/// </summary>
		/// <returns></returns>
		[Obsolete]
		public List<Sport> GetEvents()
		{
			#region Raw HTML

			/*
             * <section class="chart AR">
    <h2>
        Auto Racing</h2>
    <table cellspacing="0" class="col-3">
        <tr class="first espn3">
            <td class="date">
                8/05
            </td>
            <td class="time">
                2:20 PM EDT
            </td>
            <td class="event">
                <a href="#" onclick='launchPlayer("218865", "", "", ""); return false;' class="watchable">
                    American Le Mans Series Mid-Ohio Sports Car Challenge (Qualifying Rounds)</a>
            </td>
            <td class="channel">
                <span class="channel-logo channel-logo-espn3">espn3</span>
            </td>
        </tr>
        <tr class="last espn3">
            <td class="date">
                8/06
            </td>
            <td class="time">
                3:15 PM EDT
            </td>
            <td class="event">
                <a href="#" onclick='launchPlayer("218891", "", "", ""); return false;' class="watchable">
                    American Le Mans Series Mid-Ohio Sports Car Challenge</a>
            </td>
            <td class="channel">
                <span class="channel-logo channel-logo-espn3">espn3</span>
            </td>
        </tr>
        <tr class="last espn3">
            <td class="date">
                8/21
            </td>
            <td class="time">
                12:00 PM EDT
            </td>
            <td class="event">
                <a href="#" onclick='launchPlayer("219208", "", "", ""); return false;' class="watchable">
                    Lucas Oil NHRA Nationals presented by Lucas Oil</a>
            </td>
            <td class="channel">
                <span class="channel-logo channel-logo-espn3">espn3</span>
            </td>
        </tr>
    </table>
</section>
             */

			#endregion

			List<Sport> sportsCollection = new List<Sport>();
			string dataUrl = "http://espn.go.com/watchespn/format/design11/index/replay_events?xhr=1&days=all&searchTerm=";

			HtmlWeb doc = new HtmlWeb();
			HtmlNodeCollection rawData = doc.Load(dataUrl).DocumentNode.SelectNodes("//h2");

			foreach (HtmlNode node in rawData)
			{
				Sport s = new Sport()
				{
					Name = node.InnerText
				};

				League l = new League();

				foreach (HtmlNode tableRow in node.NextSibling.NextSibling.ChildNodes.Where(n => n.Name.Equals("tr")))
				{
					/*
						"/section[1]/table[1]/tr[1]/td[1]" 	// 8/05
						"/section[1]/table[1]/tr[1]/td[2]" 	// 3:15 PM EDT
						"/section[1]/table[1]/tr[1]/td[4]"	// <a href="#" onclick='launchPlayer("218891", "", "", ""); return false;' class="watchable">American Le Mans Series Mid-Ohio Sports Car Challenge</a>
						"/section[1]/table[1]/tr[1]/td[5]"	// <span class="channel-logo channel-logo-espn3">espn3</span>
					*/

					string eventName = tableRow.ChildNodes[4].InnerText.Replace("\n\t\t\t", string.Empty).Trim();
					DateTime eventDT = DateTime.ParseExact(string.Concat(FormatDate(tableRow.ChildNodes[1].InnerText), FormatTime(tableRow.ChildNodes[2].InnerText)), "MMddhh:mm tt", null);

					Match m = new Match()
					{
						OccurrenceType = ListingType.REPLAY,
						Occurrence = eventDT,
						/*Date = (FormatDate(tableRow.ChildNodes[1].InnerText)),// + "2011",
						Time = FormatTime(tableRow.ChildNodes[2].InnerText),*/
						Category = node.InnerText,
						League = tableRow.ChildNodes[5].InnerText,
						Name = GetShortName(eventName),
						Description = eventName,
						VideoUrl = GetMatchVideoURL(tableRow.ChildNodes[4].InnerHtml)
					};

					l.Matches.Add(m);
				}

				s.Leagues.Add(l);
				sportsCollection.Add(s);
			}

			return sportsCollection;
		}

		#endregion
	}
}
