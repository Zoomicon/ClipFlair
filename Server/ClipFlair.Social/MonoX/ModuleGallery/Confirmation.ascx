<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="Confirmation.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Confirmation" %>

    <div class="container-highlighter" style="background-color:#f3f3f3">
        <div class="container text-center">
            <a href='<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/")) %>' class="logo">
                <img id="Img1" runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.logo_png %>" alt="MonoX" />
            </a>
        </div>
    </div>
    <div class="container">
        <div class="row-fluid">
            <div class="span12 clearfix text-center" style="padding:40px 0;">
                <h2><%= Title %></h2>
                <div><%= Description %></div>
            </div>
        </div>
    </div>
    <STYLE type="text/css">
        #main_wrapper {margin-bottom:10px;}
    </STYLE>

