<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master" 
    AutoEventWireup="true" 
    CodeBehind="PhotoGallery.aspx.cs" 
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.PhotoGallery" 
    MaintainScrollPositionOnPostback="true"
    Theme="Default"      
    %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="NewAlbumsList" Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/NewAlbumsList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="PhotoGalleryContainer" Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/PhotoGalleryContainer.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="left-section">
                <MonoX:NewAlbumsList runat="server" ID="snPhotoGalleryNewAlbums" IsPagerVisible="false" ShowOnlyAlbumsWithCover="false" SortBy="SortByLastActivityDate" CacheDuration="0" PageSize="8" PagingEnabled="false" />

                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                    <ZoneTemplate>                        
                        <MonoX:PhotoGalleryContainer runat="server" ID="snPhotoGallery" UsePrettyPhoto="true" GravatarRenderType="NotSet" />
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </td>
            <td class="right-section">
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                    <ZoneTemplate>
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>            
            </td>
        </tr>
    </table>
</asp:Content>
