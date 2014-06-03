<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/Default.master" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Samples.WebPartSample" Title="Web part sample" Codebehind="WebPartSample.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %> 
<%@ Register TagPrefix="MonoX" TagName="HelloWorld" Src="~/MonoX/Samples/WebPartSample/HelloWorld.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">
<table cellspacing="0" cellpadding="0">
    <tr>
        <td class="left-section" style="vertical-align:top;">
            <portal:PortalWebPartZoneTableless HeaderText="Left part zone" ID="leftWebPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm" ShowChromeForNonAdmins="true">
                <ZoneTemplate>
                    <MonoX:HelloWorld runat="server" ID="helloWorldSample" Title="Hello world" />
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>
        </td>
        <td class="right-section">
            <portal:PortalWebPartZoneTableless HeaderText="Right part zone" ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true" >
                <ZoneTemplate>
                    <asp:Panel runat="server" ID="pnlDescription">
                        <p><span class="header_blue">Web part sample</span></p>
                        <p>
                        This is a sample page demonstrating the development and the usage of a very simple Web part. The "Hello word" part on this page contains two custom properties and displays them to the user on a button click. 
                        <br /><br />
                        Feel free to modify this page during the learning process.
                        </p>
                    </asp:Panel>          
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>
        </td>
    </tr>
</table>
</asp:Content>

