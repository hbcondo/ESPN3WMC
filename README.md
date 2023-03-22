# ESPN3WMC
By Amar Kota - [Hire me](https://amarkota.com/resume)

![espn3](https://user-images.githubusercontent.com/306958/226764991-9c4050ac-6e1d-499f-804b-aaff48c5680e.png)

I wrote ESPN3WMC back in 2012 when I used Windows Media Center (WMC)[^1] to watch live TV and videos on my television using an HTPC[^2] I built. Sadly, WMC has been discontinued by Microsoft and the sports data is no longer available but I all of the sudden remembered by old SVN instance[^3] and finally got around to publishing the plugin's source code.

## Overview
Using a format similar to the Fox Sports On Later section of media center, this application displays an extensive list of sporting events available to watch on-demand. The application consumes a web service which returns the espn3 listing data and is rendered in the Media Center Markup Language (MCML). A windows installer application was also developed so users can run a MSI file on the computer to get the application to install within Media Center easily.

![espn3_1](https://user-images.githubusercontent.com/306958/226765677-72df5acc-140c-4262-9e2d-ac5ed4311ae9.gif)|

## Web Service
The web service obtains sporting event videos on-demand from ESPN3 and serializes the data into a XML format for easy consumption by the media center application. The data includes both live and replayed sporting events that aired on ESPN. The web service was developed using the .NET Framework written in C#.

![espn3_2](https://user-images.githubusercontent.com/306958/226766863-b009711a-85cf-46a1-8089-b9c3ba68e469.gif)

## Loading Page
When the ESPN3 add-in is launched, a loading screen is presented to the user while in the background the add-in obtains the sporting event data from the web service. When the data has been retrieved, the loading screen disappears and the user is presented with a selection of sporting events to watch. The loading screen was implemented by using MCML's HistoryOrientedPageSession class which is derived from the PageSession class. This PageSession class has a LoadPage method that is used to display a MCML page that will not be added to Media Center's back stack. This in turns prevents the loading screen being presented again if the user navigates back.

![espn3_3](https://user-images.githubusercontent.com/306958/226766932-f30d86b1-bd23-4c7f-a49d-273682be7464.gif)

## Class Library
A class library with business objects that encapsulates all the data and business behavior associated with the ESPN3 application. Written in C# with Visual Studio and used within the web service and ESPN3 media center application.

## Additional Info
- [MissingRemote.com](https://www.missingremote.com/review/2011/04/espn3wmc-watch-espn3-broadcasts-on-your-hdtv-via-media-center-remote-kind-of)
- [AmarKota.com](https://www.amarkota.com/portfolio/espn3)
- [ESPN3](https://en.wikipedia.org/wiki/ESPN3)

[^1]: https://en.wikipedia.org/wiki/Windows_Media_Center
[^2]: https://www.reddit.com/r/htpc/
[^3]: https://assembla.com/
