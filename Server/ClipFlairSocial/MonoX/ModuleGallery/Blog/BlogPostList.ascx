<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogPostList"
    CodeBehind="BlogPostList.ascx.cs" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register TagPrefix="MonoX" TagName="LightBox" Src="~/ClipFlair/Controls/LightBox.ascx" %>
<%@ Register Src="~/MonoX/controls/SyntaxHighlighter/SyntaxHighlighter.ascx" TagPrefix="mono"
    TagName="SyntaxHighlighter" %>

<mono:SyntaxHighlighter ID="syntaxHighlighter" runat="server" />

    <asp:Panel ID="pnlContainer" runat="server">    
        <MonoX:LightBox runat="server" ID="lbHeader">
            <ContentTemplate>
            <div style="position: relative;">
                <asp:Literal runat="server" ID="ltlH1Open"><h1></asp:Literal><asp:Literal ID="ltlBlogName" runat="server"></asp:Literal><asp:Literal runat="server" ID="ltlH1Close"></h1></asp:Literal>
                <div style="margin-bottom: 0px; overflow: hidden;" class="padding1-after"><asp:Literal ID="ltlBlogDescription" runat="server"></asp:Literal></div>
                <div class="blog-action-container padding1-before">
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
                    <asp:HyperLink runat="server" ID="lnkSettings" CssClass="BlogSettings MarginBottom"></asp:HyperLink>
                    <asp:HyperLink runat="server" ID="lnkAddNew" CssClass="AddBlogPost MarginBottom"></asp:HyperLink>
                </div>
                <!-- <div class="arrow-down arow-position"></div> -->
            </div>
            
        </ContentTemplate>
        </MonoX:LightBox>  

    <asp:Literal runat="server" ID="ltlHeaderSpace"><br /></asp:Literal>  
    <asp:ListView ID="lvItems" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
        </ItemTemplate>
    </asp:ListView>
    <asp:Literal runat="server" ID="ltlNoData"></asp:Literal>
    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true"
        AutoPaging="false">
        <PagerTemplate>
        </PagerTemplate>
    </mono:Pager>
</asp:Panel>
