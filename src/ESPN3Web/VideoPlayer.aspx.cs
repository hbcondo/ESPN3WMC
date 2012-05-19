using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace ESPN3Web
{
	public partial class VideoPlayer : System.Web.UI.Page
	{	
		protected void Page_Load(object sender, EventArgs e)
		{
			string qs = Request.Url.Query;
			NameValueCollection items = HttpUtility.ParseQueryString(HttpUtility.HtmlDecode(qs));

			string resolution = items["r"];			
			string[] screenDimensions = resolution.Split('x');
			string height = screenDimensions[1];
			string width = screenDimensions[0];
			string id = items["i"];
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
			sb.Append("<script language=\"javascript\">");
			sb.AppendFormat("var videoId = '{0}'; ", id);
			sb.AppendFormat("var resolution = '{0}'; ", resolution);
			sb.AppendFormat("var resolutionHeight = '{0}'; ", height);
			sb.AppendFormat("var resolutionWidth = '{0}'; ", width);
			sb.Append("</script>");

			ClientScript.RegisterClientScriptBlock(this.GetType(), "JSScriptBlock", sb.ToString());
		}
	}
}