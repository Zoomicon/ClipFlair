<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXListModule" Codebehind="ListModule.ascx.cs" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>

<asp:Panel ID="pnlContainer" runat="server">
<div class="list-module">
    <asp:ListView ID="lvItems" runat="server">
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
    </LayoutTemplate>
    <ItemTemplate></ItemTemplate>
    </asp:ListView>
    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
    <PagerTemplate></PagerTemplate>
    </mono:Pager>
</div>
</asp:Panel>