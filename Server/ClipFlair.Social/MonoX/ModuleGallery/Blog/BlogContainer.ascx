<%@ Control
    Language="C#"
    AutoEventWireup="True"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogContainer"
    Codebehind="BlogContainer.ascx.cs" %>

<%@ Register TagPrefix="MonoX" TagName="BlogPostList" Src="~/MonoX/ModuleGallery/Blog/BlogPostList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogPostEdit" Src="~/MonoX/ModuleGallery/Blog/BlogPostEdit.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogPostView" Src="~/MonoX/ModuleGallery/Blog/BlogPostView.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogSettings" Src="~/MonoX/ModuleGallery/Blog/BlogSettings.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogComments" Src="~/MonoX/ModuleGallery/Blog/BlogCommentsList.ascx" %>

<div class="blog">
    <MonoX:BlogPostList ID="blogPostList" runat="server" RssEnabled="true" ParseCustomTags="Description" />
    <MonoX:BlogPostEdit ID="blogPostEdit" runat="server" />
    <MonoX:BlogPostView ID="blogPostView" runat="server" />
    <MonoX:BlogSettings ID="blogSettings" runat="server" />
    <MonoX:BlogComments ID="blogComments" runat="server" />
</div>