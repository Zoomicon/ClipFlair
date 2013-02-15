<%@ Page 
	Language="C#" 
	MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master" 
	AutoEventWireup="true" 
	Inherits="MonoSoftware.MonoX.Pages.AdditionalResources" 
	Title="MonoX Resources" 
	Theme="Default" 
	Codebehind="AdditionalResources.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="content1" ContentPlaceHolderID="cp" Runat="Server">
    <table cellspacing="0" cellpadding="0">
      <tr>
        <td class="left-section">
            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                <ZoneTemplate>
                    <MonoX:Editor runat="server" ID="editor2" Title='<%$ Code: PageResources.Title_AdditionalResources %>'>
                    <DefaultContent>
                    <h2>MonoX documentation and samples</h2>
                        <ul class="list">
                        <li><a href="http://www.mono-software.com/Downloads/#MonoX" target="_blank">MonoX user manual</a></li>
                        <li><a href="http://www.mono-software.com/Downloads/#MonoX" target="_blank">MonoX API documentation</a></li>
                        <li><a href="~/MonoX/Samples/ConnectionSample/ConnectionSample.aspx" target="_blank">Web part connection sample</a></li>
                        <li><a href="~/MonoX/Samples/WebPartSample/WebPartSample.aspx" target="_blank">Web part sample</a></li>
                    </ul>
                    <h2>Books</h2>
                    <ul class="list">
                        <li><a href="http://www.amazon.com/gp/product/193239477X?ie=UTF8&amp;tag=monosoft-20&amp;linkCode=as2&amp;camp=1789&amp;creative=9325&amp;creativeASIN=193239477X" target="_blank">ASP.NET 2.0 Web Parts in Action: Building Dynamic Web Portals</a> (by Darren Neimke)</li>
                        <li><a href="http://www.lulu.com/content/804882" target="_blank">The Web Part Infrastructure Uncovered</a> (by Teun Duynstee)</li>
                        <li><a href="http://www.amazon.com/gp/product/0764584642?ie=UTF8&amp;tag=monosoft-20&amp;linkCode=as2&amp;camp=1789&amp;creative=9325&amp;creativeASIN=0764584642" target="_blank">ASP.NET 2.0 Website Programming: Problem - Design - Solution</a> (by Marco Bellinaso)</li>
                        <li><a href="http://www.amazon.com/gp/product/076457860X?ie=UTF8&amp;tag=monosoft-20&amp;linkCode=as2&amp;camp=1789&amp;creative=9325&amp;creativeASIN=076457860X" target="_blank">Professional Web Parts and Custom Controls with ASP.NET 2.0</a> (by Peter Vogel)</li>
                    </ul>
                    <h2>Articles</h2>
                    <ul class="list">
                        <li><a href="http://msdn.microsoft.com/en-us/library/e0s9t4ck.aspx" target="_blank">ASP.NET Web Parts Controls</a></li>
                        <li><a href="http://www.code-magazine.com/Article.aspx?quickid=0611031" target="_blank">ASP.NET 2.0 Web Part Infrastructure</a></li>
                        <li><a href="http://msdn.microsoft.com/hr-hr/magazine/cc163587(en-us).aspx" target="_blank">Asynchronous Web Parts</a></li>
                        <li><a href="http://www.ondotnet.com/pub/a/dotnet/2005/05/23/webparts_1.html" target="_blank">Building Web Parts</a></li>
                        <li><a href="http://www.c-sharpcorner.com/UploadFile/a_anajwala/Building_WebParts.mht08042005042119AM/Building_WebParts.mht.aspx" target="_blank">Building Web Parts in ASP.NET 2.0</a></li>
                        <li><a href="http://msdn.microsoft.com/en-us/library/ms379628.aspx" target="_blank">Introducing the ASP.NET 2.0 Web Parts Framework</a></li>
                        <li><a href="http://msdn.microsoft.com/en-us/magazine/cc300767.aspx" target="_blank">Personalize Your Portal with User Controls and Custom Web Parts</a></li>
                    </ul>
                    <h2>Webcasts</h2>
                    <ul class="list">
                        <li><a href="http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?EventID=1032266502&amp;EventCategory=5&amp;culture=en-US&amp;CountryCode=US" target="_blank">MSDN Events Reloaded - Microsoft ASP.NET 2.0 Overview (Level 200)</a></li>
                        <li><a href="http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?EventID=1032290945&amp;EventCategory=5&amp;culture=en-US&amp;CountryCode=US" target="_blank">MSDN Webcast: ASP.NET Soup to Nuts: Web Part Controls (Level 200)</a></li>
                        <li><a href="http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?EventID=1032290794&amp;EventCategory=5&amp;culture=en-US&amp;CountryCode=US" target="_blank">MSDN Architecture Webcast: Introduction to ASP.NET Web Part Framework (Level 200)</a></li>
                        <li><a href="http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?EventID=1032291436&amp;EventCategory=5&amp;culture=en-US&amp;CountryCode=US" target="_blank">MSDN Webcast: Enabling Customizable User Interfaces in ASP.NET 2.0 Web Applications (Level 200)</a></li>
                        <li><a href="http://msevents.microsoft.com/cui/WebCastEventDetails.aspx?culture=en-US&amp;EventID=1032276877&amp;CountryCode=US" target="_blank">MSDN Webcast: Applying ASP.NET Web Parts: Building an RSS Aggregation Web Site (Level 200)</a></li>
                        <li><a href="http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?EventID=1032326987&amp;EventCategory=5&amp;culture=en-US&amp;CountryCode=US" target="_blank">MSDN Webcast: Introduction to ASP.NET 2.0 Web Part Framework (Level 300)</a></li>
                    </ul>
                    <h2>Blogs</h2>
                    <ul class="list">
                        <li><a href="http://weblogs.asp.net/davidbarkol/default.aspx" target="_blank">David Barkol</a></li>
                        <li><a href="http://www.teuntostring.net/blog/index.html" target="_blank">Teun Duynstee</a></li>
                        <li><a href="http://blogs.msdn.com/mharder/default.aspx" target="_blank">Mike Harder</a></li>
                        <li><a href="http://weblogs.asp.net/scottgu/default.aspx" target="_blank">Scott Guthrie</a></li>
                        <li><a href="http://markitup.com/Default.aspx" target="_blank">Darren Neimke</a></li>
                    </ul>
                    </DefaultContent>
                    </MonoX:Editor>
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>
        </td>
        <td class="right-section">
            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                <ZoneTemplate>
                    <MonoX:Editor runat="server" ID="editor1" Title='<%$ Code: PageResources.Title_Resources %>'>
                    <DefaultContent>
                    MonoX is built upon standard ASP.NET tools and techniques, so there are many additional resources that will help you learn how to use it efficiently in the shortest amount of time. <br /><br />
                    This page presents MonoX documentation and additional resources on ASP.NET and Web part programming. Please do not hesitate to contact us if you need more information on these topics.
                    </DefaultContent>
                    </MonoX:Editor>
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>            
        </td>
        
      </tr>
    </table>
</asp:Content>

