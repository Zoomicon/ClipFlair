<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.SingleNewsModule" Codebehind="SingleNewsModule.ascx.cs" %>
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/Comments.ascx" TagPrefix="MonoX" TagName="Comments" %>
<%@ Register Src="~/MonoX/ModuleGallery/RelatedContent.ascx" TagPrefix="MonoX" TagName="RelatedContent" %>

<div width="100%" class="<%= CssClass %>">
    <div class="NewsData">
        <asp:Repeater ID="singleNewsHolder" runat="server">
            <ItemTemplate>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    
    <div id="pagerHolder" runat="server">
        
    </div>
    
    <div>
        <MonoX:RelatedContent id="relatedContent" runat="server"></MonoX:RelatedContent>
    </div>
    
    <div class="comments">
        <MonoX:Comments ID="comments" runat="server" PagingEnabled="true" CommentTextBoxVisibleOnInit="true"></MonoX:Comments>            
    </div>
</div>