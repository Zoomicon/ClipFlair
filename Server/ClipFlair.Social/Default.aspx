<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    MasterPageFile="~/MonoX/MasterPages/Default.master"
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
                    <portal:PortalWebPartZoneTableless CssClass="margin-top-40" ID="ForthLeftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>'>
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
                    <portal:PortalWebPartZoneTableless ID="ForthRightPartZone" CssClass="activities-slideshow" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>'>
                        <ZoneTemplate>
                            <MonoX:SlideShow runat="server" ID="ctlSlideShow" Title='<%$ Code: PageResources.Title_RightSection %>'
                                NavigationNextCaption="<%$ Code: MonoSoftware.MonoX.Resources.DefaultResources.SlideShow_Next.ToUpper() %>" 
                                NavigationPreviousCaption="<%$ Code: MonoSoftware.MonoX.Resources.DefaultResources.SlideShow_Prev.ToUpper() %>"
                                SliderTitle="<%$ Code: MonoSoftware.MonoX.Resources.DefaultResources.SlideShow_Title.ToUpper() %>">
                                <SlideShowItems>                                    
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/Tutorial.jpg" Url="http://studio.clipflair.net/?activity=Tutorial.clipflair" Title="Tutorial - Watch Big Buck Bunny and his adventures and create your own narration. Feel free to use your imagination!"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/Waterloo-Cap-C1-ANY.jpg" Url="http://studio.clipflair.net/?activity=1Waterloo-Cap-C1-ANY.clipflair" Title="Waterloo - Learning objectives: identify stereotypes the English have about the French and vice versa. Translate a scene containing a geographical dialect and cultural references"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/Garibaldi_SUB.jpg" Url="http://studio.clipflair.net/?activity=Garibaldi_SUB.clipflair" Title="Garibaldi SUB - Increasing the pragmalinguistic competenceof Italian learners of English (complaints and requests)"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/LaJourneeDeMaggie-01-Cap-A1-A2-FR.jpg" Url="http://studio.clipflair.net/?activity=LaJourneeDeMaggie-01-Cap-A1-A2-FR.clipflair" Title="La journée de Maggie - la sorcière - Learning to talk about daily routine, use of present tense, saying aat what time something happens"></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/VisitBritain-CapRev-C1-EN.jpg" Url="http://studio.clipflair.net/?activity=VisitBritain-CapRev-C1-EN.clipflair" Title="Visit Britain. Tourist ad for the blind - A tourist ad (without dialogue) meant for students to record an audiodescription track so as to improve speaking skills."></ModuleGallery:SlideShowItem>                                   
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/Edinburgh.jpg" Url="http://studio.clipflair.net/?activity=Edinburgh.clipflair" Title="Edinburgh (tourist ad) for the blind - A tourist ad (without dialogue) meant for students to record an audiodescription track so as to improve speaking skills."></ModuleGallery:SlideShowItem>
                                    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/Yalom-Cap-B2-EN.jpg" Url="http://studio.clipflair.net/?activity=Yalom-Cap-B2-EN.clipflair" Title="Yalom‘s Cure - Let‘s create captions for our hard-of-hearing friends who would like to watch this documentary trailer about Irvin Yalom, the world-famous psychiatrist and writer..."></ModuleGallery:SlideShowItem>
				    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/FriendsBabyShower-Cap-C1-EN.jpg" Url="http://studio.clipflair.net/?activity=FriendsBabyShower-Cap-C1-EN.clipflair" Title="Friends - Baby Shower episode for the HoH - This is an activity to practice AV listening and AV writing. Students are asked to simulate the work of a subtitler for the deaf and hard-of-hearing. In order to do that, first they need to listen carefully to the dialogues and transcribe them..."></ModuleGallery:SlideShowItem>
				    <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/ClipFlair/img/Projects/LiedWunder-Rev-A2_DE.jpg" Url="http://studio.clipflair.net/?activity=LiedWunder-Rev-A2_DE.clipflair" Title="Wunder - Σβήστε λέξεις και δημιουργήστε κενά που θα συμπληρώσουν οι μαθητές. Χαμηλώστε τη φωνή και αφήστε τους μαθητές να διαβάσουν τους υποτίτλους και να ηχογραφήσουν τη φωνή τους. Παίξτε καραόκε!"></ModuleGallery:SlideShowItem>
                                </SlideShowItems>
                            </MonoX:SlideShow>
			    
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
		    <p><a class="iframe" style="font-size: 18px; font-weight: 600; padding-top: 10px;" data-fancybox-type="iframe" href="https://docs.google.com/forms/d/1oqBPyNDwVBEK33MwmSeNXEiBthjG5UdYHwJLFTUphmo/viewform?embedded=true">Click here to tell us what you think about ClipFlair!</a></p>		    
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