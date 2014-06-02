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
    <div class="row-fluid">
        <div class="span12 clearfix">
            <portal:PortalWebPartZone HeaderText="Content zone" ID="fullPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                <ZoneTemplate>
                    <MonoX:Editor ID="ctlEditor" runat="server" Title=""></MonoX:Editor>
                </ZoneTemplate>
            </portal:PortalWebPartZone>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span6 clearfix">
            <portal:PortalWebPartZone HeaderText="Content zone" ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                <ZoneTemplate>
                    <MonoX:Editor ID="ctlEditor" runat="server" Title=""></MonoX:Editor>
                </ZoneTemplate>
            </portal:PortalWebPartZone>
        </div>
        <div class="span6 clearfix"  style="position: relative;">
            <portal:PortalWebPartZone HeaderText="Right part zone" ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" ShowChromeForNonAdmins="true">
                <ZoneTemplate>
                     
                </ZoneTemplate>
            </portal:PortalWebPartZone>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span4 clearfix">
            <portal:PortalWebPartZone HeaderText="Content zone" ID="contentPartZone_1" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                <ZoneTemplate>
                    <MonoX:Editor ID="ctlEditor" runat="server" Title=""></MonoX:Editor>
                </ZoneTemplate>
            </portal:PortalWebPartZone>
        </div>
        <div class="span4 clearfix">
            <portal:PortalWebPartZone HeaderText="Right part zone" ID="contentPartZone_2" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" ShowChromeForNonAdmins="true">
                <ZoneTemplate>
                     
                </ZoneTemplate>
            </portal:PortalWebPartZone>
        </div>
        <div class="span4 clearfix">
            <portal:PortalWebPartZone HeaderText="Right part zone" ID="contentPartZone_3" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" ShowChromeForNonAdmins="true">
                <ZoneTemplate>
                     
                </ZoneTemplate>
            </portal:PortalWebPartZone>
        </div>
    </div>   
</asp:Content>