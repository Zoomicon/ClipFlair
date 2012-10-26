<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="UserProfile.aspx.cs"
    Inherits="MonoSoftware.MonoX.Mobile.UserProfile"
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>  
<%@ Register TagPrefix="MonoX" TagName="UserProfile" Src="~/MonoX/ModuleGallery/Mobile/ProfileModule/UserProfileModule.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div data-role="fieldcontain">
        <MonoX:UserProfile id="ctlProfile" runat="server" IsPreviewMode="true" AutoDetectUser="false" HiddenFieldsString="FirstName,LastName" GravatarRenderType="NotSet" >
            <EditTemplate>                                
            </EditTemplate>
            <PreviewTemplate>
            </PreviewTemplate>
        </MonoX:UserProfile>                                                
    </div>
</asp:Content>