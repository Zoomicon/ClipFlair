<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlbumList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.AlbumList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager"
    TagPrefix="mono" %>
<asp:Panel ID="pnlContainer" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <h3><asp:Label ID="labTitle" runat="server"></asp:Label></h3>
            <div class="photo-nav">
                <asp:LinkButton ID="lnkCreateAlbum" runat="server" CssClass="top-links create-album"></asp:LinkButton>
                <asp:HyperLink ID="lnkAlbums" runat="server" CssClass="top-links"></asp:HyperLink>
                <asp:HyperLink ID="lnkMyAlbums" runat="server" CssClass="top-links"></asp:HyperLink>
            </div>
            <div id="rowGallery" runat="server" class="photo-gallery">                
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
            <mono:Pager runat="server" ID="pager" PageSize="8" NumericButtonCount="5" AllowCustomPaging="true"
                AutoPaging="false">
                <PagerTemplate>
                </PagerTemplate>
            </mono:Pager>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
