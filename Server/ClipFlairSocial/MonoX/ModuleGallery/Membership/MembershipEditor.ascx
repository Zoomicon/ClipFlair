    <%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.MembershipEditor" Codebehind="MembershipEditor.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"%>
<%@ Register Src="~/MonoX/Controls/TimeZonePicker.ascx" TagPrefix="monox" TagName="TimeZonePicker" %>

<div id="Div1" runat="server">
    <asp:ValidationSummary ID="validationSummary" CssClass="validationSummary" runat="server" />
</div> 

<div class="login membership-module-container login-form"> 
<div class="register">
    <h2><%= Page.User.Identity.IsAuthenticated ? DefaultResources.MembershipEditor_Title_UpdateAccount : DefaultResources.MembershipEditor_Title_CreateAccount %></h2>
    <ul class="form-labels">
    
        <li>
            <asp:RequiredFieldValidator ID="vldRequiredUserName" runat="server" ControlToValidate="txtUserName" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <asp:CustomValidator ID="vldCustomUserName" runat="server" ControlToValidate="txtUserName" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <label for="<%= txtUserName.ClientID %>"><%= DefaultResources.MembershipEditor_UserName%></label>
            
        </li>
        <li>
            <asp:RequiredFieldValidator ID="vldRequiredPassword" runat="server" ControlToValidate="txtPassword" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <label for="<%= txtPassword.ClientID %>"><%= DefaultResources.MembershipEditor_Password%></label>
        </li>       
        <li>
            <asp:RequiredFieldValidator ID="vldRequiredRepeatPassword" runat="server" ControlToValidate="txtRepeatPassword" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <asp:CompareValidator ID="vldCompareRepeatPassword" runat="server" ControlToValidate="txtRepeatPassword" Text="!" SetFocusOnError="true" ControlToCompare="txtPassword" Operator="Equal" CssClass="validator ValidatorAdapter" Display="Dynamic"></asp:CompareValidator>
            <label for="<%= txtRepeatPassword.ClientID %>"><%= DefaultResources.MembershipEditor_RepeatPassword%></label>
        </li>
        <li>
            <asp:RequiredFieldValidator ID="vldRequiredEmail" runat="server" ControlToValidate="txtEmail" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <asp:RegularExpressionValidator ID="vldRegexEmail" runat="server" ControlToValidate="txtEmail" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            <label for="<%= txtEmail.ClientID %>"><%= DefaultResources.MembershipEditor_Email%></label>
        </li>
            
    </ul>
    <ul class="form-boxes">
        <li> 
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <asp:Label ID="lblUserName" style="line-height:26px; color:#000;" runat="server"></asp:Label>
        </li>
        <li>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            
        </li>
        <li>
            <asp:TextBox ID="txtRepeatPassword" runat="server" TextMode="Password"></asp:TextBox>
            
        </li>
        <li>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            
        </li>
    </ul>
    
    <div class="login-form" >
        <asp:PlaceHolder ID="plhRememberMe" runat="server">

                <asp:CheckBox ID="chkRememberMe" runat="server" style="float: left;  margin-left:151px; " />
                <label style="width: auto !important; font-size:11px; " for="<%= chkRememberMe.ClientID %>"><%= DefaultResources.MembershipEditor_RememberMe %></label>
        </asp:PlaceHolder>
        
        <asp:Label ID="labInfo" runat="server"></asp:Label>
        
        <div class="button-holder" style="float: right;">
            <MonoX:StyledButton ID="btnCreateAccount" runat="server" CssClass="CssFormButton"></MonoX:StyledButton>
            <MonoX:StyledButton ID="btnUpdateAccount" runat="server" CssClass="CssFormButton"></MonoX:StyledButton>
        </div>
    </div>
</div>    
</div>
