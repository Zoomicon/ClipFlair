﻿<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="News.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.News"
    Theme="Default"
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="News" Src="~/MonoX/ModuleGallery/News/MainNewsModule.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">         
    <h2><asp:Literal runat="server" ID="ltlTitle"></asp:Literal></h2>
    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_ContentZone %>' ID="contentPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
        <ZoneTemplate>
            <MonoX:News ID="ctlNews" runat="server" ShowRating="true" ReadMorePageUrl="~/MonoX/Pages/NewsDetails.aspx" PagerPageSize="4"></MonoX:News>
        </ZoneTemplate>
    </portal:PortalWebPartZoneTableless>
</asp:Content>