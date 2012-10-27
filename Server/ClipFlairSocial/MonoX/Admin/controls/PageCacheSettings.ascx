<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageCacheSettings.ascx.cs" Inherits="MonoSoftware.MonoX.Admin.PageCacheSettings" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<p>
    <asp:Label ID="lblCacheDuration" AssociatedControlID="txtCacheDuration" runat="server" Text='<%$ Code: AdminResources.PageCacheSettings_CacheDuration %>'></asp:Label>
    <asp:TextBox id="txtCacheDuration" Runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="lblCacheLocation" AssociatedControlID="ddlCacheLocation" runat="server" Text='<%$ Code: AdminResources.PageCacheSettings_CacheLocation %>'></asp:Label>
    <asp:DropDownList ID="ddlCacheLocation" runat="server"></asp:DropDownList>
</p>
<p>
    <asp:Label ID="lblSlidingExpiration" AssociatedControlID="ddlSlidingExpiration" runat="server" Text='<%$ Code: AdminResources.PageCacheSettings_SlidingExpiration %>'></asp:Label>
    <asp:DropDownList ID="ddlSlidingExpiration" runat="server"></asp:DropDownList>
</p>
<p>
    <asp:Label ID="lblCacheDependencies" AssociatedControlID="txtCacheDependencies" runat="server" Text='<%$ Code: AdminResources.PageCacheSettings_CacheDependencies %>'></asp:Label>
    <asp:TextBox id="txtCacheDependencies" Runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="lblVaryByContentEncoding" AssociatedControlID="txtVaryByContentEncoding" runat="server" Text='<%$ Code: AdminResources.PageCacheSettings_VaryByContentEncoding %>'></asp:Label>
    <asp:TextBox id="txtVaryByContentEncoding" Runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="lblVaryByControl" AssociatedControlID="txtVaryByControl" runat="server" Text='<%$ Code: AdminResources.PageCacheSettings_VaryByControl %>'></asp:Label>
    <asp:TextBox id="txtVaryByControl" Runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="lblVaryByCustom" AssociatedControlID="txtVaryByCustom" runat="server" Text='<%$ Code: AdminResources.PageCacheSettings_VaryByCustom %>'></asp:Label>
    <asp:TextBox id="txtVaryByCustom" Runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="lblVaryByHeader" AssociatedControlID="txtVaryByHeader" runat="server" Text='<%$ Code: AdminResources.PageCacheSettings_VaryByHeader %>'></asp:Label>
    <asp:TextBox id="txtVaryByHeader" Runat="server"></asp:TextBox>
</p>
<p>
    <asp:Label ID="lblVaryByParam" AssociatedControlID="txtVaryByParam" runat="server" Text='<%$ Code: AdminResources.PageCacheSettings_VaryByParam %>'></asp:Label>
    <asp:TextBox id="txtVaryByParam" Runat="server"></asp:TextBox>
</p>
