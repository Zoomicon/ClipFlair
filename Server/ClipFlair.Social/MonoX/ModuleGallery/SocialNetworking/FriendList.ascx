<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.FriendList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>

<asp:Panel ID="pnlContainer" runat="server">
<asp:ListView ID="lvItems" runat="server">
    <LayoutTemplate>
        <div class="clearfix">
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </div>
    </LayoutTemplate>
    <ItemTemplate></ItemTemplate>
</asp:ListView>
</asp:Panel>
<asp:Label runat="server" ID="lblMessage" CssClass="empty-list"></asp:Label>
<div style="clear:both">
<mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
</mono:Pager>
</div>