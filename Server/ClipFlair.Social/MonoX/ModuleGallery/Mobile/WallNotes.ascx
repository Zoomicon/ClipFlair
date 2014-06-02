<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WallNotes.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.WallNotes" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Blog" TagPrefix="monox" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.PrivacyManager"
    TagPrefix="MonoXPrivacyManager" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="LightBox" Src="~/MonoX/controls/LightBox.ascx" %>
<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">
</asp:ScriptManagerProxy>
<asp:Panel ID="pnlContainer" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always" >
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlNoteEdit">
                <div class="social-network">
                    <div class="two-column">
                        <div class="left-side">
                            <MonoX:Gravatar ID="ctlGravatar" runat="server" />
                        </div>
                        <div class="right-side">
                            <asp:TextBox runat="server" ID="txtInput" CssClass="jq_expandingTextBox jq_swap_value whats-on-your-mind" TextMode="MultiLine" AutoPostBack="false"></asp:TextBox>
                        </div>
                    </div>
                    <MonoXPrivacyManager:PrivacyEditor ID="privacyEditor" runat="server" Visible="false"></MonoXPrivacyManager:PrivacyEditor>
                    <MonoX:StyledButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="CommentButton" EnableNativeButtonMode="true" />
                    <MonoX:SilverlightUpload runat="server" ID="ctlUpload" EnableFileGallery="false" CssClass="wall-file-upload" Visible="false" />
                    <MonoX:FileGallery ID="ctlFileGallery" runat="server" ParentEntityType="Note" Visible="false" />
                </div>
            </asp:Panel>
            
            <asp:ListView data-role="listview" ID="lvItems" runat="server">
                <LayoutTemplate>
                    <ul data-role="listview">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
            </asp:ListView>
            <asp:Label runat="server" ID="lblNoData"></asp:Label>
            <br />
            <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
                <PagerTemplate>
                </PagerTemplate>
            </mono:Pager>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Panel>
