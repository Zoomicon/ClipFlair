<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotoListView.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.PhotoListView" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/Comments.ascx" TagPrefix="MonoX" TagName="Comments" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>

<asp:Panel ID="pnlContainer" runat="server">
    <h3><asp:Literal ID="labAlbumName" runat="server"></asp:Literal></h3>
    <div class="photo-nav">
        <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="javascript:void(0);" CssClass="back-link top-links"></asp:HyperLink>
        <span id="panOrganize" runat="server" visible="false"><asp:LinkButton ID="lnkOrganize" runat="server" CssClass="top-links" ></asp:LinkButton></span>
        <asp:LinkButton ID="lnkAddMore" runat="server" CssClass="top-links"></asp:LinkButton>        
        <div style="float: right; display: inline;"><asp:Panel ID="panTellAFriend" runat="server"></asp:Panel></div>
    </div>    
    <div id="rowGallery" runat="server" class="photo-gallery-preview">
        <asp:ListView ID="lvItems" runat="server" GroupItemCount="4">
            <LayoutTemplate>
                <table cellpadding="0" cellspacing="0">
                    <asp:PlaceHolder runat="server" ID="groupPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </tr>
            </GroupTemplate>
            <ItemTemplate>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <mono:pager runat="server" id="pager" pagesize="8" numericbuttoncount="5" allowcustompaging="true"
        autopaging="false">
        <PagerTemplate>
        </PagerTemplate>
    </mono:pager>
    <strong class="comments-title padding1-after" id="labComments" runat="server"><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.PhotoGallery_PhotoListView_Comments%></strong></span>
    <MonoX:Comments ID="comments" runat="server" PagingEnabled="true" CommentTextBoxVisibleOnInit="true"></MonoX:Comments>
</asp:Panel>
