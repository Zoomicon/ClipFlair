<%@ Page Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.PageManagerSetUrl" EnableTheming="true" Theme="DefaultAdmin" Codebehind="PageManagerSetUrl.aspx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Set URL</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    <br />
    <asp:ValidationSummary ID="valSum" runat="server" HeaderText='<%$ Code: AdminResources.PageManagerSetUrl_valSum %>' DisplayMode="BulletList" Font-Bold="true" EnableClientScript="true" /> 
    <asp:Panel style="padding:20px;" CssClass="CssForm" runat="server" ID="pnlContainer">
        <p>
            <asp:Label ID="lblPageUrl" AssociatedControlID="txtPageUrl" runat="server" Text='<%$ Code: AdminResources.PageManagerSetUrl_lblPageUrl %>'></asp:Label>
            <asp:TextBox id="txtPageUrl" Runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldValidator1" Runat="server" CssClass="ValidatorAdapter" Display="Dynamic" Text="!" Font-Bold="true" ControlToValidate="txtPageUrl" ErrorMessage='<%$ Code: AdminResources.PageManagerSetUrl_rfvPageUrl %>'></asp:RequiredFieldValidator>
        </p>
        <br />
        <div style="text-align:center; width:100%;">
            <asp:Label ID="lblInjectScript" Runat="server"></asp:Label>  
            <asp:Button ID="btnSave" runat="server" AccessKey="S" OnClick="btnSave_Click" CssClass="AdminButton" Text='<%$ Code: AdminResources.Button_Save %>' /> <asp:button id="btnClose" OnClientClick="CloseWindow()" Text='<%$ Code: AdminResources.Button_Close %>' CssClass="AdminButton" runat="server"></asp:button>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
