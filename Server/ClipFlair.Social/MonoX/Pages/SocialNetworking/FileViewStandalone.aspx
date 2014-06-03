<%@ Page
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="FileViewStandalone.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.FileView"
    MasterPageFile="~/MonoX/MasterPages/Default.master"
    MaintainScrollPositionOnPostback="true"
    %>  

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Register TagPrefix="MonoX" TagName="FileView" Src="~/MonoX/ModuleGallery/SocialNetworking/FileView.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">    
    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
        <ZoneTemplate>
            <MonoX:FileView runat="server" ID="ctlFileView" />
        </ZoneTemplate>
    </portal:PortalWebPartZoneTableless>
</asp:Content>
