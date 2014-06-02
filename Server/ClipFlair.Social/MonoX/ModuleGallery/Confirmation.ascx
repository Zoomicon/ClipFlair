<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="Confirmation.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Confirmation" %>

<div class="top-copyright">
    Copyright &#169;2012
    <a href="http://www.mono-software.com">Mono Ltd.</a>
    <a id="A1" runat="server" href="http://monox.mono-software.com" title="Powered by MonoX">Powered by MonoX</a>
</div>
<div class="empty-top-section">
    <a href='<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/")) %>' class="logo">
        <img id="Img1" runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.logo_png %>" alt="MonoX" />
    </a>
</div>
<div class="container-fluid-mid">
    <div class="message-page">
        <h2><asp:Label runat="server" ID="lblTitle"></asp:Label></h2>
        <p>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
            <asp:PlaceHolder runat="server" ID="plhControls"></asp:PlaceHolder>
        </p>
    </div>
</div>