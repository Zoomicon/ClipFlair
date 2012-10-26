<%@ Control Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="DiscussionContainer.ascx.cs" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.DiscussionContainer" %>

<%@ Register TagPrefix="MonoX" TagName="DiscussionBoard" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionBoard.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="DiscussionTopic" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionTopics.ascx" %>  
<%@ Register TagPrefix="MonoX" TagName="DiscussionMessages" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionMessages.ascx" %>  

<asp:PlaceHolder ID="panNotification" runat="server">
    <div class="message-sep-line">
        <asp:HyperLink ID="lnkBack" runat="server" CssClass="discussion-link back-link"></asp:HyperLink>
    </div>
    <div class="discussion-permission"><asp:Label ID="labNotificationMessage" runat="server"></asp:Label></div>
</asp:PlaceHolder>

<div class="discussion-container">
    <MonoX:DiscussionBoard runat="server" ID="dBoard" Title="Discussion board" />
    <MonoX:DiscussionTopic runat="server" ID="dTopic" />
    <MonoX:DiscussionTopic runat="server" ID="dFilterTopic" />
    <MonoX:DiscussionMessages runat="server" ID="dMessages" />
</div>