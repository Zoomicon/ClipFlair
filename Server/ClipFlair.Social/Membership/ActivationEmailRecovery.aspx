<%@ Page 
    Language="C#" 
    MasterPageFile="~/App_MasterPages/ClipFlair/Login.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.ActivationEmailRecovery" 
    Codebehind="ActivationEmailRecovery.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="MonoX" TagName="ActivationEmailRecovery" Src="~/MonoX/ModuleGallery/Membership/ActivationEmailRecovery.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
    <div class="fancybox-container">
        <div class="row-fluid">
            <div class="span12 clearfix">       
                <MonoX:ActivationEmailRecovery runat="server" ID="ctlActivationEmailRecovery" />
            </div>           
        </div> 
    </div>
</asp:Content>
