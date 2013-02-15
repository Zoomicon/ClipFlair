<%@ Page Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.FileManagerUploadDialog" EnableTheming="true" Theme="DefaultAdmin" Codebehind="FileManagerUploadDialog.aspx.cs" %>
<%@ Register Src="~/MonoX/ModuleGallery/UploadModule.ascx" TagPrefix="mono" TagName="UploadModule" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Upload</title>
    <script type="text/javascript">
        
        function CloseWindow()   
        {   
            GetRadWindow().Close();   
            //GetRadWindow().BrowserWindow.RefreshGridCallBack(null);   
        }   
        
        function RebindGrid()
        {
            GetRadWindow().BrowserWindow.RefreshGridCallBack(null);   
        }
           
        function GetRadWindow()   
        {   
            var oWindow = null;   
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog   
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;//IE (and Moz az well)   
                   
            return oWindow;   
        }   
  
   
    </script>
</head>
<body style="padding:0px;">
    <form id="form1" runat="server">
    <div>
        <mono:UploadModule ID="ctlUpload" runat="server" ShowUploadProgressBar="true" OverwriteExistingFiles="true"  />
        <br />
        <asp:Label ID="lblInjectScript" Runat="server"></asp:Label>  
        <div style="text-align:center; margin-left:auto; margin-right:auto; width:100%">
            <asp:button id="btnClose" OnClick="btnClose_Click" text="Close" CssClass="AdminButton" runat="server"></asp:button>
        </div>
    </div>
    </form>
</body>
</html>
