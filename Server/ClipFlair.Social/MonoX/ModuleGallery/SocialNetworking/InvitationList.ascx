<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="InvitationList.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.InvitationList" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>

<asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:Panel ID="pnlContainer" runat="server">
        <asp:Literal runat="server" ID="ltlCaption" Visible="false"></asp:Literal>
        <asp:ListView ID="lvItems" runat="server">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </LayoutTemplate>
            <ItemTemplate></ItemTemplate>
        </asp:ListView>
        </asp:Panel>
        <div style="background: red;"><asp:Literal runat="server" ID="ltlMessage"></asp:Literal></div>
        <div style="clear:both">
            <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
                <PagerTemplate></PagerTemplate>
            </mono:Pager>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
