<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="LoginSocial.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.LoginSocial" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>
<%@ Register Assembly="MonoSoftware.Web" Namespace="MonoSoftware.Web.Controls" TagPrefix="MonoXControls" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<h3><%= DefaultResources.LoginSocial_SubTitle %></h3>
<div class="input-form">
    <div class="social-login clearfix">
        <div class="social-button-holder">
            <asp:LinkButton ID="btnFacebook" runat="server" CausesValidation="false" OnClick="btn_Click" class="facebook" CommandArgument="FACEBOOK">
                <img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.img.Sn.facebook_login_png) %>" alt="<%= DefaultResources.LoginSocial_LoginWithFacebook %>" />
                <span><%= DefaultResources.LoginSocial_LoginWithFacebook %></span>
            </asp:LinkButton>
        </div>
        <div class="social-button-holder">
            <asp:LinkButton ID="btnGoogle" runat="server" CausesValidation="false" OnClick="btn_Click" class="google" CommandArgument="GOOGLE">
                <img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.img.Sn.google_login_png) %>" alt="<%= DefaultResources.LoginSocial_LoginWithGoogle %>" />
                <span><%= DefaultResources.LoginSocial_LoginWithGoogle %></span>
            </asp:LinkButton>
        </div>
        <div class="social-button-holder">
            <asp:LinkButton ID="btnLinkedIn" runat="server" CausesValidation="false" OnClick="btn_Click" class="linkedin" CommandArgument="LINKEDIN">
                <img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.img.Sn.linkedin_login_png) %>" alt="<%= DefaultResources.LoginSocial_LoginWithLinkedIn %>" />
                <span><%= DefaultResources.LoginSocial_LoginWithLinkedIn %></span>
            </asp:LinkButton>
        </div>
        <div class="social-button-holder">
            <asp:LinkButton ID="btnTwitter" runat="server" CausesValidation="false" OnClick="btn_Click" class="twitter" CommandArgument="TWITTER">
                <img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.img.Sn.twitter_login_png) %>" alt="<%= DefaultResources.LoginSocial_LoginWithTwitter %>" />
                <span><%= DefaultResources.LoginSocial_LoginWithTwitter %></span>
            </asp:LinkButton>
        </div>
    </div>
    <asp:Panel runat="server" ID="pnlEmail" Visible="true">
        <div class="social-mail">
            <hr />
            <h3><%= DefaultResources.LoginSocial_Title %></h3>
            <dl>
                <dd>
                    <asp:Literal runat="server" ID="ltlEmailInfo"></asp:Literal>
                </dd>
                <dd>
                    <asp:Label ID="lblEmail" AssociatedControlID="txtEmail" runat="server"><%= DefaultResources.LoginSocial_Email %></asp:Label>
                    <div class="control-holder"
                        <img src="../App_Themes/ClipFlair/img/icon-mail-activation.png" alt="Password icon">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="vldRequiredEmail" runat="server" ControlToValidate="txtEmail" Text="!" CssClass="validator ValidatorAdapter" Display="Dynamic" />
                        <MonoXControls:RegExValidator ID="regexEmail" CssClass="ValidatorAdapter"  ControlToValidate="txtEmail" Display="Dynamic" Text="!" ValidationType="EMail" runat="server"></MonoXControls:RegExValidator>
                    </div>
                </dd>
                <dd>
                	<MonoX:StyledButton ID="btnSaveEmail" runat="server" CssClass="styled-button" OnClick="btnSaveEmail_Click" Text='<%$ Code:DefaultResources.LoginSocial_ProceedButton %>'></MonoX:StyledButton>
                </dd>
                <dd>
                    <asp:literal ID="ltlWarning" runat="server"></asp:literal>
                </dd>
            </dl>
        </div>    
    </asp:Panel>
</div>