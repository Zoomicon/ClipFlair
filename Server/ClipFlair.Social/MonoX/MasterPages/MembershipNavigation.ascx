<%@ Control 
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="MembershipNavigation.ascx.cs"
    Inherits="MonoSoftware.MonoX.MasterPages.MembershipNavigation" %>
<%@ Register Src="~/MonoX/controls/SeoLoginStatus.ascx" TagPrefix="monox" TagName="SeoLoginStatus" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<div class="membership-navigation">
    <div id="panLoginName" runat="server" class="login-name">
        <asp:LoginName ID="loginName" runat="server" /> 
    </div>
    <asp:PlaceHolder ID="panAnonymousTemplate" runat="server">
        <monox:SeoLoginStatus ID="loginStatus1" runat="server" />
        <%--!!!--%>
        <asp:HyperLink runat="server" ID="lnkRegister" CssClass="register-link" Visible="<% $Code: MonoSoftware.MonoX.ApplicationSettings.EnableUserRegistration %>"></asp:HyperLink> 
        <%--/!!!--%>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="panLoggedInTemplate" runat="server">
        <div class="logged">
            <asp:HyperLink runat="server" ID="lnkProfile">
                <%--!!!title NEEDS LOCALIZATION--%>
                <img runat="server" src="~/App_Themes/ClipFlair/img/icon-profile.png" alt="Profile" title="visit your profile" class="imgover" />
                <%--/!!!--%>
            </asp:HyperLink>
            <asp:HyperLink runat="server" ID="lnkMessages">
                <%--!!!--%>
                <img runat="server" src="~/App_Themes/ClipFlair/img/icon-message.png" alt="Messages" class="imgover" />
                <%--/!!!--%>
            </asp:HyperLink>    
            <monox:SeoLoginStatus runat="server" ID="loginStatus2"/>
        </div>
    </asp:PlaceHolder> 
    <script type="text/javascript">
        parent.$.fancybox.close();
        // elegxo gia null - p.x. an exei cookie kai se kanei login epitopou isos den yparxei fancybox, min xtipisei lathos to parent.$.fancybox otan paei na kalesei tin close()
    </script>
</div>