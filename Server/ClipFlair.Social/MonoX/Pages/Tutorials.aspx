<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="MonoSoftware.MonoX.BasePage"
    Theme="Default"
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master" %>
        
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
                            <MonoX:Editor runat="server" ID="editor01" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
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
                        <MonoX:Editor ID="ctlEditor2" runat="server" Title=""></MonoX:Editor>            
                    </ZoneTemplate>
                </portal:PortalWebPartZone>
            </div>
        </div>
    </div> 
    <div class="container">    
        <div class="row-fluid">
            <div class="span6 clearfix">
                <portal:PortalWebPartZone HeaderText="Content zone" ID="LeftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                    <ZoneTemplate>
                        <MonoX:Editor ID="ctlEditor3" runat="server" Title=""></MonoX:Editor>
                    </ZoneTemplate>
                </portal:PortalWebPartZone>
            </div>
            <div class="span6 clearfix" style="position: relative;">
                <portal:PortalWebPartZone HeaderText="Right part zone" ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" ShowChromeForNonAdmins="false">
                    <ZoneTemplate>
                        <MonoX:Editor ID="ctlEditor4" runat="server" Title=""></MonoX:Editor> 
                    </ZoneTemplate>
                </portal:PortalWebPartZone>
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