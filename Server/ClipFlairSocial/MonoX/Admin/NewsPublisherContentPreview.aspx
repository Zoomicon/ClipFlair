<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Admin.NewsPublisherContentPreview" 
    EnableTheming="true"
    Theme="DefaultAdmin"
    Codebehind="NewsPublisherContentPreview.aspx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="labNewsContent" runat="server"></asp:Literal>
        <br />
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td align="center" valign="middle">
                    <asp:Button id="btnClose" runat="server" CausesValidation="false" Text='<%$ Code: AdminResources.Button_Close %>' CssClass="AdminButton" OnClick="btnClose_Click" AccessKey="C"  />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
