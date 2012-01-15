using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace ESPN3Web
{
	/// <summary>
	/// Summary description for Test
	/// </summary>
	public class Test : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			bool isWorking = false;

			try
			{
				using (WebResponse webResponse = WebRequest.Create("http://espn.go.com/watchespn/index/_/source/espn3/").GetResponse())
				{
					isWorking = true;
				}
			}

			catch(WebException ex)
			{
			}

			context.Response.Write(isWorking.ToString().ToLower());
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