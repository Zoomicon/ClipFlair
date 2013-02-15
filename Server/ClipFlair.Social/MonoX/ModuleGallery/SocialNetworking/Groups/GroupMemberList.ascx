﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupMemberList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupMemberList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>

<asp:Panel ID="pnlContainer" CssClass="FriendCenterAlign" runat="server">
<asp:ListView ID="lvItems" runat="server">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
    </LayoutTemplate>
    <ItemTemplate></ItemTemplate>
</asp:ListView>
</asp:Panel>
<asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
<div style="clear:both">
<mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
</mono:Pager>
</div>