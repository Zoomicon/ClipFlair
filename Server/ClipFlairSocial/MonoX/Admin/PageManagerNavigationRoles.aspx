<%@ Page Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.PageManagerNavigationRoles" EnableTheming="true" Codebehind="PageManagerNavigationRoles.aspx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoXControls" Namespace="MonoSoftware.MonoX.Controls" Assembly="MonoX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Navigation roles</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="popupBox">
        <asp:ScriptManager ID="AjaxScriptManager" EnablePageMethods="True" runat="server">
            <Scripts>
            </Scripts>
        </asp:ScriptManager>
        <asp:Label ID="lblInjectScript" Runat="server"></asp:Label>  
        <MonoXControls:MonoXWindowManager ID="rwmSingleton" runat="server"></MonoXControls:MonoXWindowManager>
        <asp:Panel CssClass="CssForm" runat="server" ID="pnlContainer">
            <div class="header">
                <div class="headerContent">
                    <h2 class="toolIcon"><asp:Literal runat="server" ID="ltlTitle" Text='<%$ Code: AdminResources.PageManagerNavigationRoles_ltlTitle %>'></asp:Literal>
                    </h2>
                    <p>
                    <asp:Label ID="lblDescription" Text='<%$ Code: AdminResources.PageManagerNavigationRoles_lblDescription %>' runat="server"></asp:Label><br /><br />
                    </p>
                </div>
            </div>
            <div class="content"> 
                <p>
                    <asp:Label ID="lblRoles" AssociatedControlID="chkRoles" runat="server" Text='<%$ Code: AdminResources.PageManagerNavigationRoles_lblRoles %>'></asp:Label>
                    <div style="width:100%"><asp:CheckBoxList Width="100%" BorderStyle="None" BorderWidth="0px" ID="chkRoles" runat="server" DataTextField="RoleName" DataValueField="RoleId" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" TextAlign="Right"></asp:CheckBoxList></div>
                </p>
             </div>
         </asp:Panel>    
         <div class="footer">
            <asp:Button ID="btnSave" runat="server" AccessKey="S" OnClick="btnSave_Click" CssClass="AdminButton" Text='<%$ Code: AdminResources.Button_Save %>' /> <asp:button id="btnClose" OnClientClick="CloseWindow();" Text='<%$ Code: AdminResources.Button_Close %>' CssClass="AdminButton" runat="server"></asp:button>
         </div>
    </div>
    </form>
</body>
</html>
