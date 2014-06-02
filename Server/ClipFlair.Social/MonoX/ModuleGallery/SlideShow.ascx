<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="SlideShow.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SlideShow" %>

<div id="slides_<%= this.ClientID %>" class="slides">    
    <asp:Repeater runat="server" ID="rptItems">
        <HeaderTemplate>       
          <div class="slides_container">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="slide">        
                <a href='<%# Eval("Url") %>' target="<%# Eval("Target") %>" title='<%# Eval("Title") %>'><%# FormatUrl(Eval("ImageUrl").ToString(), Eval("Title").ToString(), "class=\"scale-with-grid\"") %></a>
            </div>
        </ItemTemplate>
        <FooterTemplate>
          </div>        
        </FooterTemplate>
    </asp:Repeater>  
    <a href="#" class='<%= this.EnableTextualNavigation  ? "prev-text" : "prev-arrow" %>'><img runat="server" id="imgPrev" alt="Arrow Prev" /><!--<asp:Literal runat="server" ID="ltlPrev"></asp:Literal>--></a>
    <a href="#" class='<%= this.EnableTextualNavigation ? "next-text" : "next-arrow" %>'><img runat="server" id="imgNext" alt="Arrow Next" /><!--<asp:Literal runat="server" ID="ltlNext"></asp:Literal>--></a>
    <img id="imgShadow" alt="image-shadow" runat="server" class="scale-with-grid slider-shadow" />      
    <asp:PlaceHolder runat="server" ID="plhTitle">
        <div class="title"><%= this.SliderTitle %></div>
    </asp:PlaceHolder>
</div>
<script type="text/javascript">
        $(document).ready(function () {
                setupGallery_<%= this.ClientID %>();
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (s, e) {
                        setupGallery_<%= this.ClientID %>();
                    });
                }
            });
            function setupGallery_<%= this.ClientID %>() {
                $('#slides_<%= this.ClientID %>').slides({
                    preload: true,
                    generatePagination: <%= this.EnablePagination.ToString().ToLower() %>,
                    play: <%= this.SliderPlayInterval %>,
                    pause: <%= this.SliderPauseInterval %>,
                    hoverPause: true,
                    next: '<%= EnableTextualNavigation ? "next-text" : "next-arrow" %>',
                    prev: '<%= EnableTextualNavigation ? "prev-text" : "prev-arrow" %>'
                });
            };    
</script>