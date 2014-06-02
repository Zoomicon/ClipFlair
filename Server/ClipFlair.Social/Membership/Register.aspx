<%@ Page 
    Language="C#" 
    MasterPageFile="~/App_MasterPages/ClipFlair/Login.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.Register" 
    Codebehind="Register.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>    
<%@ Register TagPrefix="MonoX" TagName="Membership" Src="~/MonoX/ModuleGallery/Membership/MembershipEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="LoginSocial" Src="~/MonoX/ModuleGallery/LoginSocial.ascx" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"%>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
    <div class="fancybox-container">
        <div class="row-fluid">
            <div class="span6 clearfix">       
                <MonoX:Membership runat="server" ID="ctlMembershipEditor" />
            </div>
            <div class="span6 clearfix" style="position: relative;"> 
                <div id="rowRPX" runat="server" Visible="<% $Code: MonoSoftware.MonoX.ApplicationSettings.EnableUserRegistration %>">
                    <!--<div class="or-use">
                        <hr />
                    </div>-->
                    <div class="user-account login-social" >
                        <MonoX:LoginSocial runat="server" ID="ctlLoginSocial" />
                        <div class="italic-style"><asp:Literal ID="Literal1" runat="server" Visible="<% $Code: MonoSoftware.MonoX.ApplicationSettings.EnableUserRegistration %>" Text="<% $Code: PageResources.Login_RpxWarning %>"></asp:Literal></div>
                    </div>
                    <div class="gotoLogin">
                        <!--needs localization-->
                        <p>Already a user? Sign in <a href="../Login.aspx">here</a>.</p>
                    </div>
                </div>
            </div>
        </div> 
    </div>
</asp:Content>
