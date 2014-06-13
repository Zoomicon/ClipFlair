<%@ Page 
    Language="C#" 
    MasterPageFile="~/App_MasterPages/ClipFlair/Default.master"
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.PasswordRecovery" 
    Codebehind="PasswordRecovery.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="MonoX" TagName="PasswordRecovery" Src="~/MonoX/ModuleGallery/Membership/PasswordRecovery.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
    <div class="container-highlighter" style="background-color:#38595b">
        <div class="container">
            <p></p>
        </div>              
    </div>
    <div class="container">
        <div class="fancybox-container login-cont">
            <div class="row-fluid">
                <div class="span12 clearfix">       
                    <MonoX:PasswordRecovery runat="server" ID="ctlPasswordRecovery" />
                </div>           
            </div> 
        </div>
    </div>
</asp:Content>