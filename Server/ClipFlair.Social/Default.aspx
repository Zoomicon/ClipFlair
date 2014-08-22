<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    MasterPageFile="~/MonoX/MasterPages/DefaultLocalized.master"
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
        <div class="container-highlighter" style="background-color:#38595b">
            <div class="container">
                <div class="row-fluid clearfix">
                    <div class="span12">
                        <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                            <ZoneTemplate>
                                <MonoX:Editor runat="server" ID="editor01" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                    <DefaultContent>
                                        <p style="font-size:27px;">ClipFlair is a web platform for foreign language learning through revoicing & captioning of clips</p>
                                    </DefaultContent>
                                </MonoX:Editor>
                            </ZoneTemplate>
                        </portal:PortalWebPartZoneTableless>
                    </div>
                </div>
            </div>              
        </div>
        <div class="container">
            <div class="row-fluid">
                <div class="span12 clearfix">
                    <portal:PortalWebPartZoneTableless ID="ForthLeftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>'>
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor02" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                
                                </DefaultContent>
                            </MonoX:Editor>
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>     
                </div>
            </div>
        </div>  
        <div class="container">
            <div class="row-fluid">
                <div class="span6 clearfix">
                    <portal:PortalWebPartZoneTableless ID="SecondLeftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>'>
                    <ZoneTemplate>
                        <MonoX:Editor runat="server" ID="editor09" Title='<%$ Code: PageResources.Title_MiddleSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_MiddleSection %>'>
                            <DefaultContent>
                                               
                            </DefaultContent>
                        </MonoX:Editor>
                    </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>             
                </div>
                <div class="span6 clearfix" style="position: relative;">
                    <portal:PortalWebPartZoneTableless ID="ForthRightPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>'>
                        <ZoneTemplate>
                            <MonoX:SlideShow runat="server" ID="ctlSlideShow" Title='<%$ Code: PageResources.Title_RightSection %>'
                                NavigationNextCaption="<%$ Code: MonoSoftware.MonoX.Resources.DefaultResources.SlideShow_Next.ToUpper() %>" 
                                NavigationPreviousCaption="<%$ Code: MonoSoftware.MonoX.Resources.DefaultResources.SlideShow_Prev.ToUpper() %>"
                                SliderTitle="<%$ Code: MonoSoftware.MonoX.Resources.DefaultResources.SlideShow_Title.ToUpper() %>">
                                <SlideShowItems>                                    
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/HablarPorTelefono-RevCap-B1-ES-RO.clipflair.jpg" Url="http://studio.clipflair.net/?activity=HablarPorTelefono-RevCap-B1-ES-RO.clipflair" Title="HablarPorTelefono-RevCap-B1-ES-RO"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/Chinese-Cap-B2-ZH.jpg" Url="http://studio.clipflair.net/?activity=zh_Chinese-recipe1.clipflair" Title="苹果鸡翅：填空-Cap-B1-ZH"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/Lucas_y_sus_Hermanos_sin_hacer.clipflair.jpg" Url="http://studio.clipflair.net/?activity=Lucas_y_sus_Hermanos_sin_hacer.clipflair" Title="Lucas y sus hermanos"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/UBB_RO_AC_activitatea9.clipflair.jpg" Url="http://studio.clipflair.net/?activity=UBB_RO_AC_activitatea9.clipflair" Title="UBB_RO_AC_activitatea9"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/PolishDances_SubtitleTheClip_Polish.clipflair.jpg" Url="http://studio.clipflair.net/?activity=Polish-dances-Cap-B1-B2-PL.clipflair" Title="Polish-dances-Cap-B1-B2-PL"></ModuleGallery:SlideShowItem>                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/Retro.clipflair.jpg" Url="http://studio.clipflair.net/?activity=Retro.clipflair" Title="Retro"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/Unitat2_catala_LaTrucada_simplificada.clipflair.clipflair.jpg" Url="http://studio.clipflair.net/?activity=Unitat2_catala_LaTrucada_simplificada.clipflair" Title="Programul zilei: dimineaţa"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/FriendsBabyShower-Cap-C1-EN.jpg" Url="http://studio.clipflair.net/?activity=FriendsBabyShower-Cap-C1-EN.clipflair" Title="FriendsBabyShower-Cap-C1-EN"></ModuleGallery:SlideShowItem>
                                </SlideShowItems>
                            </MonoX:SlideShow>
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>  
                </div>               
            </div>
        </div>    
        <div class="container">
            <div class="row-fluid">
                <div class="span4">
                    <portal:PortalWebPartZoneTableless ID="colLeft" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="Col-leftZone">
                    <ZoneTemplate>
                        <MonoX:Editor runat="server" ID="editor05" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                            <DefaultContent>
                                
                            </DefaultContent>
                        </MonoX:Editor>
                    </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
                <div class="span4">
                    <portal:PortalWebPartZoneTableless ID="colMiddle" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="Col-middleZone">
                    <ZoneTemplate>
                        <MonoX:Editor runat="server" ID="editor06" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                            <DefaultContent>
                                
                            </DefaultContent>
                        </MonoX:Editor>
                    </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
                <div class="span4" style="position: relative;">
                    <portal:PortalWebPartZoneTableless ID="colRight" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="Col-rightZone">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor07" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                
                                </DefaultContent>
                            </MonoX:Editor>
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
            </div>
        </div>
</asp:Content>