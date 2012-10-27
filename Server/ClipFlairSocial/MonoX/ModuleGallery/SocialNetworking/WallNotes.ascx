<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WallNotes.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.WallNotes" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager"
    TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Blog" TagPrefix="monox" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.PrivacyManager" TagPrefix="MonoXPrivacyManager" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="LightBox" Src="~/MonoX/controls/LightBox.ascx" %>
<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">
</asp:ScriptManagerProxy>
<asp:Panel ID="pnlContainer" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always" >
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlNoteEdit">
                <div class="social-network border-none">
                    <table cellpadding="0" cellspacing="0" class="wallNote">
                    <tr>
                        <td valign="top">
                            <div class="gravatar">
                                <MonoX:Gravatar ID="ctlGravatar" runat="server" />
                            </div>
                        </td>
                        <td class="snMainNote">
                            <MonoX:LightBox runat="server" DefaultButton="btnSave">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ID="txtInput" CssClass="jq_expandingTextBox jq_swap_value"
                                        TextMode="MultiLine" AutoPostBack="false"></asp:TextBox>
                                    <table cellpadding="0" cellspacing="0" width="100%" class="snNoteTable">
                                        <tr class="snUploadRow">
                                            <td>
                                                <MonoX:StyledButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="wall-button" />
                                                <div class="wall-privacy">
                                                    <MonoXPrivacyManager:PrivacyEditor ID="privacyEditor" runat="server"></MonoXPrivacyManager:PrivacyEditor>
                                                </div>
                                                <MonoX:SilverlightUpload runat="server" ID="ctlUpload" EnableFileGallery="false" CssClass="wall-file-upload" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </MonoX:LightBox>
                        </td>
                    </tr>
                </table>
                <MonoX:FileGallery ID="ctlFileGallery" runat="server" ParentEntityType="Note" />
                </div>
            </asp:Panel>
            <asp:ListView ID="lvItems" runat="server">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                </ItemTemplate>
            </asp:ListView>
            <br/><br/>
            <span class="big-notes"><asp:Literal runat="server" ID="ltlNoData"></asp:Literal></span>
            <br />
            <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true"
                AutoPaging="false">
                <PagerTemplate>
                </PagerTemplate>
            </mono:Pager>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
