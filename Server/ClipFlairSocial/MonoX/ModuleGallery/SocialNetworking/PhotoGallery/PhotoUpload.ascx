<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotoUpload.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.PhotoUpload" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<div>
    <div class="photo-nav">
        <asp:LinkButton ID="lnkBack" runat="server" CssClass="top-links"></asp:LinkButton>
    </div>
    <div class="photo-gallery" style="padding: 10px;">
        <h3><asp:Literal ID="labAlbumName" runat="server"></asp:Literal></h3>
        <MonoX:SilverlightUpload runat="server" ID="ctlUpload" UploadVisibleOnInit="true" />
    </div>
</div>
