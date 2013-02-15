
<%@ Import Namespace="MonoSoftware.MonoX.DAL.HelperClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.Admin.NewsIgnoredMetaKeywordsManager" EnableTheming="true"
    Theme="DefaultAdmin" Title="" CodeBehind="NewsIgnoredMetaKeywordsManager.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %>
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
                <mono:LiteGridBoundField DataField="Word" HeaderText='<%$ Code: AdminResources.NewsIgnoredMetaKeywordsManager_sortWordButton %>' SortExpression="Word" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="labNoData" runat="server" Text='<%$ Code: AdminResources.Label_NoData %>'></asp:Label>
            </EmptyDataTemplate>
            <CustomActionsTemplate>
            </CustomActionsTemplate>
            <ContentTemplate>
                <table width="100%" class="AdminGridFooterContent">
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="WordGroup"
                                ShowSummary="true" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="5%">
                            <asp:Label ID="labWord" runat="server" Text='<%$ Code: AdminResources.NewsIgnoredMetaKeywordsManager_labWord %>'></asp:Label>
                        </td>
                        <td width="95%">
                            <asp:TextBox ID="txtWord" runat="server" Width="97%" ValidationGroup="WordGroup"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredWord" runat="server" CssClass="ValidatorAdapter" ValidationGroup="WordGroup"
                                ControlToValidate="txtWord" SetFocusOnError="false" Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.NewsIgnoredMetaKeywordsManager_requiredWord %>'></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </monox:GridViewEditBox>
    </div>
</asp:Content>
