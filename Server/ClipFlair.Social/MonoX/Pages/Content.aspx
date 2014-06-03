<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Content.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.Content"    
    MasterPageFile="~/MonoX/MasterPages/Default.master" %>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="content-page">
                <h2><asp:Literal runat="server" ID="ltlTitle"></asp:Literal></h2>
                <portal:PortalWebPartZone HeaderText='<%$ Code: PageResources.Zone_ContentZone %>' ID="contentPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                    <ZoneTemplate>
                        <MonoX:Editor ID="ctlEditor" runat="server" Title='<%$ Code: PageResources.Zone_ContentZone %>' ShowRating="false"></MonoX:Editor>
                    </ZoneTemplate>
                </portal:PortalWebPartZone>
            </div>
        </div>
    </div>
</asp:Content>