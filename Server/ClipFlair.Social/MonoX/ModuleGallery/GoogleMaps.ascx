<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="GoogleMaps.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.GoogleMaps" %>

<%@ Register Assembly="Artem.Google" Namespace="Artem.Google.UI" TagPrefix="artem" %>

<div class="google-maps">
    <artem:GoogleMap ID="ctlGoogleMap" runat="server">
    </artem:GoogleMap>
</div>