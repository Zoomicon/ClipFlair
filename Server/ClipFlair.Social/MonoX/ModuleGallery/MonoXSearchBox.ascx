<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonoXSearchBox.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXSearchBox" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
<asp:panel ID="pnlContainer" runat="server" DefaultButton="btnSearch">
    <div class="search">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
        <MonoX:StyledButton runat="server" ID="lnkSearch" OnClick="btnSearch_Click"></MonoX:StyledButton>
        <div class="holder">
            <asp:TextBox runat="server" ID="txtSearch"></asp:TextBox>
        </div>
    </div>
</asp:panel>