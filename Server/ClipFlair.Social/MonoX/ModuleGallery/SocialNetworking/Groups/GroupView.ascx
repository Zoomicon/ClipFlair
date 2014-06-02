<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="GroupView.ascx.cs"Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupView" %>

<%@ Register TagPrefix="MonoX" TagName="Wall" Src="~/MonoX/ModuleGallery/SocialNetworking/WallNotes.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Photos" Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/PhotoGalleryContainer.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Discussion" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/Groups-descussion/DiscussionContainer.ascx" %>
<%@ Import Namespace="MonoSoftware.MonoX.Utilities" %>

<asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
    <ContentTemplate>       
        <h1><asp:Label ID="lblTitle" runat="server"></asp:Label></h1>
        <div class="tabs clearfix">
            <ul id="rowSwitch" runat="server">
                <li class="<%= InternalWorkingMode.Equals(SnGroupWorkingMode.Wall) ? "current" : String.Empty %>">
                    <asp:HyperLink ID="lnkWall" runat="server" CausesValidation="false" CssClass="tab-button">
                        <span id="labWall" runat="server">Wall</span>
                    </asp:HyperLink>
                </li>
                <li class="<%= InternalWorkingMode.Equals(SnGroupWorkingMode.Photos) ? "current" : String.Empty %>">
                    <asp:HyperLink ID="lnkPhotos" runat="server" CausesValidation="false" CssClass="tab-button">
                        <span id="labPhotos" runat="server">Photos</span>
                    </asp:HyperLink>
                </li>
                <li class="<%= InternalWorkingMode.Equals(SnGroupWorkingMode.DiscussionBoard) ? "current" : String.Empty %>">
                    <asp:HyperLink ID="lnkDiscussion" runat="server" CausesValidation="false" CssClass="tab-button">
                        <span id="labDiscussion" runat="server">Discussion board</span>
                    </asp:HyperLink>
                </li>
                <!--CLIPFLAIR-->
                <li class="join-group">
                    <asp:Panel runat="server" ID="pnlWarning">
                        <asp:LinkButton runat="server" ID="lnkJoin" CssClass="styled-button main-button submit-btn" OnCommand="lnkJoin_Command"></asp:LinkButton>
                    </asp:Panel>
                </li>
                <!--/CLIPFLAIR-->
            </ul>
            <div class="tab-line"></div>
        </div>        
        <!--CLIPFLAIR<asp:Literal runat="server" ID="ltlWarning2"></asp:Literal>
        <br />-->
        <asp:Panel runat="server" ID="pnlWarning2" CssClass="warnings">
            <asp:Literal runat="server" ID="ltlWarning"></asp:Literal>
            <br />
            <asp:LinkButton runat="server" ID="lnkJoin2" OnCommand="lnkJoin_Command"></asp:LinkButton>
        </asp:Panel>
        <MonoX:Wall runat="server" ID="snWallNotes" Title="Wall notes" ShowPrivacyEditor="false" />
        <MonoX:Photos runat="server" ID="snPhotos" UsePrettyPhoto="true" />
        <MonoX:Discussion runat="server" ID="snDiscussion" RelatedContentVisible="false"  />
    </ContentTemplate>
</asp:UpdatePanel>
