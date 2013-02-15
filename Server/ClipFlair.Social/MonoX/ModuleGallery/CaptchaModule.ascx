<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.CaptchaModule" 
    Codebehind="CaptchaModule.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:UpdatePanel ID="upCaptcha" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <table class="captcha">
            <tr>
                <td class="captcha-image">
                    <div class="holder">
                        <div class="image-captcha">
                            <asp:Image ID="imgCaptcha" runat="server" />
                        </div>
                        <div class="refresh">
                            <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CausesValidation="false" />
                        </div>
                    </div>
                    <div class="captcha-textbox">
                        <asp:TextBox ID="txtCaptchaCode" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqCaptchaCode" runat="server" ControlToValidate="txtCaptchaCode"
                        SetFocusOnError="true" CssClass="ValidatorAdapter" Display="dynamic" Text="!"></asp:RequiredFieldValidator>
                    </div>
                </td>
                <td class="captcha-description" rowspan="2">
                    <h3><%= DefaultResources.Captcha_ImageTitle %></h3>
                    <strong><%= DefaultResources.Captcha_ShortInfo %></strong><br />
                    <%= DefaultResources.Captcha_LongInfo %>
                </td>
            </tr>            
            <tr id="rowInvalidCaptcha" runat="server">
                <td colspan="2">
                    <span style="color: Red; font-size: 11px;">
                        <asp:Label ID="labInvalidCaptcha" runat="server"></asp:Label></span>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

