<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupList" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>

<asp:Panel ID="pnlContainer" runat="server">
<h2><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h2>
<div class="option-menu">
    <asp:HyperLink runat="server" NavigateUrl="~/MonoX/Pages/SocialNetworking/Groups/GroupSearch/" Text="All Groups" /> <!-- TODO: need to localize this -->
	<asp:HyperLink runat="server" ID="lnkAddNew"></asp:HyperLink>
</div>
<div style="clear: both;">
    <asp:ListView ID="lvItems" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate></ItemTemplate>
    </asp:ListView>
</div>
<asp:Literal runat="server" ID="ltlNoData"></asp:Literal>
<mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
</mono:Pager>
</asp:Panel>