<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogSettings.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogSettings" %>
<%@ Register TagPrefix="MonoX" TagName="UserEntry" Src="~/MonoX/controls/UserPicker.ascx" %>
<%@ Register Src="~/MonoX/ModuleGallery/Blog/BlogManageCategories.ascx" TagPrefix="MonoX" TagName="BlogManageCategories" %>

 
<asp:Panel runat="server" ID="pnlContainer">
    <!--needs localization-->
    <h1>Blog Settings</h1>
    <div class="input-form">
        <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="Modification" ShowSummary="true" />
        <dl>
            <dd>
                <asp:Label ID="lblTitle" runat="server" AssociatedControlID="txtTitle"></asp:Label>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requiredTitle" runat="server" CssClass="ValidatorAdapter" ValidationGroup="Modification" ControlToValidate="txtTitle" SetFocusOnError="true" Display="Static"></asp:RequiredFieldValidator>
            </dd>        
            <dd>
                <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription"></asp:Label>
                <asp:TextBox Rows="5" TextMode="MultiLine" runat="server" ID="txtDescription"></asp:TextBox>    
            </dd>
            <dd class="AdminAutoCompleteBox">
                <asp:Label ID="lblEditors" runat="server" AssociatedControlID="ddlEditors"></asp:Label>
                <div><MonoX:UserEntry runat="server" Height="200" Width="98%" ID="ddlEditors" UserFilterMode="ShowAllUsers" IsRequired="false" /></div>
            </dd>
            <dd>
                <MonoX:BlogManageCategories id="blogManageCategories" width="98px" runat="server"></MonoX:BlogManageCategories>
            </dd>
        </dl>
    </div>
</asp:Panel>
<div class="input-form">
    <div class="button-holder">
        <asp:PlaceHolder id="plhActions" Runat="server">            
            <!--CLIPFLAIR-->
            <MonoX:StyledButton id="btnSave" runat="server" CausesValidation="true" ValidationGroup="Modification" OnClick="btnSave_Click" CssClass="main-button submit-btn float-left"  />
            <MonoX:StyledButton id="btnCancel" runat="server" CausesValidation="false" ValidationGroup="Modification" OnClick="btnCancel_Click" CssClass="cancel-btn float-left" />
        </asp:PlaceHolder>
        <b style="color:Red;"><asp:Literal ID="labMessage" runat="server"></asp:Literal></b>
    </div>
</div>
