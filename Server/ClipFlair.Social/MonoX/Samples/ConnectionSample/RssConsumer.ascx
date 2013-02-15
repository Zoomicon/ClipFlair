<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Samples.RssConsumer" Codebehind="RssConsumer.ascx.cs" %>
<asp:Panel ID="pnlContainer" runat="server"></asp:Panel>
<asp:DataList ID="dlRss" runat="server">
<ItemTemplate>
    <p class="newsTitle"><asp:label runat="server" Text='<%# Eval("Title") %>'></asp:label></p>
    <p><asp:label runat="server" Text='<%# Eval("Description") %>'></asp:label></p>
	<asp:HyperLink runat="server" NavigateUrl='<%# Eval("Link") %>' CssClass="x" Text="read more"></asp:HyperLink>
</ItemTemplate>
</asp:DataList>
