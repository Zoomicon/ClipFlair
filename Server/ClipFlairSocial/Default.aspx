<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    MasterPageFile="~/MonoX/MasterPages/Default.master"
    Theme="Default"
    Inherits="MonoSoftware.MonoX.Pages.Default" 
    Title="MonoX - Portal Framework for ASP.NET" 
    Codebehind="Default.aspx.cs" %>
    
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
 
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Rss" Src="~/MonoX/ModuleGallery/RssReader.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.ModuleGallery" TagPrefix="ModuleGallery" %>
<%@ Register TagPrefix="MonoX" TagName="AdModule" Src="~/MonoX/ModuleGallery/AdModule.ascx"  %>
<%@ Register TagPrefix="MonoX" TagName="SlideShow" Src="~/MonoX/ModuleGallery/SlideShow.ascx"  %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">


    <table cellpadding="0" cellspacing="0" class="two-columns">
	    <tr>
    	    <td class="left-column">    	         	         	        	        
    	        <portal:PortalWebPartZoneTableless ID="leftWebPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>'>
                    <ZoneTemplate>
                        <MonoX:Editor runat="server" ID="editor1" Title='<%$ Code: PageResources.Title_LeftSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_LeftSection %>' >
                        <DefaultContent>
        	                
                        </DefaultContent>    
                        </MonoX:Editor>                
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>                             
            </td>
            <td class="right-column">
                <portal:PortalWebPartZoneTableless ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>'>
                <ZoneTemplate>
                    <MonoX:Editor runat="server" ID="editor2" Title='<%$ Code: PageResources.Title_RightSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_RightSection %>'>
                    <DefaultContent>
        	            
                    </DefaultContent>
                    </MonoX:Editor>
                </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>                        
            </td>
	    </tr>
    </table>
</div> 

  
</asp:Content>