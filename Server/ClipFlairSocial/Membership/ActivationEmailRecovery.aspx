<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.ActivationEmailRecovery" 
    Theme="Default"
    Codebehind="ActivationEmailRecovery.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="MonoX" TagName="ActivationEmailRecovery" Src="~/MonoX/ModuleGallery/Membership/ActivationEmailRecovery.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<div class="main input-form"> <!-- Main Start -->
    <MonoX:ActivationEmailRecovery runat="server" ID="ctlActivationEmailRecovery" />
</div>
</asp:Content>
