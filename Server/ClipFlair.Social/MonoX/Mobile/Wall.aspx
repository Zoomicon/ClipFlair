<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Wall.aspx.cs"
    Inherits="MonoSoftware.MonoX.Mobile.Wall"
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>  
<%@ Register TagPrefix="MonoX" TagName="Wall" Src="~/MonoX/ModuleGallery/Mobile/WallNotes.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div>
        <MonoX:Wall AllowFileUpload="false" ShowPrivacyEditor="false" runat="server" ID="snWallNotes" UsePrettyPhoto="false" ShowRating="false" GravatarRenderType="NotSet" EnableLinkify="false" >
        </MonoX:Wall>   
    </div>
</asp:Content>