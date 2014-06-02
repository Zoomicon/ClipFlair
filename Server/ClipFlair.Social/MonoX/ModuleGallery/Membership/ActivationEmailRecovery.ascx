<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.ActivationEmailRecovery"
    Codebehind="ActivationEmailRecovery.ascx.cs" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources"%>
<%@ Register Src="~/MonoX/ModuleGallery/CaptchaModule.ascx" TagPrefix="monox" TagName="CaptchaModule" %>

<div class="user-account">
    <h2 class="title"><%= DefaultResources.ActivationEmailRecovery_Title %></h2>
    <div class="input-form">
        <dl>
            <dd id="Div1" runat="server">
                <asp:ValidationSummary ID="validationSummary" CssClass="validation-summary" runat="server" />
            </dd>
            <dd>
                <label for="<%= txtUserName.ClientID %>" class="inline-label"><%= DefaultResources.ActivationEmailRecovery_UserName %></label>
                <div class="control-holder">
                    <!--!!!CLIPFLAIR--><img runat="server" src="~/App_Themes/ClipFlair/img/icon-mail-activation.png" alt="Password icon" />
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="vldRequiredUserName" runat="server" ControlToValidate="txtUserName" Text="!" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic" />
            </dd>            
            <dd>
                <asp:Label ID="labInfo" runat="server"></asp:Label>
            </dd>
            <dd>
                <monox:CaptchaModule ID="captchaModule" runat="server" />
            </dd>
        </dl>
    </div>
    <MonoX:StyledButton ID="btnSend" runat="server" CssClass="main-button submit-btn float-right"></MonoX:StyledButton>
</div>