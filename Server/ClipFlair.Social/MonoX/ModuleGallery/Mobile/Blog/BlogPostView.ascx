<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogPostView.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.Blog.BlogPostView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="MonoX" TagName="BlogComments" Src="~/MonoX/ModuleGallery/Mobile/Blog/BlogComments.ascx" %>
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
    <MonoX:RelatedContent id="relatedContent" runat="server" Visible="false"></MonoX:RelatedContent>
    <MonoX:BlogComments runat="server" ID="ctlComments">
    </MonoX:BlogComments>
</asp:Panel>
