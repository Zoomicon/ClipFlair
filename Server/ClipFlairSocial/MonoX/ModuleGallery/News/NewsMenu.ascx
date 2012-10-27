<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.NewsMenu" Codebehind="NewsMenu.ascx.cs" %>

<%@ Register TagPrefix="radM" Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" %>

<asp:PlaceHolder ID="templateHolder" runat="server">
</asp:PlaceHolder>
<radM:radmenu id="radNewsMenu" runat="server"
    CausesValidation="false" 
    >
</radM:radmenu>
    