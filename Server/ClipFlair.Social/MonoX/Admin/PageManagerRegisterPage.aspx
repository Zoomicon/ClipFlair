<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageManagerRegisterPage.aspx.cs" EnableTheming="true" Inherits="MonoSoftware.MonoX.Admin.PageManagerRegisterPage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoXControls" Namespace="MonoSoftware.MonoX.Controls" Assembly="MonoX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Register page</title>
    <script type="text/javascript">
        function CloseAndSave() {
            var arg = new Object();
            arg.Id = "RefreshGridOnInsert";
            var oWindow = GetRadWindow();
            if (oWindow != null) {
                oWindow.Argument = arg;
                oWindow.Close();
            }
        }


        function ToggleTreeView() {
            if (document.getElementById("treeDiv").style.visibility == "visible")
                HideTreeView();
            else
                ShowTreeView();
        }

        function ShowTreeView() {
            document.getElementById("treeDiv").style.visibility = "visible";
        }

        function HideTreeView() {
            document.getElementById("treeDiv").style.visibility = "hidden";
        }

        function SetTextBox(id, s) {
            document.getElementById(id).value = s;
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="popupBox">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server"> 
    <script type="text/javascript">
        function ProcessClientClick(sender, eventArgs) {
            var node = eventArgs.get_node();
            SetTextBox('<%= txtPagePath.ClientID %>', node.get_text());
            document.getElementById('<%= tvSelectedValue.ClientID %>').value = node.get_value();
            HideTreeView();
        }
    </script>
    </telerik:RadCodeBlock>

    <asp:ScriptManager ID="AjaxScriptManager" EnablePageMethods="True" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>
    <MonoXControls:MonoXWindowManager ID="rwmSingleton" runat="server"></MonoXControls:MonoXWindowManager>
    <asp:HiddenField ID="tvSelectedValue" runat="server" />
    <asp:Panel CssClass="CssForm" runat="server" ID="pnlContainer">
        <div class="header">
            <div class="headerContent">
                <h2 class="toolIcon"><asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                </h2>
                <p>
                <asp:Label ID="lblHeaderDescription" runat="server"></asp:Label><br /><br />
                </p>
            </div>
        </div>
        <div class="content"> 
        <p>
            <asp:Label ID="lblPagePath" AssociatedControlID="txtPagePath" runat="server"></asp:Label>
            <asp:TextBox ID="txtPagePath" runat="server" style="float: left; z-index:1;" ReadOnly="true" onclick="ToggleTreeView()"></asp:TextBox><telerik:RadCodeBlock id="radCodeBlock2" runat="server"><img onclick="ToggleTreeView()" src='<%= GetImgPath("DropArrow.gif") %>' style="display: block; float: left; margin-top: 1px; position:relative; margin-left: -16px; z-index: 9999;" alt="" height="18" width="16" /></telerik:RadCodeBlock><br />
            <div id="treeDiv" style="position:absolute; left: 150px; visibility: hidden; border: solid 1px; background: white; height: 150px; width: 70%; clear: both; margin-top: 0px; overflow:auto;">
                <telerik:RadTreeView ID="tvFolders" runat="server" ValidationGroup="treeViewVg"  
                    AutoPostBack="True"
                    OnNodeClick="tvFolders_NodeClick"
                    Skin="Default"
                    Width="99%"
                    Height="96%"
                    AllowNodeEditing="False"
                    AccessKey="T"
                    DragAndDrop="False"
                    OnNodeExpand="tvFolders_NodeExpand"
                    ShowLineImages="true"
                    OnClientNodeClicking="ProcessClientClick"
                    >
                </telerik:RadTreeView>
            </div>
        </p>
        <p>
            <asp:Label ID="lblFileName" AssociatedControlID="cboFiles" runat="server"></asp:Label>
            <asp:DropDownList ID="cboFiles" runat="server" CssClass="InputSelect"></asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="lblPageTitle" AssociatedControlID="txtPageTitle" runat="server"></asp:Label>
            <asp:TextBox id="txtPageTitle" Runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator id="rfvPageTitle" Runat="server" CssClass="ValidatorAdapter" Display="Dynamic" Text="!" Font-Bold="true" ControlToValidate="txtPageTitle" ValidationGroup="mainVg"></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblDescription" AssociatedControlID="txtDescription" runat="server"></asp:Label>
            <asp:TextBox id="txtDescription" Runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="lblKeywords" AssociatedControlID="txtKeywords" runat="server"></asp:Label>
            <asp:TextBox id="txtKeywords" Runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="lblRoles" AssociatedControlID="chkRoles" runat="server"></asp:Label>
            <div style="width:70%;float:left;"><asp:CheckBoxList Width="100%" BorderStyle="None" BorderWidth="0px" ID="chkRoles" runat="server" DataTextField="RoleName" DataValueField="RoleId" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" TextAlign="Right"></asp:CheckBoxList></div>
        </p>
        <p>
            <asp:Label ID="lblEditRoles" AssociatedControlID="chkEditRoles" runat="server"></asp:Label>
            <div style="width:70%; float: left;"><asp:CheckBoxList Width="100%" BorderStyle="None" BorderWidth="0px" ID="chkEditRoles" runat="server" DataTextField="RoleName" DataValueField="RoleId" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" TextAlign="Right"></asp:CheckBoxList></div>
        </p>
        
        <p>
            <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="List" Font-Bold="true" EnableClientScript="true" ValidationGroup="mainVg" />
        </p>
        </div>
        </asp:Panel>

        
        <div class="footer">
            <asp:Label ID="lblInjectScript" runat="server"></asp:Label>  
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="AdminButton" ValidationGroup="mainVg" /> <asp:button id="btnClose" OnClientClick="CloseWindow();" CssClass="AdminButton" runat="server"></asp:button>
        </div>
    
    <telerik:radajaxmanager id="ajaxManager" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="tvFolders">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tvFolders" />
                    <telerik:AjaxUpdatedControl ControlID="cboFiles" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>    
    </div>
    </form>
</body>
</html>
