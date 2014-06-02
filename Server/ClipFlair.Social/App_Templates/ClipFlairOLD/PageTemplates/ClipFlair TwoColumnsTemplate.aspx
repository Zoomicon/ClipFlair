﻿<%@ Page
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
    <div class="left-column">        
        <portal:PortalWebPartZone HeaderText="Content zone" ID="contentPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
            <ZoneTemplate>
                 <MonoX:Editor ID="ctlEditor1" runat="server" Title=""></MonoX:Editor>
            </ZoneTemplate>
        </portal:PortalWebPartZone>
    </div>
    <div class="right-column">
        <portal:PortalWebPartZone HeaderText="Right part zone" ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
            <ZoneTemplate>
                  <MonoX:Editor ID="ctlEditor2" runat="server" Title=""></MonoX:Editor> 
            </ZoneTemplate>
        </portal:PortalWebPartZone>
    </div>
</asp:Content>