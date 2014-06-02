<%@ Page Title="" 
    Language="C#"
    MasterPageFile="~/MonoX/MasterPages/Default.master"
    AutoEventWireup="true"
    CodeBehind="InMail.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.InMail" %>
    
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="MessageCenter" Src="~/MonoX/ModuleGallery/SocialNetworking/InMailMessaging/MessageCenter.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">

    <div class="container-highlighter" style="background-color:#008f87">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor01" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                    <p>My messages</p>
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
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                    <ZoneTemplate>
                        <MonoX:MessageCenter runat="server" ID="messageList" UsePrettyPhoto="true" />
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
        </div>
    </div>
</asp:Content>
