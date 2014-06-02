    <%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.MembershipEditor" Codebehind="MembershipEditor.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"%>

<div class="login membership-module-container input-form">
<div id="Div1" runat="server">
    <asp:ValidationSummary ID="validationSummary" CssClass="validation-summary" runat="server" />
</div>  
<div class="register">
    <h2><%= Page.User.Identity.IsAuthenticated ? DefaultResources.MembershipEditor_Title_UpdateAccount : DefaultResources.MembershipEditor_Title_CreateAccount %></h2>
    <dl>
        <dd>
            <label for="<%= txtUserName.ClientID %>"><%= DefaultResources.MembershipEditor_UserName %></label>
            <asp:Label ID="lblUserName" runat="server"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="vldRequiredUserName" runat="server" ControlToValidate="txtUserName" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <asp:CustomValidator ID="vldCustomUserName" runat="server" ControlToValidate="txtUserName" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
        </dd>
        <dd>
            <label for="<%= txtPassword.ClientID %>"><%= DefaultResources.MembershipEditor_Password %></label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="vldRequiredPassword" runat="server" ControlToValidate="txtPassword" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
        </dd>
        <dd>
            <label for="<%= txtRepeatPassword.ClientID %>"><%= DefaultResources.MembershipEditor_RepeatPassword %></label>
            <asp:TextBox ID="txtRepeatPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="vldRequiredRepeatPassword" runat="server" ControlToValidate="txtRepeatPassword" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <asp:CompareValidator ID="vldCompareRepeatPassword" runat="server" ControlToValidate="txtRepeatPassword" Text="!" SetFocusOnError="true" ControlToCompare="txtPassword" Operator="Equal" CssClass="validator ValidatorAdapter" Display="Dynamic"></asp:CompareValidator>
        </dd>
        <dd>
            <label for="<%= txtEmail.ClientID %>"><%= DefaultResources.MembershipEditor_Email %></label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="vldRequiredEmail" runat="server" ControlToValidate="txtEmail" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="vldRegexEmail" runat="server" ControlToValidate="txtEmail" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
        </dd>
        <asp:PlaceHolder ID="plhRememberMe" runat="server">
            <dd>
                <asp:CheckBox ID="chkRememberMe" runat="server"/>
                <label for="<%= chkRememberMe.ClientID %>"><%= DefaultResources.MembershipEditor_RememberMe %></label>                
            </dd>
        </asp:PlaceHolder>
        <dd>
            <asp:Label ID="labInfo" runat="server"></asp:Label>
        </dd>
    </dl>
    <div data-inline="true">
        <MonoX:StyledButton ID="btnCreateAccount" runat="server" EnableNativeButtonMode="true"></MonoX:StyledButton>
        <MonoX:StyledButton ID="btnUpdateAccount" runat="server" EnableNativeButtonMode="true"></MonoX:StyledButton>
    </div>
</div>    
</div>
