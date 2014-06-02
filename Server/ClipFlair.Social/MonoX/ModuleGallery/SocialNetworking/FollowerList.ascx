<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="FollowerList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.FollowerList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>

<asp:Panel ID="pnlContainer" runat="server">
<asp:ListView ID="lvItems" runat="server">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
    </LayoutTemplate>
    <ItemTemplate></ItemTemplate>
</asp:ListView>
</asp:Panel>
<div style="clear:both">
<mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
</mono:Pager>
</div>
