<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageCreate.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.MessageCreate" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoXControls" Namespace="MonoSoftware.MonoX.Controls" Assembly="MonoX" %>
<%@ Register TagPrefix="MonoX" TagName="AddressEntry" Src="~/MonoX/controls/UserPicker.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>

<MonoXControls:MonoXWindowManager ID="windowDialog" runat="server" Skin="Metro" Width="800" Height="600" VisibleStatusbar="false" IconUrl='<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.popup_icon_png %>' ReloadOnShow="true" Behaviors="Close,Move" KeepInScreenBounds="true" Modal="true"></MonoXControls:MonoXWindowManager>
<asp:Panel runat="server" ID="pnlContainer">
    <div class="input-form new-message">
    <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="MessageCreateValidation" ShowSummary="true" />
    <dl>    
        <dd>
            <asp:Label ID="lblRecipients" runat="server" AssociatedControlID="ddlRecipients" CssClass="label-width"></asp:Label>
            <MonoX:AddressEntry runat="server" Height="200" Width="94%" ID="ddlRecipients" UserFilterMode="ShowFriendsListsGroups"/>
            <asp:ImageButton runat="server" ID="btnGroup" ImageUrl='<%$ Code: MonoSoftware.MonoX.Utilities.UrlUtility.ResolveThemeUrl("img/Sn/group.png") %>' />
        </dd>        
        <dd>
            <asp:Label ID="lblSubject" runat="server" AssociatedControlID="txtSubject"></asp:Label>
            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="requiredSubject" runat="server" CssClass="ValidatorAdapter"  ValidationGroup="MessageCreateValidation" ControlToValidate="txtSubject" SetFocusOnError="true" Display="Static"></asp:RequiredFieldValidator>
        </dd>        
        <dd>
            <asp:Label ID="lblMessage" runat="server" AssociatedControlID="txtMessage"></asp:Label>
            <asp:TextBox ID="txtMessage" Rows="8" TextMode="MultiLine" runat="server"></asp:TextBox>
        </dd>
        <asp:Panel runat="server" ID="pnlUpload">
        <dd>
            <asp:Label ID="lblUpload" AssociatedControlID="ctlUpload" runat="server"></asp:Label>
            <MonoX:SilverlightUpload width="300" runat="server" ID="ctlUpload" EnableFileGallery="false"  />
        </dd>
        <dd>
            <MonoX:FileGallery ID="ctlFileGallery" runat="server" ParentEntityType="Message" CssClass="rightLabel file-gallery" />
        </dd>
        </asp:Panel>
        <asp:PlaceHolder ID="plhSendMail" runat="server">
        <dd>
            <asp:CheckBox CssClass="innerCheckboxContent" BorderStyle="None" BorderWidth="0px" ID="chkSendMail" runat="server" TextAlign="Right" Checked="true" style="float: left;"></asp:CheckBox>
            <span><%= SocialNetworkingResources.Messaging_CreateMessage_SendMailCopy %></span>
        </dd>
        </asp:PlaceHolder>
    </dl>
    <!--CLIPFLAIR--><MonoX:StyledButton id="btnSend" runat="server" CausesValidation="true" ValidationGroup="MessageCreateValidation" OnClick="btnSend_Click" CssClass="styled-button-clipflair_green"  />
    <MonoX:StyledButton id="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"  />    
    </div>
<asp:Label runat="server" ForeColor="Red" ID="lblWarning"></asp:Label>
</asp:Panel>
