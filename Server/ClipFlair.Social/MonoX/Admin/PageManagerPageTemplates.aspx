
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Page Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.PageManagerPageTemplates" EnableTheming="true" Codebehind="PageManagerPageTemplates.aspx.cs" Theme="DefaultAdmin" %>
<%@ Register Src="~/MonoX/ModuleGallery/UploadModule.ascx" TagPrefix="mono" TagName="UploadModule" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoXControls" Namespace="MonoSoftware.MonoX.Controls" Assembly="MonoX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Page templates</title>


</head>
<body>
    <form id="form1" runat="server">
    <div class="popupBox">
        <asp:ScriptManager ID="AjaxScriptManager" EnablePageMethods="True" runat="server">
            <Scripts>
            </Scripts>
        </asp:ScriptManager>
        <MonoXControls:MonoXWindowManager ID="rwmSingleton" runat="server"></MonoXControls:MonoXWindowManager>
        <telerik:RadCodeBlock ID="radCodeBlock" runat="server">     
        <script type="text/javascript">
            var oldConfirm = radconfirm;
            window.radconfirm = function(text, mozEvent) {
                var ev = mozEvent ? mozEvent : window.event; //Moz support requires passing the event argument manually  
                //Cancel the event  
                ev.cancelBubble = true;
                ev.returnValue = false;
                if (ev.stopPropagation) ev.stopPropagation();
                if (ev.preventDefault) ev.preventDefault();

                //Determine who is the caller  
                var callerObj = ev.srcElement ? ev.srcElement : ev.target;

                //Call the original radconfirm and pass it all necessary parameters  
                if (callerObj) {
                    //Show the confirm, then when it is closing, if returned value was true, automatically call the caller's click method again.  
                    var callBackFn = function(arg) {
                        if (arg) {
                            callerObj["onclick"] = "";
                            if (callerObj.click) callerObj.click(); //Works fine every time in IE, but does not work for links in Moz  
                            else if (callerObj.tagName == "A") //We assume it is a link button!  
                            {
                                try {
                                    eval(callerObj.href)
                                }
                                catch (e) { }
                            }
                        }
                    }
                    oldConfirm(text, callBackFn, 300, 100, null, "MonoX");
                }
                return false;
            } 
        </script>
        </telerik:RadCodeBlock>
        <asp:Panel CssClass="CssForm" runat="server" ID="pnlContainer">
        <div class="header">
            <div class="headerContent">
                <h2 class="toolIcon"><asp:Literal runat="server" ID="ltlTitle" Text='<%$ Code: AdminResources.PageManagerPageTemplates_ltlTitle %>'></asp:Literal>
                </h2>
                <p>
                    <asp:Label ID="lblHeader" runat="server" Text='<%$ Code: AdminResources.PageManagerPageTemplates_lblHeader %>'></asp:Label>
                </p>
            </div>
        </div>
        <div class="content manage-template">
            <div class="choose-skin">
                <asp:Label ID="lblSkin" runat="server" Text='<%$ Code: AdminResources.PageManagerPageTemplates_lblSkin %>' AssociatedControlID="ddlSkins"></asp:Label>
                <asp:DropDownList ID="ddlSkins" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSkins_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList>    
            </div>
            <div style="overflow: hidden;">
                <asp:Repeater runat="server" ID="rptTemplates">
                    <ItemTemplate>
                        <div class="template-screen">
                            <div class="template-name"><asp:Literal ID="Literal1" runat="server" Text='<%# Eval("Name").ToString() %>'></asp:Literal></div>
                            <asp:Image runat="server" ImageUrl='<%# Eval("ThumbnailUrl").ToString() %>' ImageAlign="Middle" />&nbsp;&nbsp;<br />
                            <asp:Button runat="server" CssClass="AdminButton" Text='<%$ Code: AdminResources.Button_Delete %>' OnClientClick='return radconfirm(ResourceManager.GetString("DeleteConfirmation"), event);' OnCommand="DeleteTemplate" CommandArgument='<%# Eval("Path").ToString() %>' />
                        </div>                    
                    </ItemTemplate>
                </asp:Repeater>
            </div>            
            <div class="RadUploadContainer RadUploadContainerPopup">
                <asp:Label ID="lblUpload" runat="server" Text='<%$ Code: AdminResources.PageManagerPageTemplates_lblUpload %>'></asp:Label>
                <mono:UploadModule ID="ctlUpload" runat="server" ShowUploadProgressBar="true" OverwriteExistingFiles="true" />
            </div>
        </div>
        </asp:Panel>
        <div class="footer">
            <asp:button id="btnClose" OnClientClick="CloseWindow();" Text='<%$ Code: AdminResources.Button_Close %>' CssClass="AdminButton" runat="server"></asp:button>
        </div>
    </div>
    </form>
</body>
</html>
