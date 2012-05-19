<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoPlayer.aspx.cs" Inherits="ESPN3Web.VideoPlayer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body scroll="no" style="margin: 0; padding: 0; overflow: hidden;">
	<form id="form1" runat="server">
		<div id="e3p-flash-container"></div>
	</form>
	<script src="https://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js" type="text/javascript"></script>
	<script language="javascript" type="text/javascript">
		/*
		swfobject.embedSWF(swfUrl, id, width, height, version, expressInstallSwfurl, flashvars, params, attributes, callbackFn)
		1. swfUrl (String, required) specifies the URL of your SWF
		2. id (String, required) specifies the id of the HTML element (containing your alternative content) you would like to have replaced by your Flash content
		3. width (String, required) specifies the width of your SWF
		4. height (String, required) specifies the height of your SWF
		5. version (String, required) specifies the Flash player version your SWF is published for (format is: "major.minor.release" or "major")
		6. expressInstallSwfurl (String, optional) specifies the URL of your express install SWF and activates Adobe express install. Please note that express install will only fire once (the first time that it is invoked), that it is only supported by Flash Player 6.0.65 or higher on Win or Mac platforms, and that it requires a minimal SWF size of 310x137px.
		7. flashvars (Object, optional) specifies your flashvars with name:value pairs
		8. params (Object, optional) specifies your nested object element params with name:value pairs
		9. attributes (Object, optional) specifies your object's attributes with name:value pairs
		10. callbackFn (JavaScript function, optional) can be used to define a callback function that is called on both success or failure of embedding a SWF file (see API documentation)
		*/

		var eanUser = true;
		var flashvars = {

			configUrl: "http://espn.go.com/watchespn/player/config?eanUser=" + eanUser + "%26passenv=prod%26%>",

			playerSize: resolution, id: videoId, channel: "espn3"
		};
		var params = {
			allowScriptAccess: "always",
			allowFullScreen: "true"
		};
		var attributes = {
			id: "${application}",
			name: "${application}",
			align: "middle"
		};

		swfobject.embedSWF('http://espn.go.com/watchespn/player.swf', 'e3p-flash-container', resolutionWidth, resolutionHeight, '10.2', 'http://a.espncdn.com/espn360/3/playerProductInstall.swf', flashvars, params, attributes);
	</script>
</body>
</html>
