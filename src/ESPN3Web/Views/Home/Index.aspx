<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <div id="e3p-flash-container" style="width: 768px; height: 432px;">
        <!--iframe src="http://espn.go.com/watchespn/player/_/id/246413/" width="100%" height="800" id="frameESPN"></iframe-->
        <object data="http://espn.go.com/watchespn/player.swf" name="" id="" type="application/x-shockwave-flash" align="middle" height="100%" width="100%">
            <param value="always" name="allowScriptAccess">
            <param value="true" name="allowFullScreen">
            <param value="configUrl=http://espn.go.com/watchespn/player/config&amp;playerSize=768x432&amp;id=246413" name="flashvars">
        </object>
    </div>
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

        var g_playerSize = "768x432";
        
        var flashvars = {
            configUrl: "http://espn.go.com/watchespn/player/config",
            playerSize: g_playerSize, id: "246413"
        };
        var params = {
            allowScriptAccess: "always",
            allowFullScreen: "true"
        };
        var attributes = {
            id: "123",
            name: "123",
            align: "middle"
        };
        //swfobject.embedSWF('http://espn.go.com/watchespn/player.swf', 'e3p-flash-container', '100%', '100%', '10.2', 'http://a.espncdn.com/espn360/3/playerProductInstall.swf', flashvars, params, attributes);
    </script> 
</asp:Content>
