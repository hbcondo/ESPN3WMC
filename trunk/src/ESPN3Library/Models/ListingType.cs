using System;

namespace ESPN3Library.Models
{
    public enum ListingType
    {
        LIVE
        , REPLAY
        , UPCOMING
// <event id="226309" type="nexdefreplay" bamContentId="18805033" bamEventId="176-12567" mediaState="MEDIA_DONE"><gameId>null</gameId><ttl>0</ttl><name>Winnipeg Blue Bombers vs. Saskatchewan Roughriders</name><league>CFL</league><sport code="FB">FOOTBALL</sport><sportDisplayValue>FOOTBALL</sportDisplayValue><showId>756</showId><program code="CL"/><networkId>n360</networkId><caption/><site>Mosaic Stadium Regina, Sk Canada</site><summary/><espnSportId>33</espnSportId><gameStatus/><startTime>201109041600</startTime><endTime>201109041900</endTime><startTimeGmtMs>1315166400000</startTimeGmtMs><endTimeGmtMs>1315177200000</endTimeGmtMs><checkBlackout>false</checkBlackout><thumbnail><large>http://a.espncdn.com/espn360/images/fb/cfl/226309.jpg</large><small>http://a.espncdn.com/espn360/images/fb/cfl/226309_small.jpg</small></thumbnail><aspectRatio>16:9</aspectRatio><language>English</language><cc>False</cc><ccPosition>bottomLeft</ccPosition><multilanguageEvents/></event>
		, NEXDEFREPLAY
        , UNKNOWN = 99
    }
}
