<%@ Control
    Language="C#"
    AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogComments"
    Codebehind="BlogComments.ascx.cs" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>
<%@ Register Assembly="MonoSoftware.Web" Namespace="MonoSoftware.Web.Controls" TagPrefix="MonoXControls" %>
<%@ Register TagPrefix="MonoX" TagName="BlogCommentsList" Src="~/MonoX/ModuleGallery/Blog/BlogCommentsList.ascx" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:Panel ID="pnlContainer" runat="server">
    <MonoX:BlogCommentsList ID="ctlCommentsList" runat="server"></MonoX:BlogCommentsList>
    <asp:Panel runat="server" ID="pnlForm">
        <!--CCLIPFLAIR-->
        <div class="input-form comment-form">
            <dl>
                <dd>
                    <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="BlogCommentEntry" ShowSummary="true" CssClass="validation-summary"/>
                </dd>
                <dd>
                    <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="validator ValidatorAdapter" Text="!" ControlToValidate="txtName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                </dd>        
                <dd id="rowEmail" runat="server" class="half">
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <MonoXControls:RegExValidator ID="regexEmail" CssClass="ValidatorAdapter" ControlToValidate="txtEmail" Display="Dynamic" Text="!" Font-Bold="true" ValidationType="EMail" runat="server"></MonoXControls:RegExValidator>
                    <asp:RequiredFieldValidator id="rfvEmail" Runat="server" CssClass="validator ValidatorAdapter" Display="Dynamic" Text="!" Font-Bold="true" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                </dd>
                <dd>
                    <asp:Label ID="lblUrl" runat="server" AssociatedControlID="txtUrl"></asp:Label>
                    <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regexUrl" runat="server" CssClass="validator ValidatorAdapter" ControlToValidate="txtUrl" Display="Dynamic" Text="!" 
                     ValidationExpression="^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$" />
                </dd>        
                <dd>
                    <asp:Label ID="lblComment" runat="server" AssociatedControlID="txtComment"></asp:Label>
                    <asp:TextBox Rows="5" TextMode="MultiLine" runat="server" ID="txtComment"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvComment" runat="server" CssClass="validator ValidatorAdapter" Text="!" ControlToValidate="txtComment" Display="Dynamic"></asp:RequiredFieldValidator>
                </dd>        
                <dd class="offset my_checkbox">                   
                    <asp:CheckBox ID="chkNotify" runat="server"></asp:CheckBox>
                    <asp:Label ID="lblNotify" AssociatedControlID="chkNotify" runat="server"></asp:Label>                    
                    <span class="my_label"><%= BlogResources.Comments_Notify %></span>
                </dd>
                <dd class="button-holder">
                    <MonoX:StyledButton id="btnSave" runat="server" CausesValidation="true" OnClick="btnSave_Click" CssClass="main-button submit-btn float-left"  />
                </dd>
            </dl>
        </div>
    </asp:Panel>
</asp:Panel>
