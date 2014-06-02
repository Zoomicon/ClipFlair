<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" CodeBehind="PageAdmin.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.PageAdmin" Theme="DefaultAdmin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoXControls" Namespace="MonoSoftware.MonoX.Controls" Assembly="MonoX" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">
    <telerik:RadCodeBlock ID="cb1" runat="server">
    <script type="text/javascript">
        var selectedTemplateFile = "";

        $(document).ready(function() {
            $("div.searchModule").hide();
            $(".searchPagesAction").click(function() {
            $(this).parents("div.pageManagerRoot").find("div.searchModule").toggle();
            });
        }
        );        
        
        function GridContextMenu(sender, args)
        {
            sender.hide();
        }

        function RowContextMenu(sender, eventArgs)
        {
            var menu = $find("<%=gridContextMenu.ClientID %>");
            var evt = eventArgs.get_domEvent();

            if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                return;
            }

            var index = eventArgs.get_itemIndexHierarchical();
            sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);
            menu.show(evt);

            evt.cancelBubble = true;
            evt.returnValue = false;

            if (evt.stopPropagation) {
                evt.stopPropagation();
                evt.preventDefault();
            }
        }

        function RowDblClick(sender, eventArgs)
        {
            window.open("PageManagerShowPage.aspx?pageId=" + eventArgs.getDataKeyValue("Id"));
        }


        function ContextMenuClick(sender, eventArgs)
        {
            var item = eventArgs.get_item();
            if (item.get_text() == ResourceManager.GetString("Rename"))
            {
                item.startEdit();
            }
        }
        
        function ConfirmDeleteNavigationCallBack(arg)
        {
            if (arg)
            {
                //need to do it this way due to the Firefox bug - 0x80004005 (NS_ERROR_FAILURE)...
                //window.top.setTimeout(function() 
                //{ 
                var aManager = $find('<%= ajaxManager.ClientID %>');
                var tView = $find('<%= tvPages.ClientID %>');
                if (tView != null && tView.get_selectedNode() != null)
                    aManager.ajaxRequest("DeleteNavigationItem|" + tView.get_selectedNode().get_value());
                //}, 0);                     
	            return true;                    
            }
        }
        
        function RenamePageCallBack(arg)
        {
            if (arg)
            {
                var selectedRow = GetSelectedGridRow();
                if (selectedRow != null)
                {
                    var aManager = $find('<%= ajaxManager.ClientID %>');
                    aManager.ajaxRequest("RenameGridItem|" + arg + "|" + selectedRow.getDataKeyValue("Id"));
                }
            }
        }
        
        function ConfirmDeletePageCallBack(arg)
        {
            if (arg)
            {
                var selectedRow = GetSelectedGridRow();
                if (selectedRow != null)
                {
                    var aManager = $find('<%= ajaxManager.ClientID %>');
                    aManager.ajaxRequest("DeleteGridItem|" + selectedRow.getDataKeyValue("Id"));
	            }     
            }
        }
        
        function NewPageCallBack(arg)
        {
            if (arg)
            {
                var aManager = $find('<%= ajaxManager.ClientID %>');
                aManager.AjaxRequest("NewPage|" + arg + "|" + selectedTemplateFile);
            }               
        }
        
        function NewContainerPageCallBack(arg)
        {
            if (arg)
            {
                var aManager = $find('<%= ajaxManager.ClientID %>');
                aManager.ajaxRequest("NewContainerPage|" + arg);
                return true;
            }               
        }
        
        function NewExternalPageCallBack(url, title)
        {
            if (url)
            {
                var aManager = $find('<%= ajaxManager.ClientID %>');
                aManager.ajaxRequest("NewExternalPage|" + url + "|" + title);
            }
        }

        function RefreshGridCallBack()
        {
            var aManager = $find('<%= ajaxManager.ClientID %>');
            aManager.ajaxRequest("RefreshGrid|");
        }

        function RefreshGridOnInsertCallBack()
        {
            var aManager = $find('<%= ajaxManager.ClientID %>');
            aManager.ajaxRequest("RefreshGridOnInsert|");
        }

        function TreeMenuShowing(sender, eventArgs) 
        {
            var treeNode = eventArgs.get_node();
            treeNode.set_selected(true);

            if (treeNode.get_category() == "ExternalPage")
            {
                eventArgs.get_menu().get_items().getItem(4).show();
            }
            else
            {
                eventArgs.get_menu().get_items().getItem(4).hide();
            }
        }
        
        function TreeMenuClick(sender, eventArgs)
        {
            var menuItem = eventArgs.get_menuItem();
            var treeNode = eventArgs.get_node();
            menuItem.get_menu().hide();

            switch (menuItem.get_value()) {
                case "Rename":
                    treeNode.startEdit();
                    break;
                case "NewContainerPage":
                    radprompt(ResourceManager.GetString("NewPagePrompt"), NewContainerPageCallBack, 330, 100, "", ResourceManager.GetString("NewContainerPage"), " ");
                    break;
                case "Delete":
                    radconfirm(ResourceManager.GetString("DeleteConfirmation"), ConfirmDeleteNavigationCallBack, 330, 100, "", ResourceManager.GetString("Delete"));
                    break;
                case "NewExternalPage":
                    OpenRadWindow("PageManagerExternalPage.aspx", 700, 250, "NewExternalPage", menuItem.get_text());
                    break;
                case "SetUrl":
                    var treeview = $find('<%= tvPages.ClientID %>');
                    if (treeNode != null)
                    {
                        OpenRadWindow("PageManagerSetUrl.aspx?nodeId=" + treeNode.get_value(), 700, 230, "SetUrl", menuItem.get_text());
                    }
                    break;
                case "Roles":
                    var treeview = $find('<%= tvPages.ClientID %>');
                    if (treeNode != null)
                    {
                        OpenRadWindow("PageManagerNavigationRoles.aspx?nodeId=" + treeNode.get_value(), 700, 350, "Roles", menuItem.get_text());
                    }
                    break;
            }
        }

        function GetSelectedGridRow() {
            var grid = $find('<%= gridPages.ClientID %>');
            var masterTable = grid.get_masterTableView();
            var selectedRow = null;
            
            if (masterTable != null && masterTable.get_selectedItems().length > 0)
                selectedRow = masterTable.get_selectedItems()[0];
            return selectedRow;
        }
        
        function OpenRadWindow(url, width, height, windowName, windowTitle)
        {
            var oWindow = window.radopen(url, windowName);
            oWindow.set_modal(true);
            oWindow.setSize (width, height);
            oWindow.center();
            oWindow.OnClientPageLoad = function ()  
            {
                oWindow.SetTitle(windowTitle);  
            }
        }
        
        function GridMenuClick(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var selectedRow = GetSelectedGridRow();
            if (selectedRow == null)
                return;
            switch (menuItem.get_value()) {
                case "Rename":
                    radprompt(ResourceManager.GetString("RenamePrompt"), RenamePageCallBack, 330, 100, "", ResourceManager.GetString("Rename"), " ");
                    break;
                case "Delete":
                    radconfirm(ResourceManager.GetString("DeleteConfirmation"), ConfirmDeletePageCallBack, 330, 100, "", ResourceManager.GetString("Delete"));
                    break;
                case "PageProperties":
                    OpenRadWindow("FileManagerPropertiesDialog.aspx?pageId=" + selectedRow.getDataKeyValue("Id"), 700, 650, "PageProperties", eventArgs.get_item().get_text());
                    break;
                case "ManageWebParts":
                    OpenRadWindow("PageManagerPartAdmin.aspx?pageId=" + selectedRow.getDataKeyValue("Id"), 700, 650, "ManageWebParts", eventArgs.get_item().get_text());
                    break; 
                case "ManagePageTemplates":
                    OpenRadWindow("PageManagerPageTemplates.aspx", 700, 700, "ManagePageTemplates", eventArgs.get_item().get_text());
                    break; 
                case "NewPage":
                    OpenRadWindow("PageManagerPropertiesDialog.aspx", 700, 650, "NewPage", eventArgs.get_item().get_text());
                    break;
                case "RegisterPage":
                    OpenRadWindow("PageManagerRegisterPage.aspx", 700, 650, "RegisterPage", eventArgs.get_item().get_text());
                    break;
                case "TestPages":
                    OpenRadWindow("PageManagerPageCrawl.aspx", 700, 650, "PageCrawl", eventArgs.get_item().get_text());
                    break; 

            }
        }
        
        function OnClientClose(radWindow)
        {                    
            var arg = radWindow.Argument;
            if (arg != null)
            {
                if (arg.Id == "ExternalPage")
                {
                    NewExternalPageCallBack(arg.PageUrl, arg.PageTitle);
                }
                else if (arg.Id == "RefreshGrid")
                {
                    RefreshGridCallBack();
                }
                else if (arg.Id == "RefreshGridOnInsert")
                {
                    RefreshGridOnInsertCallBack();
                }
                radWindow.Argument = null;
            }
        }
                         
        function OnRequestStart(sender, arguments)
        {
            document.body.style.cursor = 'wait';
        }
        function OnResponseEnd(sender, arguments)
        {
            document.body.style.cursor = 'default';
        }

    </script>
    <style type="text/css">
        #<%= gridPanelClientID %>
        {
	        margin:0;
	        height:95%;
        }
    </style>
    </telerik:RadCodeBlock> 
    <MonoXControls:MonoXWindowManager ID="rwmSingleton" runat="server" OnClientClose="OnClientClose"></MonoXControls:MonoXWindowManager>
    <div style="width:100%;height:100%;margin-left:auto;margin-right:auto;overflow:visible;">
            <div style="height:100%;padding:0px;margin-top:0px;margin-bottom:0px;" class="pageManagerRoot">
                <telerik:RadSplitter ID="RadSplitterBrowser" runat="server" VisibleDuringInit="false" ResizeWithBrowserWindow="true" BorderColor="Gray" 
                    Height="100%"
                    Width="100%"
                    EnableClientDebug="False"
                    BorderSize="1"
                    BorderStyle="Solid"
                    Skin="Default"
                    >
                    <telerik:RadPane ID="RadPaneTreeView" runat="server" Height="100%" Width="30%">
                        <div class="leftPaneHeader"><div style="width:50%; float:left"><asp:Label ID="lblPages" runat="server" Text='<%$ Code: AdminResources.PageAdmin_lblPages %>'></asp:Label></div><div style="float:right;text-align:right;padding-right: 15px;"><asp:HyperLink NavigateUrl="javascript:void(0)" CssClass="searchPagesAction" Text='<%$ Code: AdminResources.PageAdmin_lnkSearch %>' runat="server" ID="lnkSearch"></asp:HyperLink></div></div>
                        <div class="CssForm searchModule" style="padding-left:5px;padding-top:5px;">
                          <p>
                          <asp:Label ID="labFind" runat="server" AssociatedControlID="txtFilter" Text='<%$ Code: AdminResources.PageAdmin_labFind %>'></asp:Label>
                          <asp:TextBox id="txtFilter" Runat="server" EnableViewState="True"></asp:TextBox>
                          </p>
                          <br />
                          <p>
                          <asp:Label ID="lblCheckAdmin" runat="server" AssociatedControlID="chkAdmin">&nbsp;</asp:Label>
                          <div style="width:80%;"><asp:CheckBox ID="chkAdmin" runat="server" CausesValidation="false" Text='<%$ Code: AdminResources.PageAdmin_chkAdmin %>' OnCheckedChanged="chkAdmin_CheckedChanged" Font-Size="Smaller" AutoPostBack="true" /></div>
                          </p>
                          <div style="text-align:right;padding-right: 15px;">
                          <asp:Button id="btnFilter" runat="server" CausesValidation="false" Text='<%$ Code: AdminResources.Button_Filter %>' CssClass="AdminButton" OnClick="btnFilter_Click" />
                          &nbsp;
                          <asp:Button id="btnFilterReset" runat="server"  CausesValidation="false" Text='<%$ Code: AdminResources.Button_FilterReset %>' CssClass="AdminButton" OnClick="btnFilterReset_Click" />
                          </div>
                        </div>

                        <telerik:RadTreeView ID="tvPages" runat="server" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="true" OnClientContextMenuShowing="TreeMenuShowing" OnClientContextMenuItemClicking="TreeMenuClick" OnContextMenuItemClick="mnuMain_ItemClick"
                            AutoPostBack="True"
                            OnNodeClick="tvPages_NodeClick"
                            Skin="Default"
                            Width="95%"
                            Height="95%"
                            BeforeClientContextClick="ContextMenuClick"
                            AllowNodeEditing="True"
                            OnNodeEdit="tvPages_NodeEdit"
                            AccessKey="T"
                            DragAndDrop="True"
                            OnNodeDrop="tvPages_NodeDrop"
                            OnNodeExpand="tvPages_NodeExpand"
                            ShowLineImages="true"
                            >
                            <ContextMenus>
                                <telerik:RadTreeViewContextMenu ID="treeContextMenu" ExpandAnimation-Type="None" CollapseAnimation-Type="None" runat="server">
                                    <Items>
                                        <telerik:RadMenuItem Text="New container page" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.newfolder_gif %>" />
                                        <telerik:RadMenuItem Text="New external page" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.new_gif %>" />
                                        <telerik:RadMenuItem Text="Rename" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.rename_gif %>" />
                                        <telerik:RadMenuItem Text="Delete" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.deletefolder_gif %>" />
                                        <telerik:RadMenuItem Text="Set URL" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.html_gif %>" />
                                        <telerik:RadMenuItem Text="Roles" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.zip_gif %>" />                                
                                    </Items>
                                </telerik:RadTreeViewContextMenu>
                            </ContextMenus>
                        </telerik:RadTreeView>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="RadSplitBar1" runat="server" Width="1px" />
                    <telerik:RadPane ID="RadPaneGrid" Height="100%" runat="server" Width="70%" Scrolling="X" >
                        <telerik:RadGrid ID="gridPages" runat="server" AllowSorting="true" OnPreRender="gridPages_PreRender" PagerStyle-Font-Bold="true" PagerStyle-Font-Size="12px" PagerStyle-Mode="NextPrevAndNumeric"
                            AllowAutomaticDeletes="False" AllowAutomaticInserts="False" AllowAutomaticUpdates="False"
                            AllowPaging="true" AllowCustomPaging="true"
                            AutoGenerateColumns="False" PageSize="20"
                            GridLines="None"
                            Width="100%"
                            Height="100%" 
                            OnItemDataBound="gridPages_ItemDataBound"
                            OnNeedDataSource="gridPages_NeedDataSource"
                            OnSortCommand="gridPages_SortCommand"
                            Skin="Default" BorderWidth="0"
                            >
                            <MasterTableView ClientDataKeyNames="Id" Width="100%"  DataKeyNames="Id" TableLayout="Fixed" >
                                <Columns>
                                    <telerik:GridTemplateColumn
                                        SortExpression="HasNavigation" UniqueName="HasNavigation" HeaderText='<%$ Code: AdminResources.PageAdmin_colNavigation %>'>
                                        <ItemTemplate>
                                        <asp:CheckBox ID="chkNavigation" runat="server" OnCheckedChanged="chkNavigation_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("HasNavigation") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Wrap="false"
                                        SortExpression="LocalizedTitle" UniqueName="LocalizedTitle" HeaderText='<%$ Code: AdminResources.PageAdmin_colName %>'>
                                        <ItemTemplate>
                                            <asp:Label ID="itemLocalizedTitle" runat="server" Text='<%# Eval("LocalizedTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle BorderStyle="None" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Url" ItemStyle-Wrap="false"
                                        SortExpression="Url" UniqueName="Url" HeaderText='<%$ Code: AdminResources.PageAdmin_colUrl %>'>
                                        <ItemStyle BorderStyle="None"/>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TemplateName" ItemStyle-Wrap="false"
                                        SortExpression="TemplateName" UniqueName="TemplateName" HeaderText='<%$ Code: AdminResources.PageAdmin_colTemplateName %>'>
                                        <ItemStyle BorderStyle="None"/>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Wrap="false"
                                        SortExpression="AspnetUsers.UserName" UniqueName="AspnetUsers.UserName" HeaderText='<%$ Code: AdminResources.PageAdmin_colUser %>'>
                                        <ItemTemplate>
                                            <asp:Label ID="itemUserName" runat="server" Text='<%# Eval("AspnetUsers.UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle BorderStyle="None" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="DateCreated" ItemStyle-Wrap="false"
                                        SortExpression="DateCreated" UniqueName="DateCreated" HeaderText='<%$ Code: AdminResources.PageAdmin_colDateCreated %>'>
                                        <ItemStyle BorderStyle="None" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Id" HeaderText="Id" SortExpression="Id" UniqueName="Id" Visible="False">
                                    </telerik:GridBoundColumn>                      

                                </Columns>
                                <ExpandCollapseColumn Visible="False">
                                    <HeaderStyle Width="19px" />
                                </ExpandCollapseColumn>
                                <RowIndicatorColumn Visible="False">
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                            </MasterTableView>
                            <ItemStyle BorderStyle="None" />
                            <ClientSettings  ReorderColumnsOnClient="True" >
                                <Selecting AllowRowSelect="True" />
                                <Resizing AllowColumnResize="True" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="false" />
                                <ClientEvents 
                                OnRowContextMenu="RowContextMenu"
                                OnRowDblClick="RowDblClick"
                                />
                            </ClientSettings>
                            <HeaderStyle BorderColor="DarkGray" BorderStyle="Solid" BorderWidth="1px" />
                        </telerik:RadGrid>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </div>
        </div>
    
        <telerik:RadContextMenu ID="gridContextMenu" ExpandAnimation-Type="None" CollapseAnimation-Type="None" runat="server"
            IsContext="true"
            Skin="Default"
            ContextMenuElementID="gridPages"
            OnItemClick="gridContextMenu_ItemClick"
            OnClientItemClicked="GridContextMenu"
            OnClientItemClicking="GridMenuClick"
            >
            <Items>
                <telerik:RadMenuItem Text="New page"  ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.new_gif %>" />
                <telerik:RadMenuItem Text="Rename" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.rename_gif %>" />
                <telerik:RadMenuItem Text="Delete" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.delete_gif %>" />
                <telerik:RadMenuItem Text="Page properties" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.settings_gif %>" />
                <telerik:RadMenuItem Text="Manage Web parts" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.ascx_gif %>" />
                <telerik:RadMenuItem Text="Manage page templates" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.file_gif %>" />
                <telerik:RadMenuItem Text="Register page" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.aspx_gif %>" />
                <telerik:RadMenuItem Text="Test pages" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.dll_gif %>" />
            </Items>
        </telerik:RadContextMenu>

        <telerik:radajaxmanager id="ajaxManager" runat="server" OnAjaxRequest="ajaxManager_AjaxRequest" ClientEvents-OnResponseEnd="OnResponseEnd" ClientEvents-OnRequestStart="OnRequestStart" OnAjaxSettingCreated="ajaxManager_AjaxSettingCreated">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnFilter">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tvPages" />
                        <telerik:AjaxUpdatedControl ControlID="gridPages" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnFilterReset">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tvPages" />
                        <telerik:AjaxUpdatedControl ControlID="gridPages" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="tvPages">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tvPages" />
                        <telerik:AjaxUpdatedControl ControlID="treeContextMenu" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="gridPages">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tvPages" />
                        <telerik:AjaxUpdatedControl ControlID="gridPages" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="treeContextMenu">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tvPages" />
                        <telerik:AjaxUpdatedControl ControlID="gridPages" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="gridContextMenu">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="gridPages" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ajaxManager">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tvPages" />
                        <telerik:AjaxUpdatedControl ControlID="gridPages" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="chkAdmin">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tvPages" />
                        <telerik:AjaxUpdatedControl ControlID="gridPages" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            </AjaxSettings>
        </telerik:radajaxmanager>
</asp:Content>