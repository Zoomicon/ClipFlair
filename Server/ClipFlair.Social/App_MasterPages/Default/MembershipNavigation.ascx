<%@ Control 
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="MembershipNavigation.ascx.cs"
    Inherits="MonoSoftware.MonoX.MasterPages.MembershipNavigation" %>
<%@ Register Src="~/MonoX/controls/SeoLoginStatus.ascx" TagPrefix="monox" TagName="SeoLoginStatus" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<div class="membership-navigation">
    <asp:PlaceHolder ID="panAnonymousTemplate" runat="server">
        <monox:SeoLoginStatus ID="loginStatus1" runat="server" />
        <asp:HyperLink runat="server" ID="lnkRegister" Visible="<% $Code: MonoSoftware.MonoX.ApplicationSettings.EnableUserRegistration %>"></asp:HyperLink>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="panLoggedInTemplate" runat="server">
        <asp:HyperLink runat="server" ID="lnkProfile">
            <img runat="server" src="~/App_Themes/Default/img/icon-profile.png" alt="Profile" />
        </asp:HyperLink>
        <asp:HyperLink runat="server" ID="lnkMessages">
            <img runat="server" src="~/App_Themes/Default/img/icon-message.png" alt="Messages" />
        </asp:HyperLink>    
        <monox:SeoLoginStatus runat="server" ID="loginStatus2"/>
    </asp:PlaceHolder>
    <div id="panLoginName" runat="server" class="login-name">
        <asp:LoginName ID="loginName" runat="server" /> 
    </div>
</div>