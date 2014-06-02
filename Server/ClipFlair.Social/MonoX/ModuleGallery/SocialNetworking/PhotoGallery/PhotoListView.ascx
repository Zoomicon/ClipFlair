<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="PhotoListView.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.PhotoListView" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/Comments.ascx" TagPrefix="MonoX" TagName="Comments" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>

<asp:Panel ID="pnlContainer" runat="server">
    <div class="clearfix">
        <h2><asp:Literal ID="labAlbumName" runat="server"></asp:Literal></h2>            
    </div>
    <div class="clearfix">       
        <div class="span12">           
            <% if (!IsPhotoPreview)
               { %>
            <span class="album_description"><%= (AlbumEntity != null ? AlbumEntity.Description : String.Empty)%></span>
            <% } %>
        </div>

    </div>
    <div class="photo-list clearfix">
        <div class="main-options">
            <div class="inner small-btn">
                <asp:LinkButton ID="lnkAddMore" runat="server" CssClass="styled-button main-button add-btn float-left"></asp:LinkButton>
                <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="javascript:void(0);" CssClass="styled-button back-btn float-left"></asp:HyperLink>
                <span id="panOrganize" runat="server" visible="false"><asp:LinkButton ID="lnkOrganize" runat="server" CssClass="styled-button float-left"></asp:LinkButton></span>
                <asp:Panel ID="panTellAFriend" runat="server"></asp:Panel>
            </div>
        </div>
        <div id="rowGallery" runat="server">
            <asp:ListView ID="lvItems" runat="server" GroupItemCount="4">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="groupPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>
                <GroupTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </GroupTemplate>
                <ItemTemplate>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <mono:pager runat="server" id="pager" pagesize="7" numericbuttoncount="5" allowcustompaging="true" autopaging="false">
                <PagerTemplate>
                </PagerTemplate>
            </mono:pager>
            <div class="comments">
                <h3 id="labComments" runat="server"><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.PhotoGallery_PhotoListView_Comments%></h3>
                <MonoX:Comments ID="comments" runat="server" PagingEnabled="true" CommentTextBoxVisibleOnInit="true"></MonoX:Comments>
            </div>
        </div>
    </div>
</asp:Panel>
