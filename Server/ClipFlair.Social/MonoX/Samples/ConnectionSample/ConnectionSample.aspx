<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Samples.ConnectionSample" Title="Connection sample" Codebehind="ConnectionSample.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %> 
<%@ Register TagPrefix="MonoX" TagName="RssProvider" Src="~/MonoX/Samples/ConnectionSample/RssUrlProvider.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="RssConsumer" Src="~/MonoX/Samples/ConnectionSample/RssConsumer.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="RelationshipProvider" Src="~/MonoX/Samples/ConnectionSample/TestRelationshipProvider.ascx" %>
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/Comments.ascx" TagPrefix="MonoX" TagName="Comments" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
<table cellspacing="0" cellpadding="0">
    <tr>
        <td class="left-section" style="vertical-align:top;">
            <portal:PortalWebPartZoneTableless HeaderText="Left part zone" ID="leftWebPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm" ShowChromeForNonAdmins="true">
                <ZoneTemplate>
                    <MonoX:RssProvider runat="server" ID="rssProvider" Title="RSS provider" />
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>            
        </td>
        <td class="right-section">
            <portal:PortalWebPartZoneTableless HeaderText="Right part zone" ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                <ZoneTemplate>
                    <asp:Label runat="server" ID="lblCaption">
                    <p><span class="header_blue">Web part connection sample</span></p>
                    <p>This page demonstrates the Web part connection techniques. Please refer to the section "Using the administrator's toolbar" / "Page display modes" / "Connect mode" in the User Manual for more details.</asp:Label></p>
                    <MonoX:RssConsumer runat="server" ID="rssConsumer" Title="RSS consumer" />
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>
        </td>
    </tr>
</table>

</asp:Content>
