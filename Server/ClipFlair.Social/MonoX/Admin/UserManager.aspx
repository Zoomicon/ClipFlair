<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Title="User management" Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master"
    AutoEventWireup="True" CodeBehind="UserManager.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.UserManager"
    Theme="DefaultAdmin" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %> 
<%@ Register Assembly="MonoSoftware.Web" Namespace="MonoSoftware.Web.Controls" TagPrefix="mono" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Register Src="~/MonoX/Admin/controls/GridViewEditBox.ascx" TagPrefix="monox"
    TagName="GridViewEditBox" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

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
                <tr id="rowAppFilter" runat="server">
                    <td style="width: 20%; padding-bottom: 2px;">
                        <%-- Note: Hidden but left for possible future use --%>
                        <asp:Literal ID="labAppName" runat="server" Text='<%$ Code: AdminResources.Label_Application %>'></asp:Literal>&nbsp;
                    </td>
                    <td style="width: 60%; padding-bottom: 2px;">
                        <asp:DropDownList ID="ddlApps" runat="server" AutoPostBack="true" CssClass="searchselect"
                            OnSelectedIndexChanged="ddlApps_SelectedIndexChanged" Width="306px">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <asp:Literal ID="labRoles" runat="server" Text='<%$ Code: AdminResources.UserManager_labRoles %>'></asp:Literal>&nbsp;
                    </td>
                    <td style="width: 60%;">
                        <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="true" CssClass="searchselect"
                            OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" Width="306px">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </CustomFilterTemplate>
        <Columns>
            <mono:LiteGridBoundField DataField="Id" Visible="false" />
            <mono:LiteGridBoundField DataField="UserName" HeaderText='<%$ Code: AdminResources.UserManager_sortUserName %>' SortExpression="UserName" />
            <mono:LiteGridBoundField DataField="Email" HeaderText='<%$ Code: AdminResources.UserManager_sortEMail %>' SortExpression="Email" />
            <mono:LiteGridTemplateField HeaderText='<%$ Code: AdminResources.UserManager_labComment %>' SortExpression="Comment" >
                <ItemTemplate>
                    <div style="max-width: 300px; white-space: normal;">
                        <%# MonoSoftware.MonoX.Utilities.MonoXUtility.GetSubString(Eval("AspnetMembership.Comment") != null ? Eval("AspnetMembership.Comment").ToString() : String.Empty, 0, 500)%>
                    </div>                    
                </ItemTemplate>
            </mono:LiteGridTemplateField>
            <mono:LiteGridBoundField DataField="IsApproved" HeaderText='<%$ Code: AdminResources.UserManager_sortIsApproved %>' SortExpression="IsApproved" />
            <mono:LiteGridBoundField DataField="IsLockedOut" HeaderText='<%$ Code: AdminResources.UserManager_sortIsLockedOut %>' SortExpression="IsLockedOut" />
            <mono:LiteGridBoundField DataField="LastActivityDate" HeaderText='<%$ Code: AdminResources.UserManager_sortLastActivityDate %>' SortExpression="LastActivityDate" />
            <mono:LiteGridBoundField DataField="FailedPasswordAttemptCount" HeaderText='<%$ Code: AdminResources.UserManager_sortFailedPasswordAttempts %>' SortExpression="FailedPasswordAttemptCount" />
            <mono:LiteGridBoundField DataField="FailedPasswordAnswerAttemptCount" HeaderText='<%$ Code: AdminResources.UserManager_sortFailedPasswordAnswerAttempts %>' SortExpression="FailedPasswordAnswerAttemptCount" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Literal ID="labEmptyMessage" runat="server" Text='<%$ Code: AdminResources.UserManager_labEmptyMessage %>'></asp:Literal>
        </EmptyDataTemplate>
        <CustomActionsTemplate>
            <asp:Button ID="btnApproveUser" runat="server" CssClass="AdminLargeButton" OnClick="btnApproveUser_Click" Text='<%$ Code: AdminResources.UserManager_btnApproveUser %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnLockUser" runat="server" CssClass="AdminLargeButton" OnClick="btnLockUser_Click" Text='<%$ Code: AdminResources.UserManager_btnLockUser %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnResetFailed" runat="server" CssClass="AdminLargeButton" Text='<%$ Code: AdminResources.UserManager_btnResetFailed %>' OnClick="btnResetFailed_Click"></asp:Button>
            <br />
        </CustomActionsTemplate>
        <ContentTemplate>
            <asp:PlaceHolder ID="plhModification" runat="server">
                <div class="AdminGridFooterContent input-form">
                <table width="100%" cellpadding="0" cellspacing="6">
                    <tr>
                        <td colspan="3">
                            <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="Modification" ShowSummary="true" />
                        </td>
                    </tr>
                    <asp:PlaceHolder id="plhAppEdit" runat="server" Visible="false">
                    <tr id="rowAppEdit" runat="server">
                        <td width="25%">
                            <asp:Literal ID="labAppNameEdit" runat="server" Text='<%$ Code: AdminResources.Label_Application %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAppEdit" runat="server" CssClass="select" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    </asp:PlaceHolder>
                    <tr>
                        <td>
                            <asp:Literal ID="labUsername" runat="server" Text='<%$ Code: AdminResources.UserManager_labUsername %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="requiredUserName" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true"
                                ControlToValidate="txtUserName" ValidationGroup="Modification" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_requiredUserName %>'></asp:RequiredFieldValidator>
                            <mono:RegExValidator ID="validateUserName" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true"
                                ControlToValidate="txtUserName" ValidationGroup="Modification" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validateUserName %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labMobileAlias" runat="server" Text='<%$ Code: AdminResources.UserManager_labMobileAlias %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobileAlias" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <mono:RegExValidator ID="validateMobileAlias" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                ControlToValidate="txtMobileAlias" ValidationGroup="Modification" ValidationType="alphaNumericWithSymbolsAllowedSpecialChr" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validateMobileAlias %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labMobilePin" runat="server" Text='<%$ Code: AdminResources.UserManager_labMobilePin %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobilePin" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <mono:RegExValidator ID="validateMobilePin" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                ControlToValidate="txtMobilePin" ValidationGroup="Modification" ValidationType="alphaNumericWithSymbolsAllowedSpecialChr" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validateMobilePin %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labIsAnonymous" runat="server" Text='<%$ Code: AdminResources.UserManager_labIsAnonymous %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsAnonymous" runat="server" Width="100%"></asp:CheckBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labLastActivityDate" runat="server" Text='<%$ Code: AdminResources.UserManager_labLastActivityDate %>'></asp:Literal>
                        </td>
                        <td>
                            <rad:RadDatePicker id="txtLastActivityDate" Runat="server" Calendar-Skin="Default2006">                                                    
                                <datepopupbutton ></datepopupbutton>
                            </rad:RadDatePicker>                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labPassword" runat="server" Text='<%$ Code: AdminResources.UserManager_labPassword %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="requiredPassword" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                ControlToValidate="txtPassword" ValidationGroup="Modification" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_requiredPassword %>'></asp:RequiredFieldValidator>
                            <mono:RegExValidator ID="validatePassword" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                ControlToValidate="txtPassword" ValidationGroup="Modification" ValidationType="alphaNumericWithSymbolsAllowedSpecialChr" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validatePassword %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labEmail" runat="server" Text='<%$ Code: AdminResources.UserManager_labEmail %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <mono:RegExValidator ID="validateEmail" runat="server" SetFocusOnError="true"  CssClass="ValidatorAdapter" ControlToValidate="txtEmail"
                                ValidationGroup="Modification" ValidationType="eMail" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validateEmail %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labPasswordQuestion" runat="server" Text='<%$ Code: AdminResources.UserManager_labPasswordQuestion %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPasswordQuestion" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="reqPasswordQuestion" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                ControlToValidate="txtPasswordQuestion" ValidationGroup="Modification" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_reqPasswordQuestion %>'></asp:RequiredFieldValidator>
                            <mono:RegExValidator ID="validatePasswordQuestion" runat="server" CssClass="ValidatorAdapter" SetFocusOnError="true"
                                ControlToValidate="txtPasswordQuestion" ValidationGroup="Modification" ValidationType="alphaNumericWithSymbolsAllowedSpecialChr" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validatePasswordQuestion %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labPasswordAnswer" runat="server" Text='<%$ Code: AdminResources.UserManager_labPasswordAnswer %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPasswordAnswer" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                                <asp:RequiredFieldValidator ID="reqPasswordAnswer" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                ControlToValidate="txtPasswordAnswer" ValidationGroup="Modification" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_reqPasswordAnswer %>'></asp:RequiredFieldValidator>

                            <mono:RegExValidator ID="validatePasswordAnswer" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                ControlToValidate="txtPasswordAnswer" ValidationGroup="Modification" ValidationType="alphaNumericWithSymbolsAllowedSpecialChr" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validatePasswordAnswer %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labIsApproved" runat="server" Text='<%$ Code: AdminResources.UserManager_labIsApproved %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" runat="server" Width="100%"></asp:CheckBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labIsLockedOut" runat="server" Text='<%$ Code: AdminResources.UserManager_labIsLockedOut %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsLockedOut" runat="server" Width="100%"></asp:CheckBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labCreateDate" runat="server" Text='<%$ Code: AdminResources.UserManager_labCreateDate %>'></asp:Literal>
                        </td>
                        <td>
                            <rad:RadDatePicker id="txtCreateDate" Runat="server" Calendar-Skin="Default2006">                                                    
                                <datepopupbutton ></datepopupbutton>
                            </rad:RadDatePicker>                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labLastLoginDate" runat="server" Text='<%$ Code: AdminResources.UserManager_labLastLoginDate %>'></asp:Literal>
                        </td>
                        <td>
                            <rad:RadDatePicker id="txtLastLoginDate" Runat="server" Calendar-Skin="Default2006">                                                    
                                <datepopupbutton ></datepopupbutton>
                            </rad:RadDatePicker>                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labLastPasswordChangedDate" runat="server" Text='<%$ Code: AdminResources.UserManager_labLastPasswordChangedDate %>'></asp:Literal>
                        </td>
                        <td>
                            <rad:RadDatePicker id="txtLastPasswordChangedDate" Runat="server" Calendar-Skin="Default2006">                                                    
                                <datepopupbutton ></datepopupbutton>
                            </rad:RadDatePicker>                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labLastLockoutDate" runat="server" Text='<%$ Code: AdminResources.UserManager_labLastLockoutDate %>'></asp:Literal>
                        </td>
                        <td>
                            <rad:RadDatePicker id="txtLastLockoutDate" Runat="server" Calendar-Skin="Default2006">                                                    
                                <datepopupbutton ></datepopupbutton>
                            </rad:RadDatePicker>                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labFailedPasswordAttemptCount" runat="server" Text='<%$ Code: AdminResources.UserManager_labFailedPasswordAttemptCount %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFailedPasswordAttemptCount" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="requiredFailedPasswordAttemptCount" runat="server" CssClass="ValidatorAdapter"
                                SetFocusOnError="true" ControlToValidate="txtFailedPasswordAttemptCount" ValidationGroup="Modification" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_requiredFailedPasswordAttemptCount %>'></asp:RequiredFieldValidator>
                            <mono:RegExValidator ID="validateFailedPasswordAttemptCount" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter"
                                ControlToValidate="txtFailedPasswordAttemptCount" ValidationGroup="Modification"
                                ValidationType="NumericInt32" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validateFailedPasswordAttemptCount %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labFailedPasswordAttemptWindowStart" runat="server" Text='<%$ Code: AdminResources.UserManager_labFailedPasswordAttemptWindowStart %>'></asp:Literal>
                        </td>
                        <td>
                            <rad:RadDatePicker id="txtFailedPasswordAttemptWindowStart" Runat="server" Calendar-Skin="Default2006">                                                    
                                <datepopupbutton ></datepopupbutton>
                            </rad:RadDatePicker>                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labFailedPasswordAnswerAttemptCount" runat="server" Text='<%$ Code: AdminResources.UserManager_labFailedPasswordAnswerAttemptCount %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFailedPasswordAnswerAttemptCount" runat="server" CssClass="input"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="requiredFailedPasswordAnswerAttemptCount" runat="server"
                                SetFocusOnError="true" ControlToValidate="txtFailedPasswordAnswerAttemptCount" CssClass="ValidatorAdapter"
                                ValidationGroup="Modification" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_requiredFailedPasswordAnswerAttemptCount %>'></asp:RequiredFieldValidator>
                            <mono:RegExValidator ID="validateFailedPasswordAnswerAttemptCount" runat="server" CssClass="ValidatorAdapter"
                                SetFocusOnError="true" ControlToValidate="txtFailedPasswordAnswerAttemptCount"
                                ValidationGroup="Modification" ValidationType="NumericInt32" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validateFailedPasswordAnswerAttemptCount %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labFailedPasswordAnswerAttemptWindowStart" runat="server" Text='<%$ Code: AdminResources.UserManager_labFailedPasswordAnswerAttemptWindowStart %>'></asp:Literal>
                        </td>
                        <td>
                            <rad:RadDatePicker id="txtFailedPasswordAnswerAttemptWindowStart" Runat="server" Calendar-Skin="Default2006">                                                    
                                <datepopupbutton ></datepopupbutton>
                            </rad:RadDatePicker>                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labComment" runat="server" Text='<%$ Code: AdminResources.UserManager_labComment %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" CssClass="input"
                                Rows="5" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <mono:RegExValidator ID="validateComment" runat="server" SetFocusOnError="true" CssClass="ValidatorAdapter" ControlToValidate="txtComment"
                                ValidationGroup="Modification" ValidationType="alphaNumericWithSymbolsAllowedSpecialChr" Text="!" ErrorMessage='<%$ Code: AdminResources.UserManager_validateComment %>'>
                            </mono:RegExValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="labUserRoles" runat="server" Text='<%$ Code: AdminResources.UserManager_labUserRoles %>'></asp:Literal>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ListBox runat="server" ID="lbRoles" SelectionMode="Multiple" Width="170px" Rows="5"
                                            CssClass="select"></asp:ListBox>
                                    </td>
                                    <td style="vertical-align: middle;">
                                        <asp:ImageButton runat="server" ID="addImg" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.UserManagement.frm_add_gif %>"
                                            AlternateText="Add" ToolTip="Add" />
                                        <br />
                                        <asp:ImageButton runat="server" ID="removeImg" ImageUrl="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.UserManagement.frm_remove_gif %>"
                                            AlternateText="Remove" ToolTip="Remove" />
                                    </td>
                                    <td>
                                        <asp:ListBox runat="server" ID="lbUserInRoles" Width="170px" Rows="5" SelectionMode="Multiple"
                                            CssClass="select"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="labAvailableRoles" runat="server" Text='<%$ Code: AdminResources.UserManager_labAvailableRoles %>'></asp:Literal>
                                    </td>
                                    <td style="vertical-align: middle;">
                                    </td>
                                    <td>
                                        <asp:Literal ID="labUserInRoles" runat="server" Text='<%$ Code: AdminResources.UserManager_labUserInRoles %>'></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>                    
                </table>
            </div>
            </asp:PlaceHolder>
        </ContentTemplate>
    </monox:GridViewEditBox>
</div>
</asp:Content>
