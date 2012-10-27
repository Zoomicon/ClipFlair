<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.OpenIDLoginModule" Codebehind="OpenIDLoginModule.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>
<%@ Register Assembly="DotNetOpenAuth" Namespace="DotNetOpenAuth.OpenId.RelyingParty" TagPrefix="rp" %>

<table cellpadding="0" cellspacing="0" class="login input-form">
    <tr>
        <td>
            <div id="Div1" runat="server">
                <asp:ValidationSummary ID="validationSummaryOpenId" runat="server" DisplayMode="BulletList" CssClass="validationSummary" ValidationGroup="OpenId" />
            </div>
        </td>
    </tr>
    <tr>
        <td class="login-left-section">
            <asp:Panel runat="server" ID="pnlOpenIdContainer" Width="100%">
                <div class="open-id">
                    <h2><%= MonoSoftware.MonoX.Resources.DefaultResources.Login_OpenId_Title%></h2>                    
                    <dl>
                        <dd class="openid-container">
                            <label><%= MonoSoftware.MonoX.Resources.DefaultResources.Login_OpenId_Login %></label>
                            <rp:OpenIdTextBox ID="txtOpenId" runat="server" RequestCountry="Request" RequestFullName="Request" RequestNickname="Request" 
                            RequestEmail="Require" RequestGender="Request" RequestPostalCode="Request" RequestTimeZone="Request" CssClass="openid-textbox" />
                            <asp:RequiredFieldValidator ID="vldRequiredOpenId" runat="server" ControlToValidate="txtOpenId" Text="!" SetFocusOnError="true" ValidationGroup="OpenId" CssClass="validator ValidatorAdapter" Display="None" />
                            <asp:CustomValidator ID="vldCustomOpenId" runat="server" ControlToValidate="txtOpenId" Text="!" SetFocusOnError="true" ValidationGroup="OpenId" CssClass="validator ValidatorAdapter" Display="None" />
                        </dd>
                        <dd class="example">
                            <span><%= MonoSoftware.MonoX.Resources.DefaultResources.Login_OpenId_Example%></span>&nbsp;http://your.name.myopenid.com
                            <label style="height: 20px;">&nbsp;</label>
                        </dd>
                        <dd>
                            <label style="height: 20px;">&nbsp;</label>
                            <asp:CheckBox runat="server" ID="chkOpenIdRememberMe" style="float: left; margin-top: 4px;" />
                            <label for="<%= chkOpenIdRememberMe.ClientID %>" runat="server" style="margin-top: 5px;" ><%= MonoSoftware.MonoX.Resources.DefaultResources.Login_RememberMe %></label>
                        </dd>
                        <dd style="float: right; margin: 21px 10px 0px 0px;">
                            <div style="float: right;"><MonoX:StyledButton ID="btnOpenIdLogin" runat="server" CssClass="CssFormButton" ValidationGroup="OpenId" Text="<% $Code:DefaultResources.Login_Login %>"></MonoX:StyledButton></div>
                            <div style="float: right; padding: 15px 5px 0px 0px;">
                                <a href="https://www.myopenid.com/signup" title="<%= MonoSoftware.MonoX.Resources.DefaultResources.Login_OpenId_Register_ToolTip %>"><%= MonoSoftware.MonoX.Resources.DefaultResources.Login_OpenId_Register %></a>
                            </div>
                        </dd>
                    </dl>                    
                </div>
            </asp:Panel> 
        </td>
    </tr>
</table>    
