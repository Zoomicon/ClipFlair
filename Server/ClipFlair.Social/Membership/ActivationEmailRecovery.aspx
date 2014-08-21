<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.ActivationEmailRecovery" 
    Codebehind="ActivationEmailRecovery.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="MonoX" TagName="ActivationEmailRecovery" Src="~/MonoX/ModuleGallery/Membership/ActivationEmailRecovery.ascx" %>

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
                    <MonoX:ActivationEmailRecovery runat="server" ID="ctlActivationEmailRecovery" />
                </div>           
            </div> 
        </div>
    </div>
</asp:Content>
