<%@ Import Namespace="MonoSoftware.MonoX.DAL.HelperClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.Admin.NewsCategoryManager" EnableTheming="true"
    Theme="DefaultAdmin" Title="" CodeBehind="NewsCategoryManager.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %>
<%@ Register Src="~/MonoX/controls/DualListBox.ascx" TagPrefix="mono" TagName="DualListBoxUC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="Server">
    <asp:PlaceHolder ID="plhScript" runat="server">

        <script type="text/javascript">
        var clickedNode = null;
        function MenuClick(sender, args)
        {
            clickedNode = args.get_node();  
            if (args.get_menuItem().get_text() == '<%# MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.Delete %>')
            {
                radconfirm('<%# MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.DeleteConfirmation %>', ConfirmDeleteFolderCallBack, 330, 100, "", '<%# MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.Delete %>');
                args.set_cancel(true);
                return false;
            }            
         }  
          
        function ConfirmDeleteFolderCallBack(arg)
        {
            if (arg)
            {
                //need to do it this way due to the Firefox bug - 0x80004005 (NS_ERROR_FAILURE)...
                window.top.setTimeout(function() 
                { 
                    var aManager = <%# ajaxManager.ClientID %>;                    
                    var id = clickedNode.get_value();
                    aManager.ajaxRequest("DeleteCategory::" + id);
                    clickedNode = null;
                }, 0);                 
	            return true;                    
            }
            else
                clickedNode = null;
        }
            
            
        function ShowRadMenu(node, e)
        {    
            var menu = null;
            menu = window["<%# treeContextMenu.ClientID %>"];
            if (menu)
            {
                menu.Show(e);
                e.cancelBubble = true;
                if (e.stopPropagation)
                {
                    e.stopPropagation();
                }
                e.returnValue = false;
                if (e.preventDefault)
                {
                    e.preventDefault();
                }
            }
            
        }       
       
        </script>

    </asp:PlaceHolder>
    <telerik:RadCodeBlock ID="cb1" runat="server">
    <style type="text/css">
        HTML, FORM
        {
	        margin:0;
	        height:100%;
        }
        #<%= upPage.ClientID %>
        {
        	height:100%;
        }    
    </style>
    </telerik:RadCodeBlock> 
    <rad:RadWindowManager ID="rwmSingleton" runat="server" Skin="Default">
    </rad:RadWindowManager>
    <asp:UpdatePanel ID="upPage" runat="server" ChildrenAsTriggers="true">    
        <ContentTemplate>
        <div class="input-form" style="height:100%;">
            <telerik:RadSplitter ID="RadSplitterBrowser" runat="server" VisibleDuringInit="false" ResizeWithBrowserWindow="true" BorderColor="Gray" LiveResize="true"
                Height="100%"
                Width="100%"
                EnableClientDebug="False"
                BorderSize="1"
                BorderStyle="Solid"
                Skin="Default"                
                >
                <telerik:RadPane ID="RadPaneTreeView" runat="server" Height="100%" Width="20%" BackColor="#e7f5ff" CssClass="splitter-left">
                    <div class="splitter-content">
                        <div style="padding: 10px;">
                            <rad:RadTreeView ID="radCategoriesTree" runat="server" Skin="Default" AutoPostBack="true"
                                EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="true" BeforeClientContextMenu="ShowRadMenu"
                                OnNodeClick="radCategoriesTree_NodeClick" OnNodeDrop="radCategoriesTree_NodeDrop">
                                <ContextMenus>
                                    <rad:RadTreeViewContextMenu ID="treeContextMenu" runat="server" Skin="Default" Style="left: 2px"
                                        CausesValidation="false">
                                        <Items>
                                            <rad:RadMenuItem Text="MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.Cut" />
                                            <rad:RadMenuItem Text="MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.PasteBefore" />
                                            <rad:RadMenuItem Text="MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.Paste" />
                                            <rad:RadMenuItem Text="MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.PasteAfter" />
                                            <rad:RadMenuItem Text="MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.MoveUp" />
                                            <rad:RadMenuItem Text="MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.MoveDown" />
                                            <rad:RadMenuItem Text="MonoSoftware.MonoX.Resources.CategoryContextMenuOperations.Delete" />
                                        </Items>
                                    </rad:RadTreeViewContextMenu>
                                </ContextMenus>
                            </rad:RadTreeView>
                            <br />                        
                            <b id="labCategoryDragAndDrop" runat="server"><%# MonoSoftware.MonoX.Resources.NewsAdmin.CategoryDragAndDrop %></b>
                        </div>
                        <asp:Panel runat="server" CssClass="splitterDummy" ID="pnlDummyLeft" Visible="false"></asp:Panel>                                        
                    </div>
                </telerik:RadPane>
                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" Width="1px" />
                <telerik:RadPane ID="RadPaneGrid" Height="100%" runat="server" Width="80%">
                    <div class="splitter-content">
                        <div class="splitter-top">
                            <div style="padding: 10px; overflow: hidden;">
                                <span id="imgCategory" runat="server" style="float: left;"><asp:Image ID="imgCategoryPhoto" runat="server" /></span>
                                <div style="float: left; bottom: 10px; left: 120px; color: #888;">
                                    <asp:Label ID="labSelectedCategory" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labSelectedCategory %>'></asp:Label>
                                    <h2><asp:Label ID="labTitleView" runat="server"></asp:Label></h2>
                                </div>
                            </div>                            
                        </div>
                        <asp:PlaceHolder ID="panView" runat="server">
                            <div class="split-content">
                                <div style="padding: 0px 20px;">
                                    <dl>
                                        <dd>
                                            <asp:Label ID="labPublishedViewCaption" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labPublished %>'
                                                 AssociatedControlID="chkPublishedView"></asp:Label>
                                            <asp:CheckBox ID="chkPublishedView" runat="server" Enabled="false" CssClass="checkbox" />
                                        </dd>
                                        <dd>
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="labDescriptionViewCaption" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labDescription %>'
                                                         AssociatedControlID="labDescriptionView"></asp:Label>
                                                    </legend>                                                
                                                    <div style="padding: 10px;">
                                                    <asp:Label ID="labDescriptionView" runat="server" ></asp:Label>                                                    
                                                    </div>
                                            </fieldset>
                                        </dd>
                                        <dd>
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="Label1" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labCategoryViewRole %>'
                                                        AssociatedControlID="chkViewRolesView"></asp:Label>
                                                </legend>
                                                <asp:CheckBoxList ID="chkViewRolesView" runat="server" Enabled="false" CellSpacing="10"
                                                    RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100%">
                                                </asp:CheckBoxList>
                                            </fieldset>
                                        </dd>
                                        <dd>
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="Label2" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labCategoryEditRole %>'
                                                        AssociatedControlID="chkEditRolesView"></asp:Label>
                                                </legend>
                                                <asp:CheckBoxList ID="chkEditRolesView" runat="server" Enabled="false" CellSpacing="10"
                                                    RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100%">
                                                </asp:CheckBoxList>
                                            </fieldset>
                                        </dd>
                                    </dl>                                    
                                </div>
                            </div>
                            <div class="splitter-bottom">
                                <div style="padding: 0px 10px;">
                                    <asp:Button ID="btnAddNew" runat="server" CssClass="AdminButton" CausesValidation="false"
                                        Text='<%$ Code: AdminResources.Button_Add %>' OnClick="btnAddNew_Click" />
                                    <asp:Button ID="btnEdit" runat="server" CssClass="AdminButton" CausesValidation="false"
                                        ValidationGroup="NewsCategories" Text='<%$ Code: AdminResources.Button_Edit %>'
                                        OnClick="btnEdit_Click" />
                                </div>
                            </div>        
                            <asp:Panel runat="server" CssClass="splitterDummy" ID="pnlDummyRight" Visible="false"></asp:Panel>                                        
                        </asp:PlaceHolder>                
                        <asp:PlaceHolder ID="panEdit" runat="server">
                            <div class="split-content">
                                <div style="padding: 0px 20px;">
                                    <dl>
                                        <dd>
                                            <asp:Label ID="labTitle" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labTitle %>'
                                                ToolTip='<%$ Code: AdminResources.NewsCategoryManager_labTitle_ToolTip %>' AssociatedControlID="txtTitle"></asp:Label><br />
                                            <asp:TextBox ID="txtTitle" runat="server" ValidationGroup="NewsCategories"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredTitle" runat="server" CssClass="ValidatorAdapter"
                                                ValidationGroup="NewsCategories" ControlToValidate="txtTitle" SetFocusOnError="true"
                                                Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.NewsCategoryManager_requiredTitle %>'></asp:RequiredFieldValidator>
                                        </dd>
                                        <dd>
                                            <asp:Label ID="labName" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labName %>'
                                                ToolTip='<%$ Code: AdminResources.NewsCategoryManager_labName_ToolTip %>' AssociatedControlID="txtName"></asp:Label>                        
                                            <asp:TextBox ID="txtName" runat="server" ValidationGroup="NewsCategories"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredName" runat="server" CssClass="ValidatorAdapter"
                                                ValidationGroup="NewsCategories" ControlToValidate="txtName" SetFocusOnError="true"
                                                Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.NewsCategoryManager_requiredName %>'></asp:RequiredFieldValidator>
                                        </dd>
                                        <dd>
                                            <asp:Label ID="labImage" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labImage %>'
                                                AssociatedControlID="photoUpload"></asp:Label>
                                            <div><asp:FileUpload ID="photoUpload" runat="server"  /></di>
                                        </dd>    
                                        <dd id="rowPublish" runat="server">
                                            <asp:Label ID="labPublished" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labPublished %>'
                                                AssociatedControlID="chkPublished"></asp:Label>                        
                                            <asp:CheckBox ID="chkPublished" runat="server" CssClass="checkbox" />
                                        </dd>
                                        <dd>
                                            <asp:Label ID="labDescription" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labDescription %>'
                                                AssociatedControlID="txtDescription"></asp:Label>                        
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Columns="30"
                                                Rows="8" Width="99%"></asp:TextBox>                        
                                        </dd>
                                        <dd>
                                            <asp:Label ID="labTemplate" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labTemplate %>'
                                                AssociatedControlID="ddlTemplates"></asp:Label>                        
                                            <asp:DropDownList ID="ddlTemplates" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </dd>
                                        <dd>
                                            <asp:Label ID="labTemplateFullContent" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labTemplateFullContent %>'
                                                AssociatedControlID="ddlTemplatesFullContent"></asp:Label>                        
                                            <asp:DropDownList ID="ddlTemplatesFullContent" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </dd>
                                        <dd>
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="labCategoryViewRole" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labCategoryViewRole %>'
                                                        AssociatedControlID="chkCategoryViewPermissions"></asp:Label>
                                                </legend>
                                                <asp:CheckBoxList ID="chkCategoryViewPermissions" runat="server" CellSpacing="10"
                                                    RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100%">
                                                </asp:CheckBoxList>
                                            </fieldset>
                                        </dd>
                                        <dd>
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="labCategoryEditRole" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labCategoryEditRole %>'
                                                        AssociatedControlID="chkCategoryEditPermissions"></asp:Label>
                                                </legend>
                                                <asp:CheckBoxList ID="chkCategoryEditPermissions" runat="server" CellSpacing="10"
                                                    RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Table" Width="100%">
                                                </asp:CheckBoxList>
                                            </fieldset>
                                        </dd>
                                        <dd>
                                            <strong><asp:Label ID="labCategoryRoleNote" runat="server" Text='<%$ Code: AdminResources.NewsCategoryManager_labCategoryRoleNote %>'></asp:Label></strong>
                                        </dd>
                                    </dl>   
                                </div>
                            </div>                                             
                            <div class="splitter-bottom">
                                <div style="padding: 0px 10px;">
                                    <asp:Button ID="btnSave" runat="server" CssClass="AdminButton" CausesValidation="true"
                                        ValidationGroup="NewsCategories" Text='<%$ Code: AdminResources.Button_Save %>'
                                        OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="AdminButton" CausesValidation="false"
                                        ValidationGroup="NewsCategories" Text='<%$ Code: AdminResources.Button_Cancel %>'
                                        OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="validationSummary" runat="server" DisplayMode="List" ShowSummary="true"
                                    ValidationGroup="NewsCategories" />
                                    <b style="color: Red;"><asp:Literal ID="labMessage" runat="server"></asp:Literal></b>
                                </div>
                            </div>                                                                                
                        </asp:PlaceHolder>
                    </div>
                </telerik:RadPane>
            </telerik:RadSplitter>
            </div>
        </ContentTemplate>    
    </asp:UpdatePanel>    
    <rad:RadAjaxManager ID="ajaxManager" runat="server" >
    </rad:RadAjaxManager>
</asp:Content>
