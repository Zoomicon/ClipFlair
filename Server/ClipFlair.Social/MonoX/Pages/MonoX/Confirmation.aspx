<%@ Page 
    Title=""
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Empty.master" 
    AutoEventWireup="true"
    CodeBehind="Confirmation.aspx.cs" 
    Inherits="MonoSoftware.MonoX.Pages.Confirmation"  %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register TagPrefix="MonoX" TagName="Confirmation" Src="~/MonoX/ModuleGallery/Confirmation.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <MonoX:Confirmation ID="ctlConfirmation" runat="server" />
</asp:Content>
