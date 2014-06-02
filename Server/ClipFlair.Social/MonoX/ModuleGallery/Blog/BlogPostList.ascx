<%@ Control
    Language="C#"
    AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogPostList"
    CodeBehind="BlogPostList.ascx.cs" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Src="~/MonoX/controls/SyntaxHighlighter/SyntaxHighlighter.ascx" TagPrefix="mono" TagName="SyntaxHighlighter" %>

<mono:SyntaxHighlighter ID="syntaxHighlighter" runat="server" />
<asp:Panel ID="pnlContainer" runat="server">
    <!--CLIPFLAIR-->
    <div class="blog-top-section clearfix" runat="server" id="lbHeader">
        <div class="blog-info">
            <asp:Literal runat="server" ID="ltlH1Open"><h1></asp:Literal><asp:Literal ID="ltlBlogName" runat="server"></asp:Literal><asp:Literal runat="server" ID="ltlH1Close"></h1></asp:Literal>
            <p class="blog-description"><asp:Literal ID="ltlBlogDescription" runat="server"></asp:Literal></p>
        </div>
        <div class="main-options">
            <ul class="first" id="panFilter" runat="server">
                <li title="<%= MonoSoftware.MonoX.Resources.BlogResources.BlogPostList_Filter_Title %>"> 
                    <asp:HyperLink ID="lnkCurrent" runat="server" CssClass="options-link styled-button list-btn float-left" ></asp:HyperLink>
                    <ul class="level0">
                        <li><asp:HyperLink ID="lnkFirst" runat="server" ></asp:HyperLink></li>
                        <li><asp:HyperLink ID="lnkSecond" runat="server" ></asp:HyperLink></li>
                    </ul>
                </li>
            </ul>
            <!--<asp:HyperLink runat="server" ID="lnkComments" CssClass="styled-button comments-btn float-left"></asp:HyperLink>-->
            <asp:HyperLink runat="server" ID="lnkSettings" CssClass="styled-button options-btn float-left"></asp:HyperLink>            
            <asp:HyperLink runat="server" ID="lnkAddNew" CssClass="styled-button main-button add-btn float-left"></asp:HyperLink>
        </div>
    </div>  
    <asp:Literal runat="server" ID="ltlHeaderSpace"></asp:Literal>  
    <asp:ListView ID="lvItems" runat="server">
        <LayoutTemplate>
            <div class="clearfix">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
        </ItemTemplate>
    </asp:ListView>
    <asp:Label runat="server" ID="lblNoData" CssClass="empty-list"></asp:Label>
    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true"
        AutoPaging="false">
        <PagerTemplate>
        </PagerTemplate>
    </mono:Pager>
</asp:Panel>
