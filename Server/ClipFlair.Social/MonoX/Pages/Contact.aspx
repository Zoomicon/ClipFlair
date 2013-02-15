<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Contact.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.Contact"
    Theme="Default"
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="ContactForm" Src="~/MonoX/ModuleGallery/ContactFormModule.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">             
    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_ContentZone %>' ID="contentPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
        <ZoneTemplate>
            <MonoX:ContactForm ID="ctlContact" runat="server" 
            A_WebSiteName="ClipFlair" A_Address="N.Kazantzaki" A_City="Patras" 
			A_ZipCode="26504" A_Country="Greece" A_Phone="+302610960300" A_Fax="+302610960490"  EnableSSL="false" A_EMail="ClipFlair@cti.gr" 
            ></MonoX:ContactForm>
        </ZoneTemplate>
    </portal:PortalWebPartZoneTableless>
</asp:Content>