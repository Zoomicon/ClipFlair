<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="Comments.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.Comments" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
<asp:Panel ID="pnlContainer" runat="server"> 
    <asp:Panel runat="server" ID="pnlCommentBox" CssClass="jq_wallCommentBox">
        <div class="comment-composer">
            <div class="textarea-holder">
                <asp:TextBox runat="server" ID="txtInput" CssClass="jq_expandingTextBoxSmall" TextMode="MultiLine" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqInput" runat="server" ControlToValidate="txtInput" Text="*" SetFocusOnError="true" CssClass="validator ValidatorAdapter" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <!--!!!CLIPFLAIR-->
            <div class="small-btn">
                <MonoX:StyledButton ID="btnSave" runat="server" CssClass="main-button submit-btn" OnClick="btnSave_Click" CausesValidation="true" />
            </div>
             <!--/!!!CLIPFLAIR-->
        </div>
    </asp:Panel>
    <asp:ListView ID="lvItems" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate></ItemTemplate>
    </asp:ListView>
    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
        <PagerTemplate></PagerTemplate>
    </mono:Pager>
</asp:Panel>