using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace ESPN3Web
{
	/// <summary>
	/// Summary description for Handler
	/// </summary>
	public class Handler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			string html = "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Strict//EN\"><html><body><object data=\"http://assets.espn.go.com/espn360/builds/espn3-player-web-app/1_25/espn3-player-web-app-1.25.swf\" type=\"application/x-shockwave-flash\" height=\"{2}\" width=\"{3}\" align=\"middle\"><param value=\"always\" name=\"allowScriptAccess\"><param value=\"true\" name=\"allowFullScreen\"><param value=\"configUrl=http://espn.go.com/watchespn/player/config?eanUser=true%26passenv=prod%26%&gt;&amp;playerSize={0}&amp;id={1}&amp;channel=espn3\" name=\"flashvars\"></object></body></html>";

			string qs = context.Request.Url.Query;
			NameValueCollection items = HttpUtility.ParseQueryString(HttpUtility.HtmlDecode(qs));
			string resolution = items["r"];
			string[] screenDimensions = resolution.Split('x');
			string height = screenDimensions[1];
			string width = screenDimensions[0];
			string id = items["i"];

			context.Response.ContentType = "text/HTML";
			context.Response.Write(string.Format(html, resolution, id, height, width));
			/*context.Response.Write(qs + "<br />");
			context.Response.Write(resolution + "<br />");
			context.Response.Write(id);*/
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}