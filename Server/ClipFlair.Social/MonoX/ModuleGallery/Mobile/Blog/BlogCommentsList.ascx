<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogCommentsList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.Blog.BlogCommentsList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>

<asp:Panel ID="pnlContainer" runat="server">
<asp:Panel ID="pnlButtons" runat="server">
    <div style="position: relative;">
        <h1><%= MonoSoftware.MonoX.Resources.BlogResources.Comments_Comments %></h1>
        <div style="overflow: hidden;"></div>
        <div class="blog-action-container">
            <div class="comments-option">
                <asp:LinkButton runat="server" ID="btnDeleteSpam"></asp:LinkButton><asp:Literal runat="server" ID="ltlSeparator"></asp:Literal>
                <asp:LinkButton runat="server" ID="btnDeleteUnapproved"></asp:LinkButton>
            </div>
        </div>
        <div class="arrow-down arow-position"></div>
    </div>
</asp:Panel>

<asp:ListView ID="lvItems" runat="server">
    <LayoutTemplate>
        <ul data-role="listview">
        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </ul>
    </LayoutTemplate>
    <ItemTemplate></ItemTemplate>
</asp:ListView>
<br />
<mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
</mono:Pager>
</asp:Panel>
