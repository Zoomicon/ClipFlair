<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HTML5VideoPlayer.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.HTML5VideoPlayer" %>
<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">
</asp:ScriptManagerProxy>
<div class="video-container video-js-box">
    <video class="video-js" controls="controls" poster="<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(PosterUrl) %>" title="<%= PosterTitle %>"
            width="<%= Width %>" height="<%= Height %>" 
            <%= AutoPlay ? "autoplay='autoplay'" : String.Empty %>
            <%= Loop ? "loop='loop'" : String.Empty %>
            <%= Muted ? "muted='muted'" : String.Empty %>
            preload="<%= Preload.ToString().ToLower() %>"
            >
            <asp:Repeater ID="rptVideos" runat="server">
                <ItemTemplate>
                    <source src="<%# MonoSoftware.Web.UrlFormatter.ResolveServerUrl(((MonoSoftware.MonoX.ModuleGallery.VideoArguments)Container.DataItem).VideoUrl) %>" type="<%# ((MonoSoftware.MonoX.ModuleGallery.VideoArguments)Container.DataItem).VideoMimeType %>" />
                </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder ID="panFallback" runat="server" Visible="<% $Code: !String.IsNullOrWhiteSpace(FallbackMovieUrl) %>">            
	        <object id="flash_fallback_1" class="vjs-flash-fallback" type="application/x-shockwave-flash" data="<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(PlayerUrl) %>" width="<%= Width %>" height="<%= Height %>">
		        <param name="movie" value="<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(PlayerUrl) %>" />
		        <param name="allowFullScreen" value="true" />
		        <param name="wmode" value="transparent" />
		        <param name="flashVars" value="config={'playlist':['<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(PosterUrl) %>',{'url':'<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(FallbackMovieUrl) %>','autoPlay':<%= AutoPlay.ToString().ToLower() %>}]}" />
		        <img alt="<%= PosterTitle %>" src="<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(PosterUrl) %>" width="<%= Width %>" height="<%= Height %>" title="No video playback capabilities, please download the video below" />
	        </object>
            </asp:PlaceHolder>
    </video>
</div>
