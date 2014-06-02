<%@ Page 
    Title=""
    Language="C#"
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
    AutoEventWireup="true"
    CodeBehind="AboutMe.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.Profile.AboutMe"
    %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="PeopleSearch" Src="~/MonoX/ModuleGallery/SocialNetworking/PeopleSearch.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FriendList" Src="~/MonoX/ModuleGallery/SocialNetworking/FriendList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="SocialDisconnect" Src="~/MonoX/ModuleGallery/SocialDisconnect.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="EditProfile" Src="~/MonoX/ModuleGallery/ProfileModule/UserProfileModule.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register TagPrefix="MonoX" TagName="ProfileHeader" Src="~/MonoX/ModuleGallery/ProfileModule/UserProfileHeader.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="GoogleMaps" Src="~/MonoX/ModuleGallery/GoogleMaps.ascx" %>

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
            <div class="about-me user-profile">
                <div class="span12">
                    <MonoX:ProfileHeader id="profileHeader" runat="server"></MonoX:ProfileHeader> 
                </div>
                <div class="row-fluid">
                    <div class="span7">
                        <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                                <ZoneTemplate>                                                
                                <MonoX:EditProfile id="ctlProfile" runat="server" AutoDetectUser="false" HiddenFieldsString="FirstName,LastName" GravatarRenderType="NotSet" 
                                ShowMyStatus="false" ShowUserAvatar="false" ShowSideBarContent="false" >
                                    <EditTemplate>                                
                                    </EditTemplate>
                                    <PreviewTemplate>
                                    </PreviewTemplate>
                                </MonoX:EditProfile>                                                                        
                            </ZoneTemplate>
                        </portal:PortalWebPartZoneTableless>
                    </div>
                    <div class="span5">
                        <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                            <ZoneTemplate>
                                <MonoX:PeopleSearch runat="server" ID="snPeopleSearch" ConfirmationRequired="true" />
                                <MonoX:GoogleMaps id="googleMaps" runat="server" WidthSizeUnitType="Percentage" Width="100"></MonoX:GoogleMaps>
                                <MonoX:SocialDisconnect id="socialDisconnect" runat="server"></MonoX:SocialDisconnect>
                                <MonoX:FriendList runat="server" PageSize="9" AvatarSize="40" ID="snFriendList" Template="MyFriendList"></MonoX:FriendList>
                            </ZoneTemplate>
                        </portal:PortalWebPartZoneTableless>
                    </div>
                </div>
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
