<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FileAdminAmazonS3.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.FileAdminAmazonS3" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" EnableTheming="true" Theme="DefaultAdmin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoXControls" Namespace="MonoSoftware.MonoX.Controls" Assembly="MonoX" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">
     <telerik:RadCodeBlock ID="cb1" runat="server">
        <script type="text/javascript">
        function OnFileOpen(sender, args) {
            var manager = sender.get_windowManager();
            window.setTimeout(
            function() {
                var activeWindow = manager.getActiveWindow();
                if (!activeWindow)
                    return;
                activeWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize);
                activeWindow.setSize(800, 800);
                activeWindow.center();
            }, 20);
        } 
        

        </script>
    </telerik:RadCodeBlock> 
    <MonoXControls:MonoXWindowManager ID="windowDialog" runat="server" Modal="true"></MonoXControls:MonoXWindowManager>
    <div class="fileExplorerContainer">
        <center>
            <telerik:RadFileExplorer OnClientFileOpen="OnFileOpen" runat="server" ID="ctlFileExplorer" Width="100%" Height="600px" CssClass="fileExplorer">
                <Configuration ViewPaths="~/MonoX/AmazonS3" UploadPaths="~/MonoX/AmazonS3" DeletePaths="~/MonoX/AmazonS3" />    
            </telerik:RadFileExplorer>
        </center>
    </div>
</asp:Content>