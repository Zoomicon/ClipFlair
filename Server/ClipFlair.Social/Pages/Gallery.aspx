<%@ Page 
	Language="C#" 
	MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master" 
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

 <%@ Register Assembly="CustomXml" Namespace="PAB.WebControls" TagPrefix="cc2" %> 

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">
    <table cellspacing="0" cellpadding="0">
      <tr>        
        <td class="left-section">

            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                <ZoneTemplate>
                    <MonoX:Editor runat="server" ID="editor2" Title='<%$ Code: PageResources.Title_DescriptionOfFeatures %>' ShowRating="false">
                    <DefaultContent>
                    </DefaultContent>
                    </MonoX:Editor>
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>
        </td>
      </tr>      
    </table>

</asp:Content>

