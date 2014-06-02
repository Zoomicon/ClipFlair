<%@ Page
    Language="C#"  
    AutoEventWireup="true"
    Theme="ClipFlair"
    CodeBehind="EventCalendar.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.EventCalendar"
    MasterPageFile="~/App_MasterPages/ClipFlair/Default.master" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="EventModule" Src="~/MonoX/ModuleGallery/EventModule/EventModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="Server">
    <div class="container-highlighter" style="background-color:#248b9a">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor01" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                    <p>Organize your lessons, homework and more...</p>
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
            <div class="span12">
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                    <ZoneTemplate>
                        <MonoX:EventModule ID="eventModule" runat="server" Mode="Advanced" CalendarName="MonoXCalendar" AdvancedModeDefaultView="MonthView">
                        </MonoX:EventModule>
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
        </div>
    </div>
</asp:Content>
