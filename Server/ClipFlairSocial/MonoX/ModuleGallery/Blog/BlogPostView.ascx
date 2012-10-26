<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogPostView" Codebehind="BlogPostView.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="MonoX" TagName="BlogComments" Src="~/MonoX/ModuleGallery/Blog/BlogComments.ascx" %>
<%@ Register Src="~/MonoX/controls/SyntaxHighlighter/SyntaxHighlighter.ascx" TagPrefix="mono"
    TagName="SyntaxHighlighter" %>
<%@ Register Src="~/MonoX/ModuleGallery/RelatedContent.ascx" TagPrefix="MonoX" TagName="RelatedContent" %>

<mono:SyntaxHighlighter ID="syntaxHighlighter" runat="server" />

<asp:Panel ID="pnlContainer" runat="server">
    <asp:UpdatePanel ID="updatePanel" ChildrenAsTriggers="true" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlBlogPost" runat="server">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <MonoX:RelatedContent id="relatedContent" runat="server"></MonoX:RelatedContent>
    <MonoX:BlogComments runat="server" ID="ctlComments">
    </MonoX:BlogComments>
</asp:Panel>
