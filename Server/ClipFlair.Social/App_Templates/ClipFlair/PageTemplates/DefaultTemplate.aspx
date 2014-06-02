<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
    AutoEventWireup="true"     
    Inherits="MonoSoftware.MonoX.BasePage" 
    Theme="Default"
    Title="" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <portal:PortalWebPartZone HeaderText="Left part zone" ID="leftWebPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                <ZoneTemplate>
                    <MonoX:Editor runat="server" ID="editor1" />             
                </ZoneTemplate>
            </portal:PortalWebPartZone>
        </div>
    </div>
</asp:Content>