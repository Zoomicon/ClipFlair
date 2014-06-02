<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Login.master"
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.PasswordRecovery" 
    Codebehind="PasswordRecovery.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="MonoX" TagName="PasswordRecovery" Src="~/MonoX/ModuleGallery/Membership/PasswordRecovery.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
    <div class="fancybox-container">
        <div class="row-fluid">
            <div class="span12 clearfix">       
                <MonoX:PasswordRecovery runat="server" ID="ctlPasswordRecovery" />
            </div>           
        </div> 
    </div>
</asp:Content>