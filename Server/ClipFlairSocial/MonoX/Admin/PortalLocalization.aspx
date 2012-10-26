<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>

<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master" AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.Admin.PortalLocalization" EnableTheming="true" Theme="DefaultAdmin"
    Title="Portal localization management" CodeBehind="PortalLocalization.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register Src="~/MonoX/controls/AjaxConfirmDialog.ascx" TagPrefix="mono" TagName="AjaxConfirmDialog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="Server">
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <table id="tblContent" runat="server" border="0" cellpadding="2" cellspacing="2"
                width="100%">
                <tr>
                    <td rowspan="20" width="200px" valign="top">
                        <asp:ListBox ID="lstResourceKeys" runat="server" Width="100%" Rows="30" AutoPostBack="true"
                            SelectionMode="Single" OnSelectedIndexChanged="lstResourceKeys_SelectedIndexChanged">
                        </asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <asp:Button ID="btnExportAll" runat="server" Text="Export all from DB to Resx" OnClick="btnExportAll_Click"
                            CssClass="AdminLargeButton" Visible="false" />
                        <mono:AjaxConfirmDialog ID="ajaxConfirmDialogExportAll" runat="server"></mono:AjaxConfirmDialog>
                        <asp:Button ID="btnImportAll" runat="server" Text="Import all from Resx to DB" OnClick="btnImportAll_Click"
                            CssClass="AdminLargeButton" />
                        <mono:AjaxConfirmDialog ID="ajaxConfirmDialogImportAll" runat="server"></mono:AjaxConfirmDialog>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <asp:Button ID="btnExport" runat="server" Text="Export from DB to Resx" OnClick="btnExport_Click"
                            CssClass="AdminLargeButton"  Visible="false"/>
                        <asp:Button ID="btnImport" runat="server" Text="Import from Resx to DB" OnClick="btnImport_Click"
                            CssClass="AdminLargeButton" />
                        <span style="display: none;">
                            <asp:Button ID="btnBackup" runat="server" Text="Backup" ToolTip="Backup resource database"
                                CssClass="AdminButton" /></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh Page" CssClass="AdminButton" />
                        <asp:Button ID="btnReload" runat="server" Text="Reload resources" OnClick="btnReload_Click"
                            CssClass="AdminLargeButton" />
                        <asp:Button ID="btnDeleteAllLangResSets" runat="server" Text="Delete all language resource sets"
                            OnClick="btnDeleteAllLangResSets_Click" CssClass="AdminLargeButton" />
                        <mono:AjaxConfirmDialog ID="ajaxConfirmDialogDeleteAllLangResSets" runat="server"></mono:AjaxConfirmDialog>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5" valign="top">
                        <asp:DropDownList ID="ddlReourceSet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReourceSet_SelectedIndexChanged"
                            Width="90%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Button ID="btnDeleteResource" runat="server" Text="(D)elete resource set" OnClick="btnDeleteResource_Click"
                            AccessKey="D" CssClass="AdminLargeButton" />
                        <mono:AjaxConfirmDialog ID="ajaxConfirmDialog" runat="server"></mono:AjaxConfirmDialog>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <span>Resource key: </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <asp:TextBox ID="txtResourceKey" runat="server" Width="90%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <span>Value: </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <asp:TextBox ID="txtResourceValue" runat="server" Rows="5" TextMode="MultiLine" Width="90%"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnAddNew" runat="server" Text="(A)dd new" OnClick="btnAddNew_Click"
                            AccessKey="A" CssClass="AdminButton" />
                        <asp:Button ID="btnSave" runat="server" Text="(S)ave" AccessKey="S" OnClick="btnSave_Click"
                            CssClass="AdminButton" />
                        <asp:Button ID="btnDeleteResourceKey" runat="server" Text="De(l)ete" ToolTip="Delete resource key"
                            OnClick="btnDeleteResourceKey_Click" AccessKey="L" CssClass="AdminButton" />
                        <mono:AjaxConfirmDialog ID="ajaxConfirmDialogDeleteResourceKey" runat="server"></mono:AjaxConfirmDialog>
                        <asp:Button ID="btnRename" runat="server" Text="(R)ename" OnClick="btnRename_Click"
                            AccessKey="R" CssClass="AdminButton" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <asp:CheckBox ID="chkUserTranslation" runat="server" Checked="true" AutoPostBack="true"
                            Text="Use translations ?" OnCheckedChanged="chkUserTranslation_CheckedChanged" />
                        <br />
                        Possible translations:
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <asp:ListBox ID="lstTranslations" runat="server" SelectionMode="Single" Width="100%">
                        </asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <asp:Button ID="btnTranslate" runat="server" OnClick="btnTranslate_Click" Text="Use (t)ranslation"
                            ToolTip="Use selected translation" AccessKey="T" CssClass="AdminButton" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        <asp:GridView ID="grdTranslationPreview" runat="server">
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <div id="panMessage" runat="server" class="message-box">
                <img runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.warning_png %>" alt="Warning icon" />
                <div><strong><asp:Literal ID="labMessage" runat="server"></asp:Literal></strong></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress AssociatedUpdatePanelID="up" ID="updateProgressMain" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div class="ajaxOverlay"></div>
            <div class="ajaxLoader">Loading ... <br /><br /><img id="Img1" alt="Loading" runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.Common.img.ajaxLoader_gif %>" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
