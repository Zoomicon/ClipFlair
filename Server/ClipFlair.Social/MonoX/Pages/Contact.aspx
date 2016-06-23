<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Contact.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.Contact"    
    Theme="ClipFlair"
    MasterPageFile="~/MonoX/MasterPages/Default.master" %>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="ContactForm" Src="~/MonoX/ModuleGallery/ContactFormModule.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<script language="C#" runat="server">  
  private void ContactForm_BeforeSendMail(System.Net.Mail.MailMessage mailMessage, System.ComponentModel.CancelEventArgs e)
  {
    mailMessage.IsBodyHtml = true;
    mailMessage.Body = "From: " + mailMessage.From.DisplayName + " (" +  mailMessage.From.Address + ")<br /><br />" + mailMessage.Body.Replace("\n", "<br />");
    mailMessage.From = new System.Net.Mail.MailAddress("ClipFlair <ClipFlair@gmail.com>");
  }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="container-highlighter" style="background-color:#338061">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor02" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                    <p>We will be happy to hear from you!</p>
                                </DefaultContent>
                            </MonoX:Editor>
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
            </div>
        </div>              
    </div>
    <div class="container">
        <div class="row-fluid">
            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_ContentZone %>' ID="contentPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                <ZoneTemplate>
                    <MonoX:ContactForm ID="ctlContact" runat="server" 
                      A_WebSiteName="ClipFlair.net"
                      A_Address="N.Kazantzaki"
                      A_City="Patras" 
                      A_ZipCode="26504"
                      A_Country="Greece"
                      A_Phone="+302610960300"
                      A_Fax="+302610960490"
                      EnableSSL="false"
                      A_EMail="ClipFlair@gmail.com"
                      OnBeforeSendMail="ContactForm_BeforeSendMail"                         
                    />
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>
        </div>
    </div>
</asp:Content>