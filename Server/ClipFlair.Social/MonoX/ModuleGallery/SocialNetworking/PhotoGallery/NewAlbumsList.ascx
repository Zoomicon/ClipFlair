<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewAlbumsList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.NewAlbumsList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>

<div class="photos">
    <asp:Panel ID="pnlContainer" CssClass="FriendCenterAlign" runat="server">
    <h3><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.PhotoGallery_AlbumView_NewAlbums %></h3>
    <div class="photo-gallery">
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
    </asp:Panel>
    <div style="clear:both">
    <mono:Pager runat="server" ID="pager" PageSize="4" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
    </mono:Pager>
    </div>
</div>