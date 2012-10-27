<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="ActivationEmailRecovery.aspx.cs"
    Inherits="MonoSoftware.MonoX.Mobile.ActivationEmailRecovery"
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
%>
<%@ Register TagPrefix="MonoX" TagName="ActivationEmailRecovery" Src="~/MonoX/ModuleGallery/Mobile/ActivationEmailRecovery.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<div class="main input-form"> <!-- Main Start -->
    <MonoX:ActivationEmailRecovery runat="server" ID="ctlActivationEmailRecovery" />
</div>
</asp:Content>

