<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewGroupsList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.NewGroupsList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>

<div class="social-groups">
    <asp:Panel ID="pnlContainer" runat="server">
    <asp:ListView ID="lvItems" runat="server">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
    </LayoutTemplate>
    <ItemTemplate></ItemTemplate>
    </asp:ListView>
    </asp:Panel>
    <asp:Literal runat="server" ID="ltlNoData"></asp:Literal>
    <div style="clear:both">
    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
    </mono:Pager>
    </div>
</div>