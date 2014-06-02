<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPartSource.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.EditPartSource" ValidateRequest="false"  %>
<!DOCTYPE HTML>
<html>
<head runat="server">
    <title>MonoX</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="edit-part-source">
            <style type="text/css">
              .CodeMirror {
                border: 1px solid #eee;
              }
              .CodeMirror-scroll {
                height: auto;
                overflow-y: hidden;
                overflow-x: auto;
              }
            </style>
            <div class="top-section">
                <textarea runat="server" ID="txtCode" height="200"></textarea>
            </div>
            <div class="bottom-section">
                <div class="control-holder">
                    <asp:Button ID="btnOk" runat="server" Text="OK" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                    <asp:Label ID="lblWarning" runat="server" CssClass="ErrorMessage"></asp:Label>
                    <asp:Label ID="lblInjectScript" Runat="server"></asp:Label> 
                </div>
            </div>
        </div>
    </form>
</body>
</html>
