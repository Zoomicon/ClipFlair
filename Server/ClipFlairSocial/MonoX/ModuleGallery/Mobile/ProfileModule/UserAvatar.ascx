<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAvatar.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.ProfileModule.UserAvatar" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Blog" TagPrefix="monox" %>
<%@ Register TagPrefix="radU" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.HelperClasses" %>
<asp:ScriptManagerProxy ID="proxySM" runat="server">
    <Scripts>
    </Scripts>
</asp:ScriptManagerProxy>
<MonoX:Gravatar ID="gravatar" runat="server" />
<div class="user-score">
    <%= MonoSoftware.MonoX.BusinessLayer.UserProfileBLL.GetInstance().GetUserReputation(UserId) %>
    <span><%= MonoSoftware.MonoX.Resources.UserProfileResources.UserScore %></span>
</div>

<asp:PlaceHolder ID="plhHidden" runat="server" Visible="false">
<MonoX:StyledButton ID="btnBrowse" runat="server" />
<h3>
<asp:Label ID="labInvalidFile" runat="server"></asp:Label></h3>

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
            <MonoX:StyledButton ID="btnSaveFooter" runat="server" ValidationGroup="AvatarUpload" />
            <MonoX:StyledButton ID="btnClearFooter" runat="server" CausesValidation="false" />
        </asp:PlaceHolder>
    </div>
</radU:RadToolTip>
</asp:PlaceHolder>