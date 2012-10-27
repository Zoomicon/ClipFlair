<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginModule.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.LoginModule" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>


<div id="Div1" runat="server">
    <asp:ValidationSummary ID="validationSummaryLogin" runat="server" DisplayMode="BulletList" CssClass="validation-summary" ValidationGroup="Login" />
</div>
<asp:Panel runat="server" ID="pnlLoginContainer">
    <asp:Login ID="ctlLogin" runat="server" Width="100%">
        <LayoutTemplate>
        <div class="regualar-login">
            <dl>
                <dd>
                    <asp:Label ID="lblUserName" AssociatedControlID="UserName" runat="server" Text='<%# MonoSoftware.MonoX.Resources.DefaultResources.Login_UserName %>'></asp:Label>
                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vldRequiredUserName" runat="server" ControlToValidate="UserName" Text="!" SetFocusOnError="true" ValidationGroup="Login" CssClass="validator ValidatorAdapter" Display="Dynamic" ErrorMessage="<%# String.Format(MonoSoftware.MonoX.Resources.DefaultResources.ValidationMessage_RequiredField, MonoSoftware.MonoX.Resources.DefaultResources.Global_UserName) %>" />
                </dd>
                <dd>
                    <asp:Label ID="lblPassword" AssociatedControlID="Password" runat="server" Text='<%# MonoSoftware.MonoX.Resources.DefaultResources.Login_Password %>'></asp:Label>
                    <asp:TextBox runat="server" ID="Password" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vldRequiredPassword" runat="server" ControlToValidate="Password" Text="!" SetFocusOnError="true" ValidationGroup="Login" CssClass="validator ValidatorAdapter" Display="Dynamic" ErrorMessage="<%# String.Format(MonoSoftware.MonoX.Resources.DefaultResources.ValidationMessage_RequiredField, MonoSoftware.MonoX.Resources.DefaultResources.Global_Password) %>" />
                </dd>
                <dd>
                    <fieldset data-role="controlgroup">
                        <div data-inline="true">
                            <asp:CheckBox runat="server" ID="RememberMe"/>
                        </div>
                        <asp:Label ID="lblRememberMe" AssociatedControlID="RememberMe" runat="server" Text='<%# MonoSoftware.MonoX.Resources.DefaultResources.Login_RememberMe %>'></asp:Label>
                    </fieldset>
                </dd>
            </dl>
            <asp:Button ID="Login" CommandName="Login" runat="server" Text='<%# MonoSoftware.MonoX.Resources.DefaultResources.Login_Login %>' ValidationGroup="Login"></asp:Button>
            <asp:HyperLink data-role="button" ID="lnkRegister" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(RegisterPageUrl) %>' CssClass="member-link"><%= MonoSoftware.MonoX.Resources.DefaultResources.Login_Register %></asp:HyperLink>
            <asp:HyperLink data-role="button" ID="lnkForgotPassword" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(PasswordRecoveryPageUrl) %>' CssClass="member-link"><%= MonoSoftware.MonoX.Resources.DefaultResources.Login_PasswordRecoveryText%></asp:HyperLink>
        </div>
        </LayoutTemplate>
    </asp:Login>
</asp:Panel>             
