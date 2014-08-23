<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="MonoSoftware.MonoX.BasePage"
    Theme="ClipFlair"
    MasterPageFile="~/MonoX/MasterPages/DefaultLocalized.master" %>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="container-highlighter" style="background-color:#38595b">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor01" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                    <p>ClipFlair Manual</p>
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
            <div class="span8">
                <portal:PortalWebPartZoneTableless HeaderText="Content zone" ID="contentPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                    <ZoneTemplate>
                        <MonoX:Editor ID="ctlEditor" runat="server" Title=""></MonoX:Editor>
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
            <div class="span4">
                <portal:PortalWebPartZoneTableless HeaderText="Right part zone" ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                    <ZoneTemplate>
                         
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
        </div>
    </div>
    <script type="text/javascript">   
        $(document).ready(function() {
            $("a.fancybox").fancybox();
            
            //sidebar with contents to stay on top 
            var $window = $(window),
                $stickyEl = $('#ctl00-ctl00-ctl01-ctl00-cp-cp-cp-cp-rightPartZone'),
                elTop = $stickyEl.offset().top;

            $window.scroll(function() {
                $stickyEl.toggleClass('sticky', $window.scrollTop() > elTop);
            });
            
            //add active class
            $('.contents-list a').click(function(e) {
                //e.preventDefault();
                $('.contents-list a').removeClass('active');
                $(this).addClass('active');
            });
            
            //SCROLL TO ANCHOR
           // scroll handler
          var scrollToAnchor = function( id ) {
         
            // grab the element to scroll to based on the name
            var elem = $("a[name='"+ id +"']");
         
            // if that didn't work, look for an element with our ID
            if ( typeof( elem.offset() ) === "undefined" ) {
              elem = $("#"+id);
            }
         
            // if the destination element exists
            if ( typeof( elem.offset() ) !== "undefined" ) {
         
              // do the scroll
              $('html, body').animate({
                      scrollTop: elem.offset().top
              }, 1000 );
         
            }
          };
         
          // bind to click event
          $("a").click(function( event ) {
         
            // only do this if it's an anchor link
            if ( $(this).attr("href").match("#") ) {
         
              // cancel default event propagation
              event.preventDefault();
         
              // scroll to the location
              var href = $(this).attr('href').replace('#', '')
              scrollToAnchor( href );
         
            }
         
          });
            
            
        });
    </script>
</asp:Content>