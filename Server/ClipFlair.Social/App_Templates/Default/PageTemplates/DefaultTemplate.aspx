<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true"     
    Inherits="MonoSoftware.MonoX.BasePage" 
    Theme="Default"
    Title="" 
    %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="main">
        <table cellpadding="0" cellspacing="0">
	        <tr>
    	        <td class="left-column">    	        
                <portal:PortalWebPartZone HeaderText="Left part zone" ID="leftWebPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm" ShowChromeForNonAdmins="true">
                    <ZoneTemplate>
                        <MonoX:Editor runat="server" ID="editor1" />             
                    </ZoneTemplate>
                </portal:PortalWebPartZone>            
                </td>
                <td class="right-column">                              
                    <portal:PortalWebPartZone HeaderText="Right part zone" ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor2" />  
                        </ZoneTemplate>
                    </portal:PortalWebPartZone>
                </td>
	        </tr>
        </table>
    </div>
</asp:Content>