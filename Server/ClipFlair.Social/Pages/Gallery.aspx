<%@ Page 
	Language="C#" 
	MasterPageFile="~/MonoX/MasterPages/Default.master" 
	AutoEventWireup="true" 
	Inherits="MonoSoftware.MonoX.Pages.Features" 
	Title="Media Gallery" 
	Theme="Default" 
    %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
 
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Repositories" TagPrefix="MonoRepositories" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">
    <table cellspacing="0" cellpadding="0">
      <tr>        
        <td class="left-section">
            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                <ZoneTemplate>
                    <MonoX:Editor runat="server" ID="editor2" Title='<%$ Code: PageResources.Title_DescriptionOfFeatures %>' ShowRating="false">
                    <DefaultContent>
                    <h1>Media Gallery</h1>
                    The following URLs can be used at <a href="http://clipflairsrv.cti.gr/Play" target="Playground">ClipFlair Playground</a>'s Media component (you flip it over and set the Media URL option there).
                    <br /><br />
                    <h2>Smooth Streaming Video URLs</h2>                    
                    <ul class="list">
                        <li>
                            a
					    </li>
                        <li>
                            b
					    </li>
                    </ul>
                    </DefaultContent>
                    </MonoX:Editor>
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>
        </td>
      </tr>      
    </table>

</asp:Content>

