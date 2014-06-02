<%@ Import Namespace="MonoSoftware.MonoX.DAL.HelperClasses" %>
<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.UserProfile" Codebehind="UserProfile.ascx.cs" %>

<%@ Register Src="~/MonoX/ModuleGallery/ProfileModule/EditProfile.ascx" TagPrefix="monox" TagName="EditProfile" %>

<div style="display: none;">
    <asp:PlaceHolder ID="plhHeader" runat="server">
        <MonoX:StyledButton ID="btnSave" runat="server" CausesValidation="true" CssClass="main-button submit-btn float-left" />
    </asp:PlaceHolder>
</div>

<asp:PlaceHolder ID ="plhPreview" runat="server">
    <div class="preview-profile">
        <monox:EditProfile id="previewProfile" runat="server" IsPreviewMode="true" ></monox:EditProfile>
        <asp:PlaceHolder id="plhPreviewTemplate" runat="server"></asp:PlaceHolder>
    </div>
</asp:PlaceHolder>

<asp:PlaceHolder ID ="plhEdit" runat="server">
    <div class="edit-profile">
        <monox:EditProfile id="editProfile" runat="server" IsPreviewMode="false"></monox:EditProfile>
        <asp:PlaceHolder id="plhEditTemplate" runat="server"></asp:PlaceHolder>
    </div>
</asp:PlaceHolder>

<asp:PlaceHolder ID ="plhFooter" runat="server">
    <div class="InputForm">
        <dl>
            <dd>
                <font color="red"><strong>
                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal></strong>
                </font>
                <font color="Green"><strong>
                    <asp:Literal ID="labMessage" runat="server" EnableViewState="False"></asp:Literal></strong>
                </font>
            </dd>
        </dl>
        <br /><br />
    </div>
    <MonoX:StyledButton ID="btnSaveFooter" runat="server" CausesValidation="true" CssClass="main-button submit-btn float-left" />    
</asp:PlaceHolder>
