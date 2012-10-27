<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogComments.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.Blog.BlogComments" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>
<%@ Register Assembly="MonoSoftware.Web" Namespace="MonoSoftware.Web.Controls" TagPrefix="MonoXControls" %>
<%@ Register TagPrefix="MonoX" TagName="BlogCommentsList" Src="~/MonoX/ModuleGallery/Mobile/Blog/BlogCommentsList.ascx" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:Panel ID="pnlContainer" runat="server">
<MonoX:BlogCommentsList ID="ctlCommentsList" runat="server"></MonoX:BlogCommentsList>
<asp:Panel runat="server" ID="pnlForm">
    <div class="input-form" style="margin-top: 25px;">
        <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="BlogCommentEntry" ShowSummary="true" />
        <dl>
            <dd>
                <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" CssClass="label-width"></asp:Label>
                <asp:TextBox ID="txtName" runat="server" class="short-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="ValidatorAdapter" Text="!" ControlToValidate="txtName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
            </dd>        
            <dd id="rowEmail" runat="server">
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="label-width"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" class="short-textbox"></asp:TextBox>
                <MonoXControls:RegExValidator ID="regexEmail" CssClass="ValidatorAdapter" ControlToValidate="txtEmail" Display="Dynamic" Text="!" Font-Bold="true" ValidationType="EMail" runat="server"></MonoXControls:RegExValidator>
                <asp:RequiredFieldValidator id="rfvEmail" Runat="server" CssClass="ValidatorAdapter" Display="Dynamic" Text="!" Font-Bold="true" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
            </dd>
            <dd>
                <asp:Label ID="lblUrl" runat="server" AssociatedControlID="txtUrl" CssClass="label-width"></asp:Label>
                <asp:TextBox ID="txtUrl" runat="server" class="short-textbox"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexUrl" runat="server" CssClass="ValidatorAdapter" ControlToValidate="txtUrl" Display="Dynamic" Text="!" 
                 ValidationExpression="^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$" />
            </dd>        
            <dd>
                <asp:Label ID="lblComment" runat="server" AssociatedControlID="txtComment"></asp:Label>
                <asp:TextBox Rows="5" TextMode="MultiLine" runat="server" ID="txtComment"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvComment" runat="server" CssClass="ValidatorAdapter" Text="!" ControlToValidate="txtComment" Display="Dynamic"></asp:RequiredFieldValidator>
            </dd>        
            <dd>
                <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkNotify" runat="server" Text='<% $Code: BlogResources.Comments_Notify %>' Width="100%"></asp:CheckBox>
            </dd>
        </dl>
    </div>
</asp:Panel>
<div style="overflow: hidden;">
    <MonoX:StyledButton EnableNativeButtonMode="true" id="btnSave" runat="server" CausesValidation="true" OnClick="btnSave_Click"  />
</div>
</asp:Panel>
