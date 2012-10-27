<%@ Page Title="" 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    CodeBehind="OpenSocialAuthorizationPage.aspx.cs" 
    Inherits="MonoSoftware.MonoX.OpenSocial.OpenSocialAuthorizationPage"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">

<%= String.Format(MonoSoftware.MonoX.Resources.DefaultResources.OpenSocial_RequestMessage, CurrentToken.OaConsumer.Name) %>

<asp:Button ID="btnAllow" runat="server" Text="Allow" />
<asp:Button ID="btnDeny" runat="server" Text="Deny" />

</asp:Content>
