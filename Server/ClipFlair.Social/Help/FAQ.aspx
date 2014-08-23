<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/DefaultPartiallyLocalized.master"
    AutoEventWireup="true"     
    Inherits="MonoSoftware.MonoX.BasePage" 
    Theme="ClipFlair"
    Title="" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
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
                <portal:PortalWebPartZone HeaderText="Left part zone" ID="leftWebPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                    <ZoneTemplate>
                        <MonoX:Editor runat="server" ID="editor1" />             
                    </ZoneTemplate>
                </portal:PortalWebPartZone>
            </div>
        </div>
    </div>
</asp:Content>