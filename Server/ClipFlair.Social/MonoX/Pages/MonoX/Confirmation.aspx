<%@ Page 
    Title=""
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master"
    AutoEventWireup="true"
    CodeBehind="Confirmation.aspx.cs" 
    Inherits="MonoSoftware.MonoX.Pages.Confirmation" 
    Theme="Default" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register TagPrefix="MonoX" TagName="Confirmation" Src="~/MonoX/ModuleGallery/Confirmation.ascx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cp" Runat="Server">
    <div class="main"> <!-- Main Start -->
            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="false">
                <ZoneTemplate>
                    <MonoX:Confirmation ID="ctlConfirmation" runat="server" />
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>            
    </div>    
</asp:Content>
