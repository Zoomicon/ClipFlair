<%@ Control
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="MessageCreate.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.InMail.MessageCreate" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoXControls" Namespace="MonoSoftware.MonoX.Controls" Assembly="MonoX" %>
<%@ Register TagPrefix="MonoX" TagName="AddressEntry" Src="~/MonoX/Controls/UserPicker.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="CustomRadEditor" Src="~/MonoX/Controls/CustomRadEditor.ascx" %>

<MonoXControls:MonoXWindowManager ID="windowDialog" runat="server" Skin="Metro" Width="800" Height="600" VisibleStatusbar="false" IconUrl='<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.popup_icon_png %>' ReloadOnShow="true" Behaviors="Close,Move" KeepInScreenBounds="true" Modal="true"></MonoXControls:MonoXWindowManager>
<!--CLIPFLAIR-->
<div class="message-center">
    <asp:Panel runat="server" ID="pnlContainer">
        <div id="step1form" class="input-form message-create dirtyFormMarker">
            <dl>
                <dd>
                    <asp:ValidationSummary ID="summary" runat="server" CssClass="validation-summary" DisplayMode="List" ShowSummary="true" />
                </dd>
                <asp:PlaceHolder ID="plhRecipients" runat="server">
                    <dd>
                        <asp:Label ID="lblRecipients" runat="server" AssociatedControlID="ddlRecipients" CssClass="label-width"></asp:Label>
                        <div class="message-address-entry">
                            <MonoX:AddressEntry runat="server" Height="200" Width="100%" IsRequired="true" ID="ddlRecipients" 
                            UserFilterMode="ShowFriendsListsGroups" UserAddressFormat="Fullname" />
                        </div>
                        <asp:ImageButton runat="server" ID="btnGroup" CausesValidation="false" ImageUrl='<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.Sn.group_png %>' CssClass="recipients" />
                    </dd>
                </asp:PlaceHolder>
                <dd>
                    <asp:Label ID="lblSubject" runat="server" AssociatedControlID="txtSubject"></asp:Label>
                    <asp:TextBox ID="txtSubject" runat="server" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredSubject" runat="server" CssClass="validator ValidatorAdapter" ControlToValidate="txtSubject" SetFocusOnError="true" Display="Static"></asp:RequiredFieldValidator>
                </dd>        
                <dd class="editor-height">
                    <asp:Label ID="lblMessage" runat="server" AssociatedControlID="txtMessage"></asp:Label>
                    <asp:TextBox ID="txtMessage" Rows="8" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <MonoX:CustomRadEditor ID="txtHTMLMessage" ContentAreaMode="Div" runat="server" ToolBarMode="ShowOnFocus" IsRequired="true"></MonoX:CustomRadEditor>
                </dd>
                <!--<asp:Panel runat="server" ID="pnlUpload">-->
                <dd>
                    <asp:Label ID="lblUpload" AssociatedControlID="ctlUpload" runat="server"></asp:Label>
                    <MonoX:SilverlightUpload runat="server" ID="ctlUpload" Width="100%" EnableFileGallery="false"  />
                </dd>
                <dd class="upload-cont">
                    <MonoX:FileGallery ID="ctlFileGallery" runat="server" CssClass="rightLabel file-gallery"  />
                </dd>
                <!--</asp:Panel>-->
                <asp:PlaceHolder ID="plhSendMail" runat="server">
                <dd>
                    <asp:CheckBox CssClass="innerCheckboxContent" BorderStyle="None" BorderWidth="0px" ID="chkSendMail" runat="server" TextAlign="Right" Checked="true" style="float: left;"></asp:CheckBox>
                    <span class="my_label"><%= SocialNetworkingResources.Messaging_CreateMessage_SendMailCopy %></span>
                </dd>
                </asp:PlaceHolder>
                <dd class="button-holder">
                    <!--CLIPFLAIR-->
                    <MonoX:StyledButton id="btnSend" runat="server" CausesValidation="true" OnClick="btnSend_Click" CssClass="main-button submit-btn float-left" />
                    <MonoX:StyledButton id="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click" CssClass="back-btn float-left" />
                </dd>
                <dd>
                    <asp:Label runat="server" ForeColor="Red" ID="lblWarning" CssClass="result-message"></asp:Label> 
                </dd>
            </dl>
        </div>
    </asp:Panel>
</div>