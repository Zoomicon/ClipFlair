<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TwitterFeedDefault.aspx.cs" Inherits="TwitterFeed.TwitterFeedDefault" %>
<%@ Register Src="~/MonoX/Samples/TwitterFeedModule/TwitterFeedModule.ascx" TagPrefix="twitter" TagName="TwitterFeedModule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <twitter:TwitterFeedModule ID="twitterFeedModule" runat="server" ListName="KhorvatTweets" TweetsCount="500" ProfileName="khorvat2" />
    </div>
    </form>
</body>
</html>
