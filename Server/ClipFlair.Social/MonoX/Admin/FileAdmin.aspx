<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" CodeBehind="FileAdmin.aspx.cs" EnableTheming="true" Theme="DefaultAdmin" Inherits="MonoSoftware.MonoX.Admin.FileAdmin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoXControls" Namespace="MonoSoftware.MonoX.Controls" Assembly="MonoX" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">
     <telerik:RadCodeBlock ID="cb1" runat="server">
        <script language="javascript">
        var selectedTemplateFile = "";

        function GridMenuItemClicked(sender, args)
        {
            var oExplorer = $find("<%= ctlFileExplorer.ClientID %>");
            var itemValue = args.get_item().get_value();
            var itemText = args.get_item().get_text();
            if (itemValue != null) {
                if (itemValue.indexOf("newpage|") >= 0) {
                    selectedTemplateFile = itemValue.replace(/newpage\|/g, "");
                    radprompt(ResourceManager.GetString("NewPagePrompt"), NewPageCallback, 330, 100, "", ResourceManager.GetString("New"), " ");
                    return false;
                }
                if (itemValue == "PageProperties") 
                {
                    var itemPath = oExplorer.get_selectedItem().get_path();
                    //.aspx extension interferes with URL rewriting so it is replaced to _aspx_ and decoded at the server side
                    var oWindow = window.radopen("FileManagerPropertiesDialog.aspx?pageUrl=" + encodeURIComponent(itemPath).replace(/\.aspx/gi, "_aspx_"), "PageProperties");
                    oWindow.setSize(700, 650);
                    oWindow.set_modal(true);
                    oWindow.center();
                    oWindow.OnClientPageLoad = function() {
                        oWindow.SetTitle(itemText);
                    }
                    
                }
            }
        }
        
        function NewPageCallback(arg)
        {
            if (arg)
            {
                window.top.setTimeout(function() {
                    var ajaxManager = $find("<%= ajaxManager.ClientID %>");
                    ajaxManager.ajaxRequest("NewPage|" + arg + "|" + selectedTemplateFile);
                }, 0);
                return true;
                
            }
        }

        function OnResponseEnd(sender, arguments) {
            var oExplorer = $find("<%= ctlFileExplorer.ClientID %>");
            oExplorer.refresh();

        }

        function OnFileOpen(sender, args) {
            var manager = sender.get_windowManager();
            var item = args.get_item();
            window.setTimeout(
            function() {
                var activeWindow = manager.getActiveWindow();
                if (!activeWindow)
                    return;
                activeWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize);
                if (item != null && item.get_path().substr(0, 2) == '//')
                    activeWindow.setUrl(item.get_path().substr(1, item.get_path().length));
                activeWindow.setSize(700, 700);
                activeWindow.center();
            }, 20);
        } 
        

        </script>
    </telerik:RadCodeBlock> 
    <MonoXControls:MonoXWindowManager ID="windowDialog" runat="server" Modal="true"></MonoXControls:MonoXWindowManager>
    <div style="width:100%;height:100%;margin-left:auto;margin-right:auto;" class="fileExplorerContainer">
    <center>
    <telerik:RadFileExplorer OnClientFileOpen="OnFileOpen" runat="server" ID="ctlFileExplorer" Width="99%" Height="600px" CssClass="fileExplorer" DisplayUpFolderItem="true" ExplorerMode="Default" >
        <Configuration ViewPaths="~/" UploadPaths="~/" DeletePaths="~/" />
    </telerik:RadFileExplorer>
    </center>
    </div>
    <telerik:RadAjaxManager runat="server" ID="ajaxManager" OnAjaxRequest="ajaxManager_AjaxRequest" ClientEvents-OnResponseEnd="OnResponseEnd">
       <AjaxSettings>
       <telerik:AjaxSetting AjaxControlID="ajaxManager">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ctlFileExplorer.Grid" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>