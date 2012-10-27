<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupSearch.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupSearch" %>

<div class="people-search">
    <asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
    <asp:Panel runat="server" ID="pnlContainer">
    <div class="search-textbox-group">
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>   
    <asp:RequiredFieldValidator ID="vldRequiredSearch" runat="server" ControlToValidate="txtSearch" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />      
    </div>
    <div class="sn-group-search">
    <asp:Label ID="labGroupTip" runat="server" ></asp:Label>
    </div>
    <div style="float: right;">
    <MonoX:StyledButton runat="server" ID="btnSearch" OnClick="btnSearch_Click"/>
    </div>
    </asp:Panel>
</div>