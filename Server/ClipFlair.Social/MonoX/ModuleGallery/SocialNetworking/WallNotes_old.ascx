<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="WallNotes.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.WallNotes" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Blog" TagPrefix="monox" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.PrivacyManager" TagPrefix="MonoXPrivacyManager" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">
</asp:ScriptManagerProxy>

<asp:Panel ID="pnlContainer" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always" >
        <ContentTemplate>
            <div class="wall">
                <!--CLIPFLAIR--><div class="clipflair_wall">
                    <asp:Panel runat="server" ID="pnlNoteEdit">
                        <div class="composer clearfix">
                            <MonoX:Gravatar ID="ctlGravatar" runat="server" />
                            <div class="textarea-holder">
                                <asp:TextBox runat="server" ID="txtInput" CssClass="jq_expandingTextBox jq_swap_value" TextMode="MultiLine" AutoPostBack="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix">
                            <!--CLIPFLAIR - added class: styled-button-clipflair_green-->
                            <MonoX:StyledButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="styled-button-clipflair_green float-right" />
                            <!--/CLIPFLAIR-->
                            <MonoXPrivacyManager:PrivacyEditor ID="privacyEditor" runat="server" CssClass="privacy"></MonoXPrivacyManager:PrivacyEditor>
                            <MonoX:SilverlightUpload runat="server" ID="ctlUpload" EnableFileGallery="false" />
                        </div>
                        <MonoX:FileGallery ID="ctlFileGallery" runat="server" />
                    </asp:Panel>
                    <!--CLIPFLAIR<hr />-->
                <!--/CLIPFLAIR--></div>
                <!--CLIPFLAIR--><div class="comments-wrapper">
                    <asp:ListView ID="lvItems" runat="server">
                        <LayoutTemplate>
                            <div>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:ListView>
                    <asp:Label runat="server" ID="lblNoData" CssClass="empty-list"></asp:Label>
                    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true"
                        AutoPaging="false">
                        <PagerTemplate>
                        </PagerTemplate>
                    </mono:Pager>
                <!--/CLIPFLAIR--></div>    
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>