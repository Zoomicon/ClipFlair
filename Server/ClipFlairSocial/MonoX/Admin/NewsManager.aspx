<%@ Import Namespace="MonoSoftware.MonoX.DAL.HelperClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" AutoEventWireup="True"
    Inherits="MonoSoftware.MonoX.Admin.NewsManager" EnableTheming="true" Theme="DefaultAdmin"
    Title="" CodeBehind="NewsManager.aspx.cs" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Register Src="~/MonoX/Admin/controls/GridViewEditBox.ascx" TagPrefix="monox"
    TagName="GridViewEditBox" %>
<%@ Register Src="~/MonoX/controls/CustomRadEditor.ascx" TagPrefix="mono" TagName="CustomRadEditor" %>
<%@ MasterType VirtualPath="~/MonoX/MasterPages/AdminDefault.master" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="Server">

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
        <div style="height: 100%;">
            <telerik:RadSplitter ID="RadSplitterBrowser" runat="server" VisibleDuringInit="false" ResizeWithBrowserWindow="true" BorderColor="Gray"
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
                            <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                                <ContentTemplate>
                                    <rad:RadTreeView ID="radCategoriesTree" runat="server" Skin="Default" AutoPostBack="true"
                                        MultipleSelect="false" OnNodeClick="radCategoriesTree_NodeClick" />
                                    <asp:Button ID="btnGridTreeRefresh" runat="server" CausesValidation="false" Style="visibility: hidden;
                                        display: none;" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </telerik:RadPane>
                
                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" Width="1px" />            
                
                <telerik:RadPane ID="RadPaneGrid" Height="100%" runat="server" Width="80%">            
                    <monox:GridViewEditBox ID="gridViewBox" runat="server" ShowTopActions="true">
                        <CustomFilterTemplate>                                        
                        </CustomFilterTemplate>
                        <Columns>
                            <mono:LiteGridBoundField DataField="Id" Visible="false" />
                            <mono:LiteGridBoundField DataField="Title" HeaderText='<%$ Code: AdminResources.NewsManager_sortTitle %>' SortExpression="Title" />
                            <mono:LiteGridBoundField DataField="AspnetUser.UserName" HeaderText='<%$ Code: AdminResources.NewsManager_sortAuthor %>' SortExpression="UserName" />
                            <mono:LiteGridBoundField DataField="Published" HeaderText='<%$ Code: AdminResources.NewsManager_sortStatus %>' SortExpression="Published" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="labNoData" runat="server" Text='<%$ Code: AdminResources.NewsManager_labNoData %>'></asp:Label>
                        </EmptyDataTemplate>
                        <CustomActionsTemplate>
                            <asp:Button ID="btnPublish" runat="server" CssClass="AdminLargeButton" OnClick="btnPublish_Click" Text='<%$ Code: AdminResources.NewsManager_btnPublish %>'></asp:Button>
                        </CustomActionsTemplate>
                        <ContentTemplate>
                            <rad:RadTabStrip ID="tabStripNewsItems" runat="server" CausesValidation="false" Skin="Windows7"
                                MultiPageID="multiPageNewsItems" ReorderTabRows="false" Align="left">
                                <Tabs>
                                    <rad:RadTab >
                                    </rad:RadTab>
                                    <rad:RadTab >
                                    </rad:RadTab>
                                    <rad:RadTab >
                                    </rad:RadTab>
                                    <rad:RadTab >
                                    </rad:RadTab>
                                </Tabs>
                            </rad:RadTabStrip>
                            <rad:RadMultiPage ID="multiPageNewsItems" runat="server">
                                <rad:RadPageView ID="EditData" runat="server" CssClass="tab-float-left">
                                    <table border="0" cellpadding="2" cellspacing="10" width="989px" class="tab-content">
                                        <tr>
                                            <td colspan="2" class="top-section">
                                                <table cellpadding="0" cellspacing="0" width="100%" class="top-wrapper">
                                                    <tr>
                                                        <td style="width: 110px;">
                                                            <span id="imgCategory" runat="server" style="float: left;">
                                                                <img src="MonoSoftware.MonoX.GetImage.axd?<%= MonoSoftware.MonoX.UrlParams.ImageType.Name %>=<%= MonoSoftware.MonoX.Utilities.GetImageType.NewsCategory.ToString() %>&amp;<%= MonoSoftware.MonoX.UrlParams.EntityId.Name %>=<%= SelectedCategoryId %>" />
                                                            </span>
                                                        </td>
                                                        <td style="vertical-align: bottom !important;">
                                                            <div class="selected-category">
                                                                <b style="color: Red;"><asp:Label ID="labMessage" runat="server"></asp:Label></b><br />
                                                                <asp:Label ID="labSelectedCategory" runat="server" Text='<%$ Code: AdminResources.NewsManager_labSelectedCategory %>'></asp:Label><br />
                                                                <rad:RadComboBox ID="ddlCategories" runat="server" Width="100%" >
                                                                </rad:RadComboBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="10%">
                                                <asp:Label ID="labNewsTitle" runat="server" Text='<%$ Code: AdminResources.NewsManager_labNewsTitle %>'></asp:Label>
                                            </td>
                                            <td width="65%">
                                                <asp:TextBox ID="txtNewsTitle" runat="server" Width="99%"></asp:TextBox>&nbsp;
                                                <asp:RequiredFieldValidator ID="requiredNewsTitle" runat="server" CssClass="ValidatorAdapter" ValidationGroup="NewsItems"
                                                    ControlToValidate="txtNewsTitle" SetFocusOnError="true" Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.NewsManager_requiredNewsTitle %>'></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="labNewsShortContent" runat="server" Text='<%$ Code: AdminResources.NewsManager_labNewsShortContent %>'></asp:Label>
                                            </td>
                                            <td>                                                
                                                <mono:CustomRadEditor id="radNewsShortContent" EditorHeight="250px" AutoResizeHeight="False"
                                                    runat="server" Width="100%">
                                                </mono:CustomRadEditor>                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Label ID="labNewsContent" runat="server" Text='<%$ Code: AdminResources.NewsManager_labNewsContent %>'></asp:Label>
                                            </td>
                                            <td>                                                
                                                <mono:CustomRadEditor id="radNewsContent" EditorHeight="400px" AutoResizeHeight="False"
                                                    runat="server" Width="100%">
                                                </mono:CustomRadEditor>                                                
                                            </td>
                                        </tr>
                                    </table>
                                </rad:RadPageView>
                                <rad:RadPageView ID="PublicationTitle" runat="server" CssClass="tab-float-left">
                                    <table border="0" cellpadding="2" cellspacing="10" width="989px" class="tab-content">
                                        <tr id="rowPublished" runat="server">
                                            <td>
                                                <asp:Label ID="labNewsPublished" runat="server" Text='<%$ Code: AdminResources.NewsManager_labNewsPublished %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkNewsPublished" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="rowVisiblePublishDate" runat="server">
                                            <td>
                                                <asp:Label ID="labVisiblePublishDate" runat="server" Text='<%$ Code: AdminResources.NewsManager_labVisiblePublishDate %>' ToolTip='<%$ Code: AdminResources.NewsManager_labVisiblePublishDateToolTip %>'></asp:Label>
                                            </td>
                                            <td>
                                                <rad:RadDateTimePicker ID="radVisiblePublishDate" runat="server" Calendar-Skin="Default" TimeView-RenderDirection="Vertical">
                                                </rad:RadDateTimePicker>
                                            </td>
                                        </tr>
                                        <tr id="rowPublishStart" runat="server">
                                            <td>
                                                <asp:Label ID="labNewsPublishStart" runat="server" Text='<%$ Code: AdminResources.NewsManager_labNewsPublishStart %>'></asp:Label>
                                            </td>
                                            <td>
                                                <rad:RadDateTimePicker ID="radDatePublishStarts" runat="server" Calendar-Skin="Default" TimeView-RenderDirection="Vertical">
                                                </rad:RadDateTimePicker>
                                                <asp:RequiredFieldValidator ID="reqPublishStart" runat="server" CssClass="ValidatorAdapter" ValidationGroup="NewsItems"
                                                    ControlToValidate="radDatePublishStarts" SetFocusOnError="true" Display="Static" Text="*" ErrorMessage='<%$ Code: AdminResources.NewsManager_RequiredNewsPuslishStart %>'></asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                        <tr id="rowPublishEnd" runat="server">
                                            <td>
                                                <asp:Label ID="labNewsPublishEnds" runat="server" Text='<%$ Code: AdminResources.NewsManager_labNewsPublishEnds %>'></asp:Label>
                                            </td>
                                            <td>
                                                <rad:RadDateTimePicker ID="radDatePublishEnds" runat="server" Calendar-Skin="Default" TimeView-RenderDirection="Vertical">
                                                </rad:RadDateTimePicker>
                                            </td>
                                        </tr>
                                    </table>
                                </rad:RadPageView>
                                <rad:RadPageView ID="DisplayPropsTitle" runat="server" CssClass="tab-float-left">
                                    <table border="0" cellpadding="2" cellspacing="10" width="989px" class="tab-content">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkShowOnHomePage" runat="server" Text='<%$ Code: AdminResources.NewsManager_chkShowOnHomePage %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td width="25%">
                                                            <asp:Label ID="labShowTitle" runat="server" Text='<%$ Code: AdminResources.NewsManager_labShowTitle %>'></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:CheckBox ID="chkShowTitle" runat="server" />
                                                        </td>
                                                        <td width="25%">
                                                            <asp:Label ID="labShowShortContent" runat="server" Text='<%$ Code: AdminResources.NewsManager_labShowShortContent %>'></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:CheckBox ID="chkShowShortContent" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <asp:Label ID="labShowCategoryTitle" runat="server" Text='<%$ Code: AdminResources.NewsManager_labShowCategoryTitle %>'></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:CheckBox ID="chkShowCategoryTitle" runat="server" />
                                                        </td>
                                                        <td width="25%">
                                                            <asp:Label ID="labShowCategoryTitleAsLink" runat="server" Text='<%$ Code: AdminResources.NewsManager_labShowCategoryTitleAsLink %>'></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:CheckBox ID="chkShowCategoryTitleAsLink" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <asp:Label ID="labShowFullCategoryPath" runat="server" Text='<%$ Code: AdminResources.NewsManager_labShowFullCategoryPath %>'></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:CheckBox ID="chkShowFullCategoryPath" runat="server" />
                                                        </td>
                                                        <td width="25%">
                                                            <asp:Label ID="labShowUserName" runat="server" Text='<%$ Code: AdminResources.NewsManager_labShowUserName %>'></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:CheckBox ID="chkShowUserName" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="25%">
                                                            <asp:Label ID="labShowDateEntered" runat="server" Text='<%$ Code: AdminResources.NewsManager_labShowDateEntered %>'></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:CheckBox ID="chkShowDateEntered" runat="server" />
                                                        </td>
                                                        <td width="25%">
                                                            <asp:Label ID="labShowDateModified" runat="server" Text='<%$ Code: AdminResources.NewsManager_labShowDateModified %>'></asp:Label>
                                                        </td>
                                                        <td width="25%">
                                                            <asp:CheckBox ID="chkShowDateModified" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </rad:RadPageView>
                                <rad:RadPageView ID="PageView1" runat="server" CssClass="tab-float-left">
                                    <table border="0" cellpadding="2" cellspacing="10" width="989px" class="tab-content">
                                        <tr>
                                            <td>
                                                <asp:Label ID="labMetaDescription" runat="server" Text='<%$ Code: AdminResources.NewsManager_labMetaDescription %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMetaDescription" runat="server" TextMode="MultiLine" Rows="5"
                                                    Width="100%"></asp:TextBox>&nbsp;
                                                <br />
                                                <strong><%= MonoSoftware.MonoX.Resources.NewsAdmin.MetaDescriptionNote %></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="labMetaKeywords" runat="server" Text='<%$ Code: AdminResources.NewsManager_labMetaKeywords %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMetaKeywords" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>&nbsp;
                                                <br />
                                                <strong><%= MonoSoftware.MonoX.Resources.NewsAdmin.MetaKeywordsNote %></strong>
                                            </td>
                                        </tr>
                                    </table>
                                </rad:RadPageView>
                            </rad:RadMultiPage>
                        </ContentTemplate>
                    </monox:GridViewEditBox>
                </telerik:RadPane>
            </telerik:RadSplitter>
        </ContentTemplate>    
    </asp:UpdatePanel>    
    <rad:RadAjaxManager ID="ajaxManager" runat="server" >
    </rad:RadAjaxManager>
</asp:Content>
