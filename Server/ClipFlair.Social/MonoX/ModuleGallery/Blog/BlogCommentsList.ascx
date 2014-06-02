<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="BlogCommentsList.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogCommentsList" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>
<%@ Register TagPrefix="MonoX" TagName="LightBox" Src="~/MonoX/controls/LightBox.ascx" %>

<asp:Panel ID="pnlContainer" runat="server">
    <!--CLIPFLAIR-->
    <div class="clearfix">
        <h2><%= MonoSoftware.MonoX.Resources.BlogResources.Comments_Comments %></h2>
        <asp:Panel ID="pnlButtons" runat="server">
            <div class="comment-top clearfix" runat="server" ID="lbHeader">            
                <div class="inner small-btn">
                    <asp:LinkButton runat="server" ID="btnDeleteSpam" CssClass="styled-button delete-btn float-left"></asp:LinkButton>
                    <asp:Literal runat="server" ID="ltlSeparator"></asp:Literal>
                    <asp:LinkButton runat="server" ID="btnDeleteUnapproved" CssClass="styled-button delete-btn float-left"></asp:LinkButton>
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:ListView ID="lvItems" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate></ItemTemplate>
    </asp:ListView>
    <mono:Pager runat="server" ID="pager" PageSize="6" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
        <PagerTemplate></PagerTemplate>
    </mono:Pager>
</asp:Panel>