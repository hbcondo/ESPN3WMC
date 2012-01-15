using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HtmlAgilityPack;

namespace ESPN3Web
{
    /// <summary>
    /// Summary description for Handler
    /// </summary>
    public class Handler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HtmlWeb doc = new HtmlWeb();
            HtmlDocument rawData = doc.Load("http://espn.go.com/watchespn/player/_/id/226213/");    
            
            context.Response.ContentType = "text/HTML";
            context.Response.Write(rawData.DocumentNode.InnerHtml);
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