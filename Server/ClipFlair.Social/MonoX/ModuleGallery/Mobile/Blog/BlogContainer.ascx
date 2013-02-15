<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="BlogContainer.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.Blog.BlogContainer" %>

<%@ Register TagPrefix="MonoX" TagName="BlogPostList" Src="~/MonoX/ModuleGallery/Mobile/Blog/BlogPostList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogPostEdit" Src="~/MonoX/ModuleGallery/Blog/BlogPostEdit.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogPostView" Src="~/MonoX/ModuleGallery/Mobile/Blog/BlogPostView.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogSettings" Src="~/MonoX/ModuleGallery/Blog/BlogSettings.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogComments" Src="~/MonoX/ModuleGallery/Mobile/Blog/BlogCommentsList.ascx" %>

<MonoX:BlogPostList ID="blogPostList" runat="server" RssEnabled="true" ParseCustomTags="Description" />
<MonoX:BlogPostEdit ID="blogPostEdit" runat="server" />
<MonoX:BlogPostView ID="blogPostView" runat="server" />
<MonoX:BlogSettings ID="blogSettings" runat="server" />
<MonoX:BlogComments ID="blogComments" runat="server" />
