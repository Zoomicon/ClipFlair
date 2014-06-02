<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="GroupContainer.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupContainer" %>

<%@ Register TagPrefix="MonoX" TagName="GroupList" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="GroupEdit" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupEdit.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="GroupView" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupView.ascx" %>

<div class="groups">
    <MonoX:GroupEdit ID="groupEdit" runat="server" />
    <MonoX:GroupList ID="groupList" runat="server" />
    <MonoX:GroupView ID="groupView" runat="server" />
</div>