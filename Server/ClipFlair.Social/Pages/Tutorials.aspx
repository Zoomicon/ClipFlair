<%@ Page 
	Language="C#" 
	MasterPageFile="~/MonoX/MasterPages/Default.master" 
	AutoEventWireup="true" 
	Inherits="MonoSoftware.MonoX.Pages.Features" 
	Title="Video Tutorials" 
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
		  <div>

            <cc2:CustomXml
             DocumentUrl="http://gallery.clipflair.net/video/tutorials.xml"
             XslUrl="http://gallery.clipflair.net/video/video_tutorials.xsl"
             runat="server"
             />
			 
		  </div>
        </td>
      </tr>      
    </table>

</asp:Content>

