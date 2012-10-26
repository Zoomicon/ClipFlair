<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXMenu" Codebehind="MonoXMenu.ascx.cs" %>

<%@ Register TagPrefix="radM" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>
<asp:PlaceHolder ID="templateHolder" runat="server">
</asp:PlaceHolder>
<radM:radmenu id="radMonoXMenu" runat="server" CausesValidation="false">
</radM:radmenu>
    