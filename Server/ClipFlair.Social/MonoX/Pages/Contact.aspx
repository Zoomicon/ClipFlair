﻿<%@ Page
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

<script language="C#" runat="server">
  private void ContactForm_BeforeSendMail(System.Net.Mail.MailMessage mailMessage, System.ComponentModel.CancelEventArgs e)
  {
    mailMessage.Body = "from: " + mailMessage.From + "\n\n" + mailMessage.Body;
    mailMessage.From = new System.Net.Mail.MailAddress("clipflair@cti.gr");
  }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">             
    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_ContentZone %>' ID="contentPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
        <ZoneTemplate>
            <MonoX:ContactForm ID="ctlContact" runat="server" 
            A_WebSiteName="ClipFlair" A_Address="N.Kazantzaki" A_City="Patras" 
            A_ZipCode="26504" A_Country="Greece" A_Phone="+302610960312" A_Fax="+302610960490"  EnableSSL="false" A_EMail="clipflair@cti.gr" 
            OnBeforeSendMail="ContactForm_BeforeSendMail"
		    />
        </ZoneTemplate>
    </portal:PortalWebPartZoneTableless>
</asp:Content>