<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.CaptchaModule" 
    Codebehind="CaptchaModule.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:UpdatePanel ID="upCaptcha" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <dl class="captcha">
            <!--CLIPFLAIR<h3><%= DefaultResources.Captcha_ImageTitle %></h3>-->
            <dd class="clearfix">
                <div class="image-captcha float-left">
                    <asp:Image ID="imgCaptcha" runat="server" />
                </div>
                <!--CLIPFLAIR<div class="float-left empty-space">&nbsp;</div>-->
                <div class="captcha-input float-right" style="width: 48%;">
                    <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CausesValidation="false" CssClass="refresh" />
                    <asp:TextBox ID="txtCaptchaCode" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqCaptchaCode" runat="server" ControlToValidate="txtCaptchaCode" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="dynamic" Text="!"></asp:RequiredFieldValidator>
                </div>
            </dd>
            <dd class="padding-top">
                <label class="italic-style"><%= DefaultResources.Captcha_ShortInfo %></label>
                <!--CLIPFLAIR<hr />--><br />
                
                <label class="italic-style"><%= DefaultResources.Captcha_LongInfo %></label>
            </dd>
            <dd id="rowInvalidCaptcha" runat="server">
                <asp:Label ID="labInvalidCaptcha" runat="server"></asp:Label>
            </dd>
        </dl>
    </ContentTemplate>
</asp:UpdatePanel>