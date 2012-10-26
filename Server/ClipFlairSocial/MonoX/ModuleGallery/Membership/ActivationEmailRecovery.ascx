<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.ActivationEmailRecovery" Codebehind="ActivationEmailRecovery.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"%>

<div class="login membership-module-container input-form">
    <div id="Div1" runat="server">
        <asp:ValidationSummary ID="validationSummary" CssClass="validation-summary" runat="server" />
    </div>
    <div class="email-recovery">
        <h2 class="title"><%= DefaultResources.ActivationEmailRecovery_Title %></h2>
        <dl>
            <dd>
                <label for="<%= txtUserName.ClientID %>"><%= DefaultResources.ActivationEmailRecovery_UserName %></label>
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="vldRequiredUserName" runat="server" ControlToValidate="txtUserName" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />                
            </dd>            
            <dd>
                <asp:Label ID="labInfo" runat="server"></asp:Label>
            </dd>
        </dl>
        <div class="input-form">
            <div class="button-holder">
                <MonoX:StyledButton ID="btnSend" runat="server" CssClass="CssFormButton"></MonoX:StyledButton>
            </div>
        </div>
    </div>
</div>        

