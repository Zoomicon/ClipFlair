<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.ProfileModule.UserProfile" %>

<%@ Register Src="~/MonoX/ModuleGallery/Mobile/ProfileModule/EditProfile.ascx" TagPrefix="monox" TagName="EditProfile" %>

<div style="display: none;">
    <asp:PlaceHolder ID="plhHeader" runat="server">
        <MonoX:StyledButton ID="btnSave" runat="server" EnableNativeButtonMode="true" CausesValidation="true" />
    </asp:PlaceHolder>
</div>
<asp:PlaceHolder ID ="plhPreview" runat="server">
    <monox:EditProfile id="previewProfile" runat="server" IsPreviewMode="true" ></monox:EditProfile>
    <asp:PlaceHolder id="plhPreviewTemplate" runat="server"></asp:PlaceHolder>
</asp:PlaceHolder>
<asp:PlaceHolder ID ="plhEdit" runat="server">
    <monox:EditProfile id="editProfile" runat="server" IsPreviewMode="false"></monox:EditProfile>
    <asp:PlaceHolder id="plhEditTemplate" runat="server"></asp:PlaceHolder>
</asp:PlaceHolder>
<asp:PlaceHolder ID ="plhFooter" runat="server">
    <div class="InputForm">
        <dl>
            <dd>
                <font color="red"><strong>
                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal></strong>
                </font><font color="Green"><strong>
                    <asp:Literal ID="labMessage" runat="server" EnableViewState="False"></asp:Literal></strong></font>
            </dd>
        </dl>
    </div>
    <MonoX:StyledButton ID="btnSaveFooter" runat="server" CausesValidation="true" EnableNativeButtonMode="true" />    
</asp:PlaceHolder>
