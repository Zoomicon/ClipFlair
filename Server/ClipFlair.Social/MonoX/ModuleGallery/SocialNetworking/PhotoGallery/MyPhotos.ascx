<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyPhotos.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.MyPhotos" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/Comments.ascx" TagPrefix="MonoX" TagName="Comments" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>

<asp:Panel ID="pnlContainer" runat="server">
    <div id="rowGallery" runat="server" class="photo-gallery-preview">
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
    <asp:Label runat="server" ID="lblMessage" CssClass="empty-list"></asp:Label>
    <div style="clear:both">
    <mono:pager runat="server" id="pager" pagesize="8" numericbuttoncount="5" allowcustompaging="true"
        autopaging="false">
        <PagerTemplate>
        </PagerTemplate>
    </mono:pager>
    </div>
</asp:Panel>
