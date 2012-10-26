<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonoXSearchBox.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXSearchBox" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
<asp:panel ID="pnlContainer" runat="server" DefaultButton="btnSearch">
    <asp:TextBox runat="server" ID="txtSearch" style="float: left;"></asp:TextBox><asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" /><MonoX:StyledButton runat="server" ID="lnkSearch" OnClick="btnSearch_Click" CssClass="search-styled-button"></MonoX:StyledButton>
</asp:panel>