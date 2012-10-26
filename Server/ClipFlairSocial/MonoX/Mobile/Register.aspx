<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Register.aspx.cs"
    Inherits="MonoSoftware.MonoX.Mobile.Register"
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
%>
<%@ Register TagPrefix="MonoX" TagName="Membership" Src="~/MonoX/ModuleGallery/Mobile/MembershipEditor.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
<div class="main"> <!-- Main Start -->
    <MonoX:Membership runat="server" ID="ctlMembershipEditor" />
</div>
</asp:Content>
