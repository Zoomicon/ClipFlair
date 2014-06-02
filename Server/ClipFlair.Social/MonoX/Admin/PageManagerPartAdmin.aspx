<%@ Page Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.PageManagerPartAdmin" EnableTheming="true" Codebehind="PageManagerPartAdmin.aspx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Web part administration</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    <div class="popupBox">
        <div class="header">
            <div class="headerContent">
                <h2 class="toolIcon"><asp:Literal runat="server" ID="ltlTitle" Text='<%$ Code: AdminResources.PageManagerPartAdmin_ltlTitle %>'></asp:Literal>
                </h2>
                <p>
                    <asp:Label ID="lblHeader" runat="server" Text='<%$ Code: AdminResources.PageManagerPartAdmin_lblHeader %>'></asp:Label>        
                </p>
            </div>
        </div>
        <div class="content"> 
            <asp:Label ID="lblScope" runat="server" Text='<%$ Code: AdminResources.PageManagerPartAdmin_lblScope %>'></asp:Label>&nbsp;<asp:DropDownList ID="ddlScope" CssClass="searchselect" runat="server" AutoPostBack="true"></asp:DropDownList><br /><br />
            <asp:Repeater ID="rptParts" runat="server">
                <HeaderTemplate>
                    <ol style="padding-left: 20px;">
                </HeaderTemplate>
                <ItemTemplate>
                    <li><%# Eval("DisplayTitle") %>  -  <%# Eval("Zone.ID") %><br />
                    <asp:LinkButton Enabled='<%# !Convert.ToBoolean(Eval("IsStatic")) %>' runat="server" OnCommand="btnDelete_Click" CommandArgument='<%# Eval("ID") %>' Text='<%# AdminResources.PageManagerPartAdmin_DeleteText %>'></asp:LinkButton> | <asp:LinkButton Enabled='<%# IsSharedMode() %>' runat="server" OnCommand="btnCopy_Click" CommandArgument='<%# Eval("ID") %>' Text='<%# AdminResources.PageManagerPartAdmin_CopyText %>'></asp:LinkButton></li>
                </ItemTemplate>
                <SeparatorTemplate>
                    <br /><br />
                </SeparatorTemplate>
                <FooterTemplate>
                    </ol>
                </FooterTemplate>
            </asp:Repeater>
            <br />
            <hr />
            <div class="admin-page">
                <ul>
                    <li><asp:LinkButton ID="btnReset" OnClick="btnReset_Click" runat="server" Text='<%$ Code: AdminResources.PageManagerPartAdmin_btnReset %>'></asp:LinkButton></li>
                    <li><asp:LinkButton ID="btnRemoveOrphanedWebParts" OnClick="btnRemoveOrphanedWebParts_Click" runat="server" Text='<%$ Code: AdminResources.PageManagerPartAdmin_btnRemoveOrphanedWebParts %>'></asp:LinkButton></li>
                    <li><asp:LinkButton ID="btnRemoveOrphanedDocs" OnClick="btnRemoveOrphanedDocs_Click" runat="server" Text='<%$ Code: AdminResources.PageManagerPartAdmin_btnRemoveOrphanedDocs %>'></asp:LinkButton></li>
                </ul>
            </div>
            <asp:Label ID="lblStatus" ForeColor="red" runat="server"></asp:Label>
        </div>
        <div class="footer">
            <asp:button id="btnClose" OnClientClick="CloseWindow()" Text='<%$ Code: AdminResources.Button_Close %>' CssClass="AdminButton" runat="server"></asp:button>
        </div>
    
    </div>
    </form>
</body>
</html>
