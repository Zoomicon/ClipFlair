<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogCommentsList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogCommentsList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>
<%@ Register TagPrefix="MonoX" TagName="LightBox" Src="~/ClipFlair/Controls/LightBox.ascx" %>

<asp:Panel ID="pnlContainer" runat="server">
<asp:Panel ID="pnlButtons" runat="server">
    <MonoX:LightBox runat="server" ID="lbHeader">
        <ContentTemplate>
            <div style="position: relative;">
                <h1 class="padding1-after"><%= MonoSoftware.MonoX.Resources.BlogResources.Comments_Comments %></h1>
                <!--<div style="overflow: hidden;"></div>-->
                <div class="blog-action-container">
                    <div class="comments-option">
                        <asp:LinkButton runat="server" ID="btnDeleteSpam"></asp:LinkButton><asp:Literal runat="server" ID="ltlSeparator"></asp:Literal>
                        <asp:LinkButton runat="server" ID="btnDeleteUnapproved"></asp:LinkButton>
                    </div>
                </div>
                <!--<div class="arrow-down arow-position"></div>-->
            </div>
        </ContentTemplate>
    </MonoX:LightBox>  
</asp:Panel>

<asp:ListView ID="lvItems" runat="server">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
    </LayoutTemplate>
    <ItemTemplate></ItemTemplate>
</asp:ListView>
<mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
</mono:Pager>
</asp:Panel>
