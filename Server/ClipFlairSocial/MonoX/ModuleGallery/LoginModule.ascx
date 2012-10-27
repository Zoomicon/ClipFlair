<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.LoginModule" Codebehind="LoginModule.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<div runat="server">
    <asp:ValidationSummary ID="validationSummaryLogin" runat="server" DisplayMode="BulletList" CssClass="validationSummary" ValidationGroup="Login" />
</div>
<table cellpadding="0" cellspacing="0" class="login login-form">
 
    <tr>
        <td>
            
            <asp:Panel runat="server" ID="pnlLoginContainer">
                <asp:Login ID="ctlLogin" runat="server" Width="100%">
                    <LayoutTemplate>
                    <div class="regualar-login">
                        <!--<h2><%# MonoSoftware.MonoX.Resources.DefaultResources.Login_Title%></h2>-->
                        <div class="log_wrap">

                        <div class="log_cont">
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWEUsername" runat="server"
                                  TargetControlID="UserName"
                                  WatermarkText="email or username"
                                  WatermarkCssClass="watermarked" />                                
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldRequiredUserName" runat="server" ControlToValidate="UserName" Text="!" SetFocusOnError="true" ValidationGroup="Login" CssClass="validator ValidatorAdapter" Display="Dynamic" ErrorMessage="<%# String.Format(MonoSoftware.MonoX.Resources.DefaultResources.ValidationMessage_RequiredField, MonoSoftware.MonoX.Resources.DefaultResources.Global_UserName) %>" />
                            </div>

                            <div class="log_cont">
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWEPassword" runat="server"
                                  TargetControlID="password"
                                  WatermarkText=" "
                                  WatermarkCssClass="watermarked_psw" />                                
                                <asp:TextBox runat="server" ID="Password" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldRequiredPassword" runat="server" ControlToValidate="Password" Text="!" SetFocusOnError="true" ValidationGroup="Login" CssClass="validator ValidatorAdapter" Display="Dynamic" ErrorMessage="<%# String.Format(MonoSoftware.MonoX.Resources.DefaultResources.ValidationMessage_RequiredField, MonoSoftware.MonoX.Resources.DefaultResources.Global_Password) %>" />
                           </div>
                            
                        </div>
                        <div class="log_wrap_2">                         
                            <asp:CheckBox runat="server" ID="RememberMe" style="float: left;"  />
                            <asp:Label ID="lblRememberMe" AssociatedControlID="RememberMe" runat="server" style="width: auto !important; font-size:11px;" Text='<%# MonoSoftware.MonoX.Resources.DefaultResources.Login_RememberMe %>'></asp:Label> | 
                            <asp:HyperLink ID="lnkForgotPassword" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(PasswordRecoveryPageUrl) %>' CssClass="member-link"><%= MonoSoftware.MonoX.Resources.DefaultResources.Login_PasswordRecoveryText%></asp:HyperLink>
                            
                            <div style="float:right; margin-right:-2px;">
                                <MonoX:StyledButton ID="Login" CommandName="Login" runat="server" Text='<%# MonoSoftware.MonoX.Resources.DefaultResources.Login_Login %>' ValidationGroup="Login"></MonoX:StyledButton>     
                            </div>
                        
                        </div>
                        
                    </div>
                    </LayoutTemplate>
                </asp:Login>
            </asp:Panel>             
        </td>        
    </tr>
</table>    
