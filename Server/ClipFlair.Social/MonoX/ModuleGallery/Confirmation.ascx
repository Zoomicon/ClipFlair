<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="Confirmation.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Confirmation" %>

<div class="message-page">
    <h2><asp:Label runat="server" ID="lblTitle"></asp:Label></h2>
    <div>
        <asp:Label runat="server" ID="lblMessage"></asp:Label>
        <asp:PlaceHolder runat="server" ID="plhControls"></asp:PlaceHolder>
    </div>
</div>