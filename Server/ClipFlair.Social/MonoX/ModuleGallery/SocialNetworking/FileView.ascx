<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileView.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.FileView" %>
<%@ Register TagPrefix="MonoX" TagName="Comments" Src="~/MonoX/ModuleGallery/SocialNetworking/Comments.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="VideoPlayer" Src="~/MonoX/ModuleGallery/SocialNetworking/FlashVideoPlayer.ascx" %>
<%@ Register Src="~/MonoX/ModuleGallery/RelatedContent.ascx" TagPrefix="MonoX" TagName="RelatedContent" %>
<%@ Register Src="~/MonoX/controls/TagTextBox.ascx" TagPrefix="mono" TagName="TagTextBox" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.PrivacyManager" TagPrefix="MonoXPrivacyManager" %>
<%@ Register TagPrefix="radU" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
<asp:Panel ID="pnlContainer" runat="server">
    <div class="jq_snFileContainer file-view">
        <asp:HyperLink ID="lnkFile" runat="server" Target="_blank">
            <asp:Image runat="server" ID="imgFile" CssClass="image scale-with-grid" />
        </asp:HyperLink><br />
        <MonoX:VideoPlayer runat="server" ID="ctlVideoPlayer" Width="520" Height="330"></MonoX:VideoPlayer>
        <asp:Panel runat="server" ID="pnlDescription" CssClass="input-form">
            <dl>
                <dd>
                    <label><%= MonoSoftware.MonoX.Resources.GlobalText.Title %></label>
                    <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                </dd>
                <dd>
                    <label><%= MonoSoftware.MonoX.Resources.GlobalText.Description %></label>
                    <asp:TextBox runat="server" ID="txtDescription" CssClass="jq_expandingTextBox" TextMode="MultiLine"></asp:TextBox>
                </dd>                
                <dd>
                    <mono:TagTextBox id="tags" runat="server"></mono:TagTextBox>
                </dd>
                <dd>
                    <label><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.FileView_ChangeThumbnail %>:</label>
                    <radU:RadUpload ID="radUpload" runat="server" InitialFileInputsCount="1" OverwriteExistingFiles="true" Skin="Default" />
                </dd>
                <dd>
                    <label><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.FileView_ReplaceExistingFile %>:</label>
                    <radU:RadUpload ID="radReplaceFile" runat="server" InitialFileInputsCount="1" OverwriteExistingFiles="true" Skin="Default" />
                </dd>
                <dd class="button-holder">
                    <MonoX:StyledButton ID="btnSave" runat="server" OnClick="btnSave_Click" /> 
                    <MonoX:StyledButton runat="server" ID="lnkDelete" OnClick="lnkDelete_Click" />
                    <MonoXPrivacyManager:PrivacyEditor ID="privacyEditor" runat="server" CssClass="privacy-popup" />
                </dd>
            </dl>            
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDescriptionLiteral">
            <p>
                <strong><%= MonoSoftware.MonoX.Resources.GlobalText.Title %>:</strong><br />
                <asp:Literal runat="server" ID="labTitle"></asp:Literal>
            </p>
            <p>
                <strong><%= MonoSoftware.MonoX.Resources.GlobalText.Description %>:</strong><br />
                <asp:Literal runat="server" ID="ltlDescription"></asp:Literal>
            </p>
            <asp:PlaceHolder ID="plhTags" runat="server">
            <p>
                <strong><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.FileView_Tags %>:</strong><br />
                <asp:Literal ID="ltlTags" runat="server"></asp:Literal>
                <asp:PlaceHolder ID="panTags" runat="server"></asp:PlaceHolder>
            </p>
            </asp:PlaceHolder>
        </asp:Panel>
        
        <MonoX:RelatedContent id="relatedContent" runat="server"></MonoX:RelatedContent>
        
        <asp:PlaceHolder ID="plhComments" runat="server">
            <div class="date-option">
                <asp:Literal runat="server" ID="ltlDate"></asp:Literal>&nbsp;|&nbsp;
                <asp:HyperLink ID="lnkComments" runat="server" NavigateUrl="javascript:void(0);" CssClass="jq_wallCommentAction"></asp:HyperLink>
            </div>
            <div class="comments">
                <MonoX:Comments ID="ctlComments" PagingEnabled="true" runat="server" />
            </div>
        </asp:PlaceHolder>
    </div>
</asp:Panel>
