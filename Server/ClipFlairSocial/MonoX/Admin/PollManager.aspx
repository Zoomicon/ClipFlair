﻿<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Title="User management" Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master"
    AutoEventWireup="True" CodeBehind="PollManager.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.PollManager"
    Theme="DefaultAdmin" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Register Src="~/MonoX/Admin/controls/GridViewEditBox.ascx" TagPrefix="monox"
    TagName="GridViewEditBox" %>
<%@ Register Src="~/MonoX/Admin/controls/ColorPickerDropdown.ascx" TagPrefix="mono"
    TagName="ColorPicker" %>
<%@ Register Assembly="SD.LLBLGen.Pro.ORMSupportClasses.NET20" Namespace="SD.LLBLGen.Pro.ORMSupportClasses"
    TagPrefix="llblgenpro" %>
<%@ MasterType VirtualPath="~/MonoX/MasterPages/AdminDefault.master" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <asp:ScriptManagerProxy ID="gridScriptManager" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/MonoX/Admin/controls/scripts/JSHelper.js" ScriptMode="Auto" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <div class="AdminContainer">
        <monox:GridViewEditBox ID="gridViewBox" runat="server" ShowTopActions="true">
            <CustomFilterTemplate>
                <table>
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
                <mono:LiteGridBoundField DataField="Title" HeaderText='<%$ Code: AdminResources.PollManager_colTitle %>' SortExpression="Title" />
                <mono:LiteGridBoundField DataField="Question" HeaderText='<%$ Code: AdminResources.PollManager_colQuestion %>' SortExpression="Question" />
                <mono:LiteGridBoundField DataField="DateModified" HeaderText='<%$ Code: AdminResources.PollManager_colDateModified %>' SortExpression="DateModified" />
                <mono:LiteGridBoundField DataField="UserName" HeaderText='<%$ Code: AdminResources.PollManager_colCreatedByUserName %>' SortExpression="UserName" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="labNoData" runat="server" Text='<%$ Code: AdminResources.Label_NoData %>'></asp:Label>
            </EmptyDataTemplate>
            <CustomActionsTemplate>
            </CustomActionsTemplate>
            <ContentTemplate>
            <div class="AdminGridFooterContent">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            <asp:Panel CssClass="CssForm" runat="server" ID="pnlContainer">
                                <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="Modification"
                                    ShowSummary="true" />
                                <p style="display: none;">
                                    <asp:Label ID="lblApp" runat="server" Text='<%$ Code: AdminResources.Label_Application %>' AssociatedControlID="ddlApp"></asp:Label>
                                    <asp:DropDownList ID="ddlApp" runat="server">
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%$ Code: AdminResources.PollManager_lblTitle %>' AssociatedControlID="txtTitle"></asp:Label>
                                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredTitle" runat="server" CssClass="ValidatorAdapter" ValidationGroup="Modification"
                                        ControlToValidate="txtTitle" SetFocusOnError="true" Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.PollManager_requiredTitle %>'></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="lblQuestion" runat="server" Text='<%$ Code: AdminResources.PollManager_lblQuestion %>' AssociatedControlID="txtQuestion"></asp:Label>
                                    <asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredQuestion" runat="server" CssClass="ValidatorAdapter" ValidationGroup="Modification"
                                        ControlToValidate="txtQuestion" SetFocusOnError="true" Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.PollManager_requiredQuestion %>'></asp:RequiredFieldValidator>
                                </p>
                            </asp:Panel>
                            <br />
                            <div style="width: 100%; text-align: center">
                                <b style="color: Red;">
                                    <asp:Literal ID="labMessage" runat="server"></asp:Literal></b>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            </ContentTemplate>
        </monox:GridViewEditBox>
        <monox:GridViewEditBox ID="gridViewBoxDetails" runat="server" ShowTopActions="true" UpdateMode="Always">
            <CustomFilterTemplate>
            </CustomFilterTemplate>
            <Columns>
                <mono:LiteGridBoundField DataField="Id" Visible="false" />
                <mono:LiteGridBoundField DataField="Answer" HeaderText='<%$ Code: AdminResources.PollManager_colAnswer %>' SortExpression="Answer" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkMoveUp" runat="server" CommandArgument='<%# Eval("Id") %>'
                            OnCommand="lnkMoveUp_Click" Text='<%$ Code: AdminResources.PollManager_lnkMoveUp %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkMoveDown" runat="server" CommandArgument='<%# Eval("Id") %>'
                            OnCommand="lnkMoveDown_Click" Text='<%$ Code: AdminResources.PollManager_lnkMoveDown %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="labNoData" runat="server" Text='<%$ Code: AdminResources.Label_NoData %>'></asp:Label>
            </EmptyDataTemplate>
            <CustomActionsTemplate>
            </CustomActionsTemplate>
            <ContentTemplate>
            <div class="AdminGridFooterContent">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            <asp:Panel CssClass="CssForm" runat="server" ID="Panel1">
                                <p>
                                    <asp:Label ID="labAnswer" runat="server" Text='<%$ Code: AdminResources.PollManager_labAnswer %>' style="width: 20%; float: left;"></asp:Label>
                                    <asp:TextBox ID="txtAnswer" runat="server" Text='<%# Eval("Answer") %>'></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Label ID="Label1" runat="server" Text='<%$ Code: AdminResources.PollManager_lblColor %>' style="width: 20%; float: left;"></asp:Label>
                                    <mono:ColorPicker ID="ddlColor" runat="server" SelectedValue='<%# Eval("Color") %>'/>
                                </p>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
            </ContentTemplate>
        </monox:GridViewEditBox>
    </div>
</asp:Content>
