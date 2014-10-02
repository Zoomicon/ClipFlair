<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="GroupEdit.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupEdit" %>

<%@ Register TagPrefix="MonoX" TagName="Upload" Src="~/MonoX/ModuleGallery/UploadModule.ascx" %>
<%@ Register Src="~/MonoX/controls/UserPicker.ascx" TagPrefix="MonoX" TagName="UserSearch" %>

<asp:Panel runat="server" ID="pnlContainer">
    <!--!!!CLIPFLAIR-->
        
        <h1>Create / Edit group</h1>
    <!--/!!!CLIPFLAIR-->
    <div class="input-form">
        <dl>
            <dd class="validation-element">
                <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="Modification" CssClass="validation-summary" ShowSummary="true" EnableClientScript="true" />
            </dd>
            <dd>
                <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName"></asp:Label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredName" runat="server" CssClass="validator ValidatorAdapter" ValidationGroup="Modification" ControlToValidate="txtName" SetFocusOnError="true" Display="Dynamic" Text="!"></asp:RequiredFieldValidator>
            </dd>
            <dd>
                <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription"></asp:Label>
                <asp:TextBox Rows="5" TextMode="MultiLine" runat="server" ID="txtDescription"></asp:TextBox>
            </dd>
            <dd>
                <asp:Label ID="lblCategories" AssociatedControlID="ddlCategories" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlCategories" runat="server">
                </asp:DropDownList>
                <!--!!!CLIPFLAIR-->
                <div style="padding:12px 0 0 0;">
                    <span style="font-size: 12px;"><asp:Label ID="lblCategoryNote" runat="server" AssociatedControlID="ddlCategories" CssClass="legend"></asp:Label></span>
                </div>
                <!--/!!!CLIPFLAIR-->
            </dd>
            <dd>
                <asp:Label ID="lblPicture" AssociatedControlID="ctlUpload" runat="server"></asp:Label><br />
                <asp:Image runat="server" ID="imgGroupLogo" CssClass="img" />
                <!--!!!CLIPFLAIR-->
                <MonoX:Upload ControlObjectsVisibility="None" CssClass="upload-control" runat="server" ID="ctlUpload" />
                <!--/!!!CLIPFLAIR-->
            </dd>
            <dd class="my_checkbox">               
                <asp:CheckBox ID="chkPrivacy" Checked="true" runat="server"></asp:CheckBox> 
                <asp:Label ID="lblCheck" AssociatedControlID="chkPrivacy" runat="server"></asp:Label>&nbsp;&nbsp;
                <asp:Label ID="lblPrivacy" CssClass="my_label" runat="server"></asp:Label>                
            </dd>
            <dd>
                <!--CLIPFLAIR<hr />-->
                <asp:Label ID="lblPrivacyLegend" runat="server" CssClass="legend"></asp:Label>
                <!--CLIPFLAIR<hr />-->
            </dd>
            <dd class="AdminAutoCompleteBox">
                <asp:Panel ID="pnlAdmins" runat="server">
                    <asp:Label ID="labUserAdmins" runat="server" AssociatedControlID="userAdmins"></asp:Label>
                    <MonoX:UserSearch id="userAdmins" runat="server" Width="100%"></MonoX:UserSearch>
                </asp:Panel>
            </dd>
        </dl>
    </div>
</asp:Panel>
<!--!!!CLIPFLAIR-->
<div class="button-holder" style="padding-top:20px;">
    <asp:PlaceHolder ID="plhActions" runat="server">
        <MonoX:StyledButton id="btnCancel" runat="server" CssClass="cancel-btn float-right" CausesValidation="false" ValidationGroup="Modification" OnClick="btnCancel_Click" />
        <MonoX:StyledButton id="btnSave" CssClass="main-button submit-btn float-right" runat="server" CausesValidation="true" ValidationGroup="Modification" OnClick="btnSave_Click" />
    </asp:PlaceHolder>
    <b style="color: Red;"><asp:Literal ID="ltlMessage" runat="server"></asp:Literal></b>
</div>
<!--/!!!CLIPFLAIR-->
