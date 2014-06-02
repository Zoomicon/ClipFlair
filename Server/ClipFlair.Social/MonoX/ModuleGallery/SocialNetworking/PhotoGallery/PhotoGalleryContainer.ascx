<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="PhotoGalleryContainer.ascx.cs" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.PhotoGalleryContainer" %>
    
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/AlbumList.ascx" TagPrefix="MonoX" TagName="AlbumList" %>    
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/AlbumEditView.ascx" TagPrefix="MonoX" TagName="AlbumEditView" %>    
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/PhotoUpload.ascx" TagPrefix="MonoX" TagName="PhotoUpload" %>    
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/PhotoListView.ascx" TagPrefix="MonoX" TagName="PhotoListView" %>    
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/PhotoEditView.ascx" TagPrefix="MonoX" TagName="PhotoEditView" %>    

<script type="text/javascript">
    function NavigateBack() {
        history.go(-1);
    }
</script>

<div class="photos">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <MonoX:AlbumList ID="albumList" runat="server"></MonoX:AlbumList>
            <MonoX:AlbumEditView ID="albumEditView" runat="server"></MonoX:AlbumEditView>
            <div class="span12">
                <MonoX:PhotoUpload ID="photoUpload" runat="server"></MonoX:PhotoUpload>
            </div>
            <MonoX:PhotoListView ID="photoListView" runat="server"></MonoX:PhotoListView>
            <MonoX:PhotoListView ID="photoPreview" runat="server" IsPhotoPreview="true"></MonoX:PhotoListView>
            <MonoX:PhotoEditView ID="photoEditView" runat="server"></MonoX:PhotoEditView>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>