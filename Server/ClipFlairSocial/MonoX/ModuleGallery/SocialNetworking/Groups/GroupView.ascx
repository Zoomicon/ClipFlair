<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupView.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupView" %>
<%@ Register TagPrefix="MonoX" TagName="Wall" Src="~/MonoX/ModuleGallery/SocialNetworking/WallNotes.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Photos" Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/PhotoGalleryContainer.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Discussion" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionContainer.ascx" %>
<%@ Import Namespace="MonoSoftware.MonoX.Utilities" %>
<asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" class="user-profile" width="100%">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <h3><asp:Label ID="lblTitle" runat="server"></asp:Label></h3> 
                                <div class="tabs">
                                    <ul class="tab" id="rowSwitch" runat="server">
                                        <li class="<%= InternalWorkingMode.Equals(SnGroupWorkingMode.Wall) ? "current" : String.Empty %>">
                                            <asp:HyperLink ID="lnkWall" runat="server" CausesValidation="false">
                                                <span id="labWall" runat="server">Wall</span></asp:HyperLink></li>
                                        <li class="<%= InternalWorkingMode.Equals(SnGroupWorkingMode.Photos) ? "current" : String.Empty %>">
                                            <asp:HyperLink ID="lnkPhotos" runat="server" CausesValidation="false">
                                                <span id="labPhotos" runat="server">Photos</span></asp:HyperLink></li>
                                        <li class="<%= InternalWorkingMode.Equals(SnGroupWorkingMode.DiscussionBoard) ? "current" : String.Empty %>">
                                            <asp:HyperLink ID="lnkDiscussion" runat="server" CausesValidation="false">
                                                <span id="labDiscussion" runat="server">Discussion board</span></asp:HyperLink></li>
                                    </ul>
                                </div>
                                <div class="tab-line"></div>
                            </td>
                        </tr>                        
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlWarning" style="font-size: 11px;">
            <asp:Literal runat="server" ID="ltlWarning"></asp:Literal>
            <br />
            <asp:LinkButton runat="server" ID="lnkJoin" OnCommand="lnkJoin_Command"></asp:LinkButton>
        </asp:Panel>
        <MonoX:Wall runat="server" ID="snWallNotes" Title="Wall notes" ShowPrivacyEditor="false" />
        <MonoX:Photos runat="server" ID="snPhotos" />
        <div class="group-view">
            <MonoX:Discussion runat="server" ID="snDiscussion" RelatedContentVisible="false" BoardTitleVisible="false"  />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
