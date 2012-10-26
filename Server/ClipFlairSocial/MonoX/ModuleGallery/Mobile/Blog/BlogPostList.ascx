<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogPostList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.Blog.BlogPostList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register TagPrefix="MonoX" TagName="LightBox" Src="~/MonoX/controls/LightBox.ascx" %>
<%@ Register Src="~/MonoX/controls/SyntaxHighlighter/SyntaxHighlighter.ascx" TagPrefix="mono"
    TagName="SyntaxHighlighter" %>


<mono:SyntaxHighlighter ID="syntaxHighlighter" runat="server" />
    <asp:Panel ID="pnlContainer" runat="server">
        <div class="blog-top">
            <asp:Literal runat="server" ID="ltlH1Open"><h1></asp:Literal><asp:Literal ID="ltlBlogName" runat="server"></asp:Literal><asp:Literal runat="server" ID="ltlH1Close"></h1></asp:Literal>
            <div style="margin-bottom: 0px; overflow: hidden;"><asp:Literal ID="ltlBlogDescription" runat="server"></asp:Literal></div>
            <asp:Panel runat="server" ID="pnlCommands">
            <div class="blog-action-container">
                <ul class="first" id="panFilter" runat="server">
                    <li title="<%= MonoSoftware.MonoX.Resources.BlogResources.BlogPostList_Filter_Title %>">
                        <asp:HyperLink ID="lnkCurrent" runat="server" ></asp:HyperLink>
                        <ul class="level0">
                            <li><asp:HyperLink ID="lnkFirst" runat="server" ></asp:HyperLink></li>
                            <li><asp:HyperLink ID="lnkSecond" runat="server" ></asp:HyperLink></li>
                        </ul>
                    </li>
                </ul>
                <asp:HyperLink runat="server" ID="lnkComments" CssClass="BlogComments MarginBottom"></asp:HyperLink>
                <asp:HyperLink runat="server" ID="lnkSettings" CssClass="BlogSettings MarginBottom" Visible="false"></asp:HyperLink>
                <asp:HyperLink runat="server" ID="lnkAddNew" CssClass="AddBlogPost MarginBottom" Visible="false"></asp:HyperLink>
            </div>
            </asp:Panel>
        </div>
    <asp:Literal runat="server" ID="ltlHeaderSpace"></asp:Literal>  
    <asp:ListView ID="lvItems" runat="server">
        <LayoutTemplate>
            <ul data-role="listview">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
        </ItemTemplate>
    </asp:ListView>
    <asp:Literal runat="server" ID="ltlNoData"></asp:Literal>
    <br />
    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
        <PagerTemplate>
        </PagerTemplate>
    </mono:Pager>
</asp:Panel>
