<%@ Control
    Language="C#"
    AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.ModuleGallery.UserAvatar"
    CodeBehind="UserAvatar.ascx.cs" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Blog" TagPrefix="monox" %>
<%@ Register TagPrefix="radU" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="monox" TagName="CropImage" Src="~/MonoX/controls/CropImage/CropImage.ascx" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.HelperClasses" %>

<asp:ScriptManagerProxy ID="proxySM" runat="server">
    <Scripts>
    </Scripts>
</asp:ScriptManagerProxy>

    <div id="rowGravatar" runat="server" class="user-avatar"> 
        <MonoX:Gravatar ID="gravatar" runat="server" />
        <MonoX:StyledButton ID="btnBrowse" runat="server" />
    </div>
        
    <!--<div class="user-score">
        <%= MonoSoftware.MonoX.BusinessLayer.UserProfileBLL.GetInstance().GetUserReputation(UserId) %>
        <span><%= MonoSoftware.MonoX.Resources.UserProfileResources.UserScore %></span>
    </div>-->
    <h3><asp:Label ID="labInvalidFile" runat="server"></asp:Label></h3>

<script type="text/javascript">
    function <%= GetValidatorFunctionName() %>(source, arguments) {
        arguments.IsValid = getRadUpload('<%= uploadAvatar.ClientID %>').validateExtensions();
        }
</script>

<radU:RadToolTip ID="wndUpload" runat="server" RelativeTo="Element" TargetControlID="rowGravatar"
    Skin="Default" Modal="true" Position="MiddleRight" ShowEvent="FromCode" ManualClose="true" RenderInPageRoot="true">
    <div class="upload-container">
        <asp:PlaceHolder ID="plhHeader" runat="server">
            <h3>
                <asp:Label ID="labCaption" runat="server"></asp:Label></h3>
            <asp:CustomValidator ID="validFile" runat="server" Display="Static" 
                Text="<%$ Code: MonoSoftware.MonoX.Resources.UserProfileResources.UserAvatar_InvalidFile%>"
                ErrorMessage="<%$ Code: MonoSoftware.MonoX.Resources.UserProfileResources.UserAvatar_InvalidFile%>"
                ValidationGroup="AvatarUpload">
            </asp:CustomValidator>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhEdit" runat="server">
            <div id="rowUpload" runat="server">
                <radU:RadUpload ID="uploadAvatar" runat="server" InitialFileInputsCount="1" OverwriteExistingFiles="true"
                    Skin="Default" MaxFileInputsCount="1" Width="100px" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhFooter" runat="server">
            <div class="small-btn">
                <MonoX:StyledButton ID="btnSaveFooter" runat="server" ValidationGroup="AvatarUpload" CssClass="main-button submit-btn float-left" />
                <MonoX:StyledButton ID="btnClearFooter" runat="server" CausesValidation="false" CssClass="cancel-btn float-left" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plhCropImage">
            <monox:CropImage  runat="server" ID="cropImage" UseBase64ImageEncoding="true" CropMinSizeWidth="<%$ Code: AvatarSize %>" 
            CropMinSizeHeight="<%$ Code: AvatarSize %>" DesiredImageWidth="<%$ Code: AvatarSize %>"
            DesiredImageHeight="<%$ Code: AvatarSize %>" />
        </asp:PlaceHolder>
    </div>
</radU:RadToolTip>
