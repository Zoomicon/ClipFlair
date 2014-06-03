<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master"
    AutoEventWireup="true"     
    Inherits="MonoSoftware.MonoX.BasePage" 
    Theme="Default"
    Title="" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>  
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %> 
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    
    <div class="container-highlighter" style="background-color:#248b9a">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor02" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                    <p>ClipFlair <strong>Help</strong>: Browse our <strong>Tutorials</strong>, read the <strong>FAQ</strong>, or <strong>contact</strong> us</p>
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
                <portal:PortalWebPartZone HeaderText="Top part zone" ID="TopWebPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                    <ZoneTemplate>
                                    
                    </ZoneTemplate>
                </portal:PortalWebPartZone>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row-fluid">
            <div class="span6 clearfix">
                <portal:PortalWebPartZoneTableless ID="FirstLeftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>'>
                <ZoneTemplate>
                    
                </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>             
            </div>
            <div class="span6 clearfix" style="position: relative;">
                <portal:PortalWebPartZoneTableless ID="SecondRightPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>'>
                    <ZoneTemplate>
                        
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>  
            </div>               
        </div>
    </div>
    <div class="container">
        <div class="row-fluid">
            <div class="span6 clearfix">
                <portal:PortalWebPartZoneTableless ID="ThirdLeftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>'>
                <ZoneTemplate>
                    
                </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>             
            </div>
            <div class="span6 clearfix" style="position: relative;">
                <portal:PortalWebPartZoneTableless ID="ForthRightPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>'>
                    <ZoneTemplate>
                        
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>  
            </div>               
        </div>
    </div>
    <div class="container">
        <div class="row-fluid">
            <div class="span12">
                <portal:PortalWebPartZone HeaderText="Bottom part zone" ID="BottomWebPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                    <ZoneTemplate>
                                    
                    </ZoneTemplate>
                </portal:PortalWebPartZone>
            </div>
        </div>
    </div>
</asp:Content>