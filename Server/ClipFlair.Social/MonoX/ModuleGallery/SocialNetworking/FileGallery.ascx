<%@ Control
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="FileGallery.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.FileGallery" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radW" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">
</asp:ScriptManagerProxy>
<radW:RadWindowManager ID="wndManager" runat="server">
</radW:RadWindowManager>

<asp:Panel ID="pnlContainer" runat="server">
    <asp:PlaceHolder ID="panTitle" runat="server">
        <h3><asp:Literal ID="labTitle" runat="server" Text="<%$ Code: MonoSoftware.MonoX.Resources.SocialNetworkingResources.FileGallery_AttachedFiles %>"></asp:Literal></h3>
    </asp:PlaceHolder>
    <div class="clearfix">
        <asp:ListView ID="lvItems" runat="server">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="groupPlaceholder"></asp:PlaceHolder>
            </LayoutTemplate>
            <GroupTemplate>
                <div id="panGroup" runat="server" class="<%$ Code: GroupCssClass %>">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </div>
            </GroupTemplate>
            <ItemTemplate>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <mono:Pager runat="server" ID="pager" PageSize="36" NumericButtonCount="5" AllowCustomPaging="true"
        AutoPaging="false">
        <PagerTemplate>
        </PagerTemplate>
    </mono:Pager>
</asp:Panel>
