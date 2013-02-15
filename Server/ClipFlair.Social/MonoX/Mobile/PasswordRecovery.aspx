<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Mobile.PasswordRecovery" 
    Codebehind="PasswordRecovery.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Register TagPrefix="MonoX" TagName="PasswordRecovery" Src="~/MonoX/ModuleGallery/Mobile/PasswordRecovery.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<div class="main input-form"> <!-- Main Start -->
    <MonoX:PasswordRecovery runat="server" ID="ctlPasswordRecovery"  />
</div>
</asp:Content>
