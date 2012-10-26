<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Login.aspx.cs"
    Inherits="MonoSoftware.MonoX.Mobile.Login"
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
    
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>  
<%@ Register TagPrefix="MonoX" TagName="Login" Src="~/MonoX/ModuleGallery/Mobile/LoginModule.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div data-role="fieldcontain">
    <MonoX:Login runat="server" ID="ctlLogin" Width="100%" />
    </div>
</asp:Content>