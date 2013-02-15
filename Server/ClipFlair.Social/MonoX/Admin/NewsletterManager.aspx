<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Title="User management" Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master"
    AutoEventWireup="True" CodeBehind="NewsletterManager.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.NewsletterManager"
    Theme="DefaultAdmin" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Register Src="~/MonoX/Admin/controls/GridViewEditBox.ascx" TagPrefix="monox"
    TagName="GridViewEditBox" %>
<%@ Register Src="~/MonoX/controls/CustomRadEditor.ascx" TagPrefix="mono" TagName="CustomRadEditor" %>
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
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 20%; padding-bottom: 2px;">
                            <%-- Note: Hidden but left for possible future use --%>
                            <asp:Literal ID="labAppName" runat="server" Text='<%$ Code: AdminResources.Label_Application %>'></asp:Literal>&nbsp;
                        </td>
                        <td style="width: 60%; padding-bottom: 2px;">
                            <asp:DropDownList ID="ddlApps" runat="server" AutoPostBack="true" CssClass="searchselect"
                                OnSelectedIndexChanged="ddlApps_SelectedIndexChanged" Width="306px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%; padding-bottom: 2px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labRoles" runat="server" Text='<%$ Code: AdminResources.NewsletterManager_labRoles %>'></asp:Literal>&nbsp;
                        </td>
                        <td style="width: 60%">
                            <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="true" CssClass="searchselect"
                                OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" Width="306px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 20%">&nbsp;</td>
                    </tr>
                </table>
            </CustomFilterTemplate>
            <Columns>
                <mono:LiteGridBoundField DataField="Id" Visible="false" />
                <mono:LiteGridBoundField DataField="Title" HeaderText='<%$ Code: AdminResources.NewsletterManager_colTitle %>' SortExpression="Title" />
                <mono:LiteGridBoundField DataField="UserName" HeaderText='<%$ Code: AdminResources.NewsletterManager_colCreatedByUserName %>' SortExpression="UserName" />
                <mono:LiteGridBoundField DataField="DateModified" HeaderText='<%$ Code: AdminResources.NewsletterManager_colDateModified %>' SortExpression="DateModified" />
                <mono:LiteGridBoundField DataField="Status" HeaderText='<%$ Code: AdminResources.NewsletterManager_colStatus %>' SortExpression="Status" />
                <mono:LiteGridBoundField DataField="SentOnString" HeaderText='<%$ Code: AdminResources.NewsletterManager_colSentOn %>' SortExpression="SentOn" />
                <mono:LiteGridBoundField DataField="NewsletterRolesString" HeaderText='<%$ Code: AdminResources.NewsletterManager_colNewsletterRoles %>' SortExpression="NewsletterRolesString" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="labNoData" runat="server" Text='<%$ Code: AdminResources.NewsletterManager_labNoData %>'></asp:Label>
            </EmptyDataTemplate>
            <CustomActionsTemplate>
                <asp:Button ID="btnReset" runat="server" CssClass="AdminLargeButton" OnClick="btnReset_Click" Text='<%$ Code: AdminResources.NewsletterManager_btnReset %>'></asp:Button>
                &nbsp;
                <asp:Button ID="btnTest" runat="server" CssClass="AdminLargeButton" OnClick="btnTest_Click" Text='<%$ Code: AdminResources.NewsletterManager_btnTest %>'></asp:Button>
                &nbsp;
                <asp:Button ID="btnSend" runat="server" CssClass="AdminLargeButton" OnClick="btnSend_Click" Text='<%$ Code: AdminResources.NewsletterManager_btnSend %>'></asp:Button>
            </CustomActionsTemplate>
            <ContentTemplate>
            <div  class="AdminGridFooterContent">
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
                                    <asp:Label ID="lblTitle" runat="server" Text='<%$ Code: AdminResources.NewsletterManager_lblTitle %>' AssociatedControlID="txtTitle"></asp:Label>
                                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredTitle" runat="server" CssClass="ValidatorAdapter" ValidationGroup="Modification"
                                        ControlToValidate="txtTitle" SetFocusOnError="true" Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.NewsletterManager_requiredTitle %>'></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="lblMailFrom" runat="server" Text='<%$ Code: AdminResources.NewsletterManager_lblMailFrom %>' AssociatedControlID="txtMailFrom"></asp:Label>
                                    <asp:TextBox ID="txtMailFrom" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredMailFrom" runat="server" CssClass="ValidatorAdapter" ValidationGroup="Modification"
                                        ControlToValidate="txtMailFrom" SetFocusOnError="true" Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.NewsletterManager_requiredMailFrom %>'></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="lblTestAddress" runat="server" Text='<%$ Code: AdminResources.NewsletterManager_lblTestAddress %>' AssociatedControlID="txtTestAddress"></asp:Label>
                                    <asp:TextBox ID="txtTestAddress" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredTestAddress" runat="server" CssClass="ValidatorAdapter" ValidationGroup="Modification"
                                        ControlToValidate="txtTestAddress" SetFocusOnError="true" Display="Static" Text="!" ErrorMessage='<%$ Code: AdminResources.NewsletterManager_requiredTestAddress %>'></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="lblRoles" AssociatedControlID="chkRoles" runat="server" Text='<%$ Code: AdminResources.NewsletterManager_lblRoles %>'></asp:Label>
                                    <div>
                                        <asp:CheckBoxList BorderStyle="None" BorderWidth="0px" ID="chkRoles" runat="server"
                                            DataTextField="RoleName" DataValueField="RoleId" RepeatColumns="3" RepeatLayout="Table"
                                            RepeatDirection="Horizontal" TextAlign="Right">
                                        </asp:CheckBoxList>
                                    </div>
                                </p>
                                <p>
                                    <asp:Label ID="lblTextOnly" AssociatedControlID="chkRoles" runat="server" Text='<%$ Code: AdminResources.NewsletterManager_lblTextOnly %>'></asp:Label>
                                    <div>
                                        <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkTextOnly" runat="server"
                                            TextAlign="Right"></asp:CheckBox></div>
                                </p>
                                <p>
                                    <asp:Label ID="lblTextContent" runat="server" Text='<%$ Code: AdminResources.NewsletterManager_lblTextContent %>' AssociatedControlID="txtContent"></asp:Label>
                                    <asp:TextBox Rows="5" TextMode="MultiLine" runat="server" ID="txtContent"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Label ID="lblContent" runat="server" Text='<%$ Code: AdminResources.NewsletterManager_lblContent %>' AssociatedControlID="radContent"></asp:Label>
                                    <div style="float: left; width: 75%;"><mono:CustomRadEditor Width="100%" ID="radContent" ToolBarMode="ShowOnFocus" EditorHeight="500px" runat="server" ContentAreaMode="Iframe"></mono:CustomRadEditor></div>
                                </p>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
                <br />
                <b style="color: Red;">
                    <asp:Label ID="labMessage" runat="server"></asp:Label></b>
            </ContentTemplate>
        </monox:GridViewEditBox>
    </div>
</asp:Content>
