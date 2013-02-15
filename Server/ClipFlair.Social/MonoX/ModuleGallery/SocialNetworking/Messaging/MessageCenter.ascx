<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageCenter.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.MessageCenter" %>
<%@ Register TagPrefix="MonoX" TagName="MessageList" Src="~/MonoX/ModuleGallery/SocialNetworking/Messaging/MessageList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="MessageCreate" Src="~/MonoX/ModuleGallery/SocialNetworking/Messaging/MessageCreate.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="MessageDetails" Src="~/MonoX/ModuleGallery/SocialNetworking/Messaging/MessageDetails.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="MonoSoftware.MonoX.Utilities"  %>

<asp:UpdatePanel ID="ajaxPanelMain" runat="server" UpdateMode="Always">
<ContentTemplate>
<div style="width:100%" class="message-center">
<telerik:RadTabStrip CausesValidation="false" ID="tabStrip" runat="server" Skin="MonoxTabStrip" EnableEmbeddedSkins="false" Width="100%" MultiPageID="multiPage" SelectedIndex="0" AutoPostBack="True" ontabclick="tabStrip_Click">
            <Tabs>
                <telerik:RadTab runat="server" ID="tabMessages" Selected="True" Width="20%"></telerik:RadTab>
                <telerik:RadTab runat="server" ID="tabMessagesSent" Width="20%"></telerik:RadTab>
                <telerik:RadTab runat="server" ID="tabMessageDetails" Width="0%"></telerik:RadTab>
                <telerik:RadTab Text="&nbsp;" Width="39%" Height="1%" IsSeparator="true"></telerik:RadTab>
                <telerik:RadTab runat="server" ID="tabNewMessage" Width="20%"></telerik:RadTab>
            </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" RenderSelectedPageOnly="True">
    <telerik:RadPageView ID="pageViewMessages" runat="server">
        <MonoX:MessageList runat="server" ID="messageList" MessageStatusFilter="Received" Title="Received messages" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="pageViewSent" runat="server">
        <MonoX:MessageList runat="server" ID="messageListSent" MessageStatusFilter="Sent" Title="Sent messages" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="pageViewMessageDetails" runat="server">
        <MonoX:MessageDetails runat="server" ID="messageDetails" MessageStatusFilter="Sent" Title="Display messages" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="pageViewSeparator" runat="server">
    </telerik:RadPageView>
    <telerik:RadPageView ID="pageViewMessageCreate" runat="server">
        <MonoX:MessageCreate runat="server" ID="messageCreate" Title="New message" />
    </telerik:RadPageView>
</telerik:RadMultiPage>
</div>
</ContentTemplate>
</asp:UpdatePanel>
