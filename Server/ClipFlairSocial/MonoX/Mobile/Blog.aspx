<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Blog.aspx.cs"
    Inherits="MonoSoftware.MonoX.Mobile.Blog"
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>  
<%@ Register TagPrefix="MonoX" TagName="BlogContainer" Src="~/MonoX/ModuleGallery/Mobile/Blog/BlogContainer.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always" >
        <ContentTemplate>
            <MonoX:BlogContainer ID="blogContainer" runat="server" UsePrettyPhoto="true" DateFormatString="d" 
            RelatedContentVisible="false" EnableSyntaxHighlighter="false" GravatarRenderType="NotSet" />
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>