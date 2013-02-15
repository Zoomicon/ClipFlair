<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.Admin.NewsPublisherManager" EnableTheming="true"
    Theme="DefaultAdmin" Title="" CodeBehind="NewsPublisherManager.aspx.cs" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %>
<%@ Register Src="~/MonoX/Admin/controls/DatePicker.ascx" TagPrefix="mono" TagName="DatePicker" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Register Src="~/MonoX/Admin/controls/GridViewEditBox.ascx" TagPrefix="monox"
    TagName="GridViewEditBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="Server">
    <asp:ScriptManagerProxy ID="gridScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/MonoX/Admin/controls/scripts/JSHelper.js" ScriptMode="Auto" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <div class="AdminContainer">
        <monox:GridViewEditBox ID="gridViewBox" runat="server" ShowTopActions="true">
            <CustomFilterTemplate>                
            </CustomFilterTemplate>
            <Columns>
                <mono:LiteGridBoundField DataField="Id" Visible="false" />
                <mono:LiteGridCheckBoxField DataField="NewsItem.Published" HeaderText='<%$ Code: AdminResources.NewsPublisherManager_sortPublishedButton %>' SortExpression="Published" />
                <mono:LiteGridBoundField DataField="NewsItem.PublishStart" HeaderText='<%$ Code: AdminResources.NewsPublisherManager_sortPublishStartButton %>' SortExpression="PublishStart" />
                <mono:LiteGridBoundField DataField="NewsItem.PublishEnd" HeaderText='<%$ Code: AdminResources.NewsPublisherManager_sortPublishEndButton %>' SortExpression="PublishEnd" />
                <mono:LiteGridBoundField DataField="NewsItem.Title" ReadOnly="true" HeaderText='<%$ Code: AdminResources.NewsPublisherManager_sortTitleButton %>' SortExpression="Title" />
                <asp:TemplateField HeaderText='<%$ Code: AdminResources.NewsPublisherManager_sortNewsContentButton %>'>
                    <ItemTemplate>
                        <div style="width: 85px; height: 22px; text-align: center; vertical-align: middle;" id="hoverArea" runat="server">
                            <asp:Button ID="btnToolTip" runat="server" CausesValidation="false" CssClass="AdminButton" UseSubmitBehavior="false" Text='<%$ Code: AdminResources.NewsPublisherManager_btnToolTip %>' />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <mono:LiteGridBoundField DataField="NewsItem.AspnetUser.Username" ReadOnly="true" HeaderText='<%$ Code: AdminResources.NewsPublisherManager_sortAspnetUserButton %>' SortExpression="Username" />
                <mono:LiteGridBoundField DataField="NewsItem.DateEntered" ReadOnly="true" HeaderText='<%$ Code: AdminResources.NewsPublisherManager_sortDateEnteredButton %>' SortExpression="DateEntered" />
                <mono:LiteGridBoundField DataField="NewsItem.DateModified" ReadOnly="true" HeaderText='<%$ Code: AdminResources.NewsPublisherManager_sortDateModifiedButton %>' SortExpression="DateModified" />
                <mono:LiteGridBoundField DataField="NewsItem.Revision" ReadOnly="true" HeaderText='<%$ Code: AdminResources.NewsPublisherManager_sortRevisionButton %>' SortExpression="Revision" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="labNoData" runat="server" Text='<%$ Code: AdminResources.Label_NoData %>'></asp:Label>
            </EmptyDataTemplate>
            <CustomActionsTemplate>
                <asp:Button ID="btnMultiPublish" runat="server" CausesValidation="false" Text='<%$ Code: AdminResources.NewsPublisherManager_btnMultiPublish %>' CssClass="AdminLargeButton" OnClick="btnMultiPublish_Click" />&nbsp;
            </CustomActionsTemplate>
            <ContentTemplate>
                <table width="100%" class="AdminGridFooterContent">
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="2" cellspacing="0" width="100%" class="News_mainNewsTable">
                                <tr id="rowPublished" runat="server">
                                    <td>
                                        <asp:Label ID="labNewsPublished" runat="server" Text='<%$ Code: AdminResources.NewsPublisherManager_labNewsPublished %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkNewsPublished" runat="server" />
                                    </td>
                                </tr>
                                <tr id="rowPublishStart" runat="server">
                                    <td>
                                        <asp:Label ID="labNewsPublishStart" runat="server" Text='<%$ Code: AdminResources.NewsPublisherManager_labNewsPublishStart %>'></asp:Label>
                                    </td>
                                    <td>
                                        <rad:RadDatePicker ID="radDatePublishStarts" runat="server" Calendar-Skin="Default2006">
                                        </rad:RadDatePicker>
                                    </td>
                                </tr>
                                <tr id="rowPublishEnd" runat="server">
                                    <td>
                                        <asp:Label ID="labNewsPublishEnds" runat="server" Text='<%$ Code: AdminResources.NewsPublisherManager_labNewsPublishEnds %>'></asp:Label>
                                    </td>
                                    <td>
                                        <rad:RadDatePicker ID="radDatePublishEnds" runat="server" Calendar-Skin="Default2006">
                                        </rad:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </monox:GridViewEditBox>
    </div>
    <rad:RadWindowManager ID="radWindowManager" runat="server" VisibleStatusbar="false">
        <Windows>
            <rad:RadWindow ID="windowDialog" runat="server" Height="300px" Width="600px" Left="350px"
                ReloadOnShow="true" Modal="true" />
        </Windows>
    </rad:RadWindowManager>
</asp:Content>
