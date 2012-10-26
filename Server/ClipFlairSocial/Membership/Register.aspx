<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.Register" 
    Theme="Default" 
    Codebehind="Register.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>    
<%@ Register TagPrefix="MonoX" TagName="Membership" Src="~/MonoX/ModuleGallery/Membership/MembershipEditor.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<div class="main"> <!-- Main Start -->
    <MonoX:Membership runat="server" ID="ctlMembershipEditor" />
</div>
</asp:Content>
