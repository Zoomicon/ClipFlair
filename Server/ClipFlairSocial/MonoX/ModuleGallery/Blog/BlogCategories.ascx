<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogCategories.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogCategories" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>

<asp:Panel ID="pnlContainer" runat="server">
    <div class="blog-categories">
        <asp:ListView ID="lvItems" runat="server">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
    </LayoutTemplate>
    <ItemTemplate></ItemTemplate>
        </asp:ListView>
        <asp:Literal runat="server" ID="ltlNoData"></asp:Literal>
        <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
        </mono:Pager>
    </div>
</asp:Panel>