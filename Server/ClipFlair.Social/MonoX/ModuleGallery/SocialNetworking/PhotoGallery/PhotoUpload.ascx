<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="PhotoUpload.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.PhotoUpload" %>

<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>


<div class="photo-upload">
    <h2><asp:Literal ID="labAlbumName" runat="server"></asp:Literal></h2>
    <div class="main-options">
        <div class="inner small-btn">
            <asp:LinkButton ID="lnkBack" runat="server" CssClass="styled-button back-btn"></asp:LinkButton>
        </div>
    </div>
    <div class="photo-gallery">        
        <MonoX:SilverlightUpload width="300" runat="server" ID="ctlUpload" UploadVisibleOnInit="true" />
    </div>
</div>

