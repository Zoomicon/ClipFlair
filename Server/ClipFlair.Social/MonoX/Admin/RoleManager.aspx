<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Title="User management" Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master"
    AutoEventWireup="True" CodeBehind="RoleManager.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.RoleManager"
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
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20%">
                        <%-- Note: Hidden but left for possible future use --%>
                        <asp:Literal ID="labAppName" runat="server" Text='<%$ Code: AdminResources.Label_Application %>'></asp:Literal>&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlApps" runat="server" AutoPostBack="true" CssClass="searchselect"
                            OnSelectedIndexChanged="ddlApps_SelectedIndexChanged" Width="306px">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </CustomFilterTemplate>
        <Columns>
            <mono:LiteGridBoundField DataField="Id" Visible="false" />
            <mono:LiteGridBoundField DataField="RoleName" HeaderText='<%$ Code: AdminResources.RoleManager_sortNameButton %>' SortExpression="RoleName" />
            <mono:LiteGridBoundField DataField="LoweredRoleName" HeaderText='<%$ Code: AdminResources.RoleManager_sortLoweredNameButton %>' SortExpression="LoweredRoleName" />
            <mono:LiteGridBoundField DataField="Description" HeaderText='<%$ Code: AdminResources.RoleManager_sortDescriptionButton %>' SortExpression="Description" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Literal ID="labEmptyMessage" runat="server" Text='<%$ Code: AdminResources.RoleManager_labNoData %>'></asp:Literal>
        </EmptyDataTemplate>
        <CustomActionsTemplate>
        </CustomActionsTemplate>
        <ContentTemplate>
        <div class="AdminGridFooterContent input-form">
            <table width="100%" cellpadding="0" cellspacing="6">
            <tr>
                <td colspan="3">
                    <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="Modification" ShowSummary="true" />
                </td>
            </tr>
            <asp:PlaceHolder id="plhAppEdit" runat="server" Visible="false">
            <tr>
              <td style="width:20%;">
                <asp:Literal ID="labAppNameEdit" runat="server" Text='<%$ Code: AdminResources.Label_Application %>'></asp:Literal>
              </td>
              <td style="width:80%;">
                <asp:DropDownList ID="ddlAppEdit" runat="server" CssClass="select" Width="100%"></asp:DropDownList>
              </td>
              <td></td>
            </tr>
            </asp:PlaceHolder>
            <tr>
              <td style="width:20%;">
                <asp:Literal ID="labName" runat="server" Text='<%$ Code: AdminResources.RoleManager_labName %>'></asp:Literal>
              </td>
              <td style="width:80%;">
                <asp:TextBox ID="txtName" Runat="server" CssClass="input"  Width="100%"></asp:TextBox>
              </td>
              <td>
                <asp:RequiredFieldValidator ID="requiredName" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true" ControlToValidate="txtName" ValidationGroup="Modification" Text="!" ErrorMessage='<%$ Code: AdminResources.RoleManager_requiredName %>'></asp:RequiredFieldValidator>
                <mono:RegExValidator ID="validateName" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true" ControlToValidate="txtName" ValidationGroup="Modification" ValidationType="alphaNumericWithSymbolsAllowedSpecialChr" Text="!" ErrorMessage='<%$ Code: AdminResources.RoleManager_validateName %>'></mono:RegExValidator>
              </td>
            </tr>
            <tr>
              <td valign="top">
                <asp:Literal ID="labDescription" runat="server" Text='<%$ Code: AdminResources.RoleManager_labDescription %>'></asp:Literal>
              </td>
              <td>
                <asp:TextBox ID="txtDescription" Runat="server" CssClass="input" TextMode="multiline" Width="100%" Height="100px"></asp:TextBox>
              </td>
              <td>
                <mono:RegExValidator ID="validateDescription" runat="server"  CssClass="ValidatorAdapter" SetFocusOnError="true" ControlToValidate="txtDescription" ValidationGroup="Modification" ValidationType="alphaNumericWithSymbolsAllowedSpecialChr" Text="!" ErrorMessage='<%$ Code: AdminResources.RoleManager_validateDescription %>'></mono:RegExValidator>
              </td>
            </tr>
          </table>
        </div>
        </ContentTemplate>
    </monox:GridViewEditBox>
</div>    
</asp:Content>
