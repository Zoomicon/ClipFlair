<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="MonoSoftware.MonoX.BasePage"
    Theme="ClipFlair"
    MasterPageFile="~/MonoX/MasterPages/ClipFlairSmallHeader.master"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">             
    <portal:PortalWebPartZone HeaderText="Content zone" ID="contentPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
        <ZoneTemplate>
            <MonoX:Editor ID="ctlEditor" runat="server" Title=""></MonoX:Editor>
        </ZoneTemplate>
    </portal:PortalWebPartZone>
</asp:Content>