<%@ Control
    Language="C#" 
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

<asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <monox:discussionboard runat="server" id="dBoard" title="Discussion board" />
        <monox:discussiontopic runat="server" id="dTopic" />
        <monox:discussiontopic runat="server" id="dFilterTopic" />
        <monox:discussionmessages runat="server" id="dMessages" />
    </ContentTemplate>
</asp:UpdatePanel>
