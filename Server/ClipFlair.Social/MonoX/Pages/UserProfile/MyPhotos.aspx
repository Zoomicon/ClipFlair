<%@ Page 
    Title=""
    Language="C#"
    MasterPageFile="~/MonoX/MasterPages/Default.master"
    AutoEventWireup="true"
    CodeBehind="MyPhotos.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.Profile.MyPhotos"
    %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register TagPrefix="MonoX" TagName="ProfileHeader" Src="~/MonoX/ModuleGallery/ProfileModule/UserProfileHeader.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="PhotoGalleryContainer" Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/PhotoGalleryContainer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">  
    <div class="container-highlighter" style="background-color:#264d4d">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">  
                    <h2 id="profile_name-place"></h2>
                </div>
            </div>
        </div>              
    </div>
    <div class="container">
        <div class="row-fluid">
            <div class="photos user-profile">
                <div class="span12">
                    <MonoX:ProfileHeader id="profileHeader" runat="server"></MonoX:ProfileHeader> 
                </div>
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" >
                    <ZoneTemplate>    
                        <MonoX:PhotoGalleryContainer runat="server" ID="snPhotoGallery" UsePrettyPhoto="true" PageSize="7" GravatarRenderType="NotSet" AlbumsButtonVisible="false" MyAlbumsButtonVisible="false" />
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //get profile name from div and display it elsewhere
        var profile_name = document.getElementById('profile_name');
        var profile_name_new = document.getElementById('profile_name-place');
        profile_name_new.innerHTML = profile_name.innerHTML;
        profile_name.innerHTML = "";
    </script>
</asp:Content>
