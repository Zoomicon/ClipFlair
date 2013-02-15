<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Admin.UCLoader" Codebehind="UCLoader.aspx.cs" %>

<%@ Register Src="~/MonoX/Admin/controls/AdminHeader.ascx" TagPrefix="mono" TagName="AdminHeader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="AjaxScriptManager" EnablePageMethods="true" runat="server"></asp:ScriptManager>   
        <mono:AdminHeader id="adminHeader" runat="server" />

        <table width="100%">
        <tr>
        <td class="mainTable" align="center">
            <table cellspacing="0" cellpadding="0" class="tableMain">
            <tr>
            <td align="left">
                <asp:PlaceHolder ID="plhContainer" runat="server">
                </asp:PlaceHolder>
            </td>
            </tr>
            </table>
        </td>
        </tr>
        </table>


    </form>
</body>
</html>
