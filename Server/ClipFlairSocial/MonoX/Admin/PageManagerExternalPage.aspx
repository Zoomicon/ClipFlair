<%@ Page Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.PageManagerExternalPage" EnableTheming="true" Theme="DefaultAdmin" Codebehind="PageManagerExternalPage.aspx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>External page</title>
    <script type="text/javascript">
        function CloseAndSave()   
        {   
            var arg = new Object();
            arg.Id = "ExternalPage";
            arg.PageUrl = document.getElementById("txtPageUrl").value;
            arg.PageTitle = document.getElementById("txtPageTitle").value;
            GetRadWindow().Argument = arg;
            GetRadWindow().Close();   
        }   
        
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <asp:ValidationSummary ID="valSum" runat="server" HeaderText='<%$ Code: AdminResources.PageManagerExternalPage_valSum %>' DisplayMode="BulletList" Font-Bold="true" EnableClientScript="true" /> 
    <asp:Panel style="padding:20px;" CssClass="CssForm" runat="server" ID="pnlContainer">
        <p>
            <asp:Label ID="lblPageTitle" AssociatedControlID="txtPageTitle" runat="server" Text='<%$ Code: AdminResources.PageManagerExternalPage_lblPageTitle %>'></asp:Label>
            <asp:TextBox id="txtPageTitle" Runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator id="rfvPageTitle" Runat="server" CssClass="ValidatorAdapter" Display="Dynamic" Text="!" Font-Bold="true" ControlToValidate="txtPageTitle" ErrorMessage='<%$ Code: AdminResources.PageManagerExternalPage_rfvPageTitle %>'></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblPageUrl" AssociatedControlID="txtPageUrl" runat="server" Text='<%$ Code: AdminResources.PageManagerExternalPage_lblPageUrl %>'></asp:Label>
            <asp:TextBox id="txtPageUrl" Runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator id="RequiredFieldValidator1" Runat="server" CssClass="ValidatorAdapter" Display="Dynamic" Text="!" Font-Bold="true" ControlToValidate="txtPageUrl" ErrorMessage='<%$ Code: AdminResources.PageManagerExternalPage_rfvPageUrl %>'></asp:RequiredFieldValidator>
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
