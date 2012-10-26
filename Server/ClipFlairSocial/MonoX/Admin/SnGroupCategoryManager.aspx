<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Title="Group category manager" Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master"
    AutoEventWireup="True" CodeBehind="SnGroupCategoryManager.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.SnGroupCategoryManager"
    Theme="DefaultAdmin" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %> 
<%@ Register Assembly="MonoSoftware.Web" Namespace="MonoSoftware.Web.Controls" TagPrefix="mono" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Register Src="~/MonoX/Admin/controls/GridViewEditBox.ascx" TagPrefix="monox" TagName="GridViewEditBox" %>
<%@ MasterType VirtualPath="~/MonoX/MasterPages/AdminDefault.master" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">

<asp:ScriptManagerProxy ID="gridScriptManager" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/MonoX/Admin/controls/scripts/JSHelper.js" ScriptMode="Auto" />
    </Scripts>
</asp:ScriptManagerProxy>

<div class="AdminContainer">
    <monox:GridViewEditBox ID="gridViewBox" runat="server" ShowTopActions="true" >
        <CustomFilterTemplate>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="display: none;">
                        <%-- Note: Hidden but left for possible future use --%>
                        <asp:Literal ID="labAppName" runat="server" Text='<%$ Code: AdminResources.Label_Application %>'></asp:Literal>&nbsp;
                        <asp:DropDownList ID="ddlApps" runat="server" AutoPostBack="true" CssClass="searchselect"
                            OnSelectedIndexChanged="ddlApps_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </CustomFilterTemplate>
        <Columns>
            <mono:LiteGridBoundField DataField="Id" Visible="false" />
            <mono:LiteGridBoundField DataField="Name" HeaderText='<%$ Code: AdminResources.SnGroupCategoryManager_sortNameButton %>' SortExpression="Name" />
            <mono:LiteGridBoundField DataField="Slug" HeaderText='<%$ Code: AdminResources.SnGroupCategoryManager_sortSlugButton %>' SortExpression="Slug" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Literal ID="labEmptyMessage" runat="server" Text='<%$ Code: AdminResources.SnGroupCategoryManager_labEmptyMessage %>'></asp:Literal>
        </EmptyDataTemplate>
        <CustomActionsTemplate>
        </CustomActionsTemplate>
        <ContentTemplate>
        <div class="AdminGridFooterContent">
            <table width="100%">
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="Modification" ShowSummary="true" />
                    <br />
                </td>
            </tr>
            <tr style="display:none;">
              <td style="width:20%;">
                <asp:Literal ID="labAppNameEdit" runat="server" Text='<%$ Code: AdminResources.Label_Application %>'></asp:Literal>
              </td>
              <td style="width:80%;">
                <asp:DropDownList ID="ddlAppEdit" runat="server" CssClass="select" Width="100%"></asp:DropDownList>
              </td>
              <td></td>
            </tr>
            <tr>
              <td style="width:20%;">
                <asp:Literal ID="labName" runat="server" Text='<%$ Code: AdminResources.SnGroupCategoryManager_labName %>'></asp:Literal>
              </td>
              <td style="width:80%;">
                <asp:TextBox ID="txtName" Runat="server" CssClass="input"  Width="100%"></asp:TextBox>
              </td>
              <td>
                <asp:RequiredFieldValidator ID="requiredName" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true" ControlToValidate="txtName" ValidationGroup="Modification" Text="!" ErrorMessage='<%$ Code: AdminResources.SnGroupCategoryManager_requiredName %>'></asp:RequiredFieldValidator>
                <mono:RegExValidator ID="validateName" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true" ControlToValidate="txtName" ValidationGroup="Modification" Text="!" ValidationType="AlphaNumericWithSymbolsAllowedSpecialChr" ErrorMessage='<%$ Code: AdminResources.SnGroupCategoryManager_validateName %>'></mono:RegExValidator>
              </td>
            </tr>
          </table>
        </div>
        </ContentTemplate>
    </monox:GridViewEditBox>
</div>    
</asp:Content>
