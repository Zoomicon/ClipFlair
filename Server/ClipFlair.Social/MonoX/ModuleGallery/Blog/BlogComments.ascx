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
        <br />
        <div class="input-form">
            <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ValidationGroup="BlogCommentEntry" ShowSummary="true" CssClass="validationSummary"/>
            <dl>
                <dd>
                    <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" CssClass="new-label-width"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server" class="short-textbox padding1-after"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="ValidatorAdapter" Text="!" ControlToValidate="txtName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                </dd>        
                <dd>
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="new-label-width"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" class="short-textbox padding1-after"></asp:TextBox>
                    <MonoXControls:RegExValidator ID="regexEmail" CssClass="ValidatorAdapter" ControlToValidate="txtEmail" Display="Dynamic" Text="!" Font-Bold="true" ValidationType="EMail" runat="server"></MonoXControls:RegExValidator>
                    <asp:RequiredFieldValidator id="rfvEmail" Runat="server" CssClass="ValidatorAdapter" Display="Dynamic" Text="!" Font-Bold="true" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                </dd>
                <dd>
                    <asp:Label ID="lblUrl" runat="server" AssociatedControlID="txtUrl" CssClass="new-label-width"></asp:Label>
                    <asp:TextBox ID="txtUrl" runat="server" class="short-textbox padding1-after"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regexUrl" runat="server" CssClass="ValidatorAdapter" ControlToValidate="txtUrl" Display="Dynamic" Text="!" 
                     ValidationExpression="^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$" />
                </dd>        
                <dd>
                    <asp:Label ID="lblComment" runat="server" AssociatedControlID="txtComment" CssClass="new-label-width"></asp:Label>
                    <asp:TextBox Rows="5" TextMode="MultiLine" runat="server" ID="txtComment" class="padding1-after"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvComment" runat="server" CssClass="ValidatorAdapter" Text="!" ControlToValidate="txtComment" Display="Dynamic"></asp:RequiredFieldValidator>
                </dd>        
                <dd>
                    <asp:Label ID="lblNotify" AssociatedControlID="chkNotify" runat="server">&nbsp;</asp:Label>
                    <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkNotify" runat="server" TextAlign="Right" style="float: left;"></asp:CheckBox>
                    <span class="checkbox-notes" style="float: left;"><%= BlogResources.Comments_Notify %></span>
                </dd>
            
        </div>

        <div class="input-form">
            <div class="button-holder">         
                <MonoX:StyledButton id="btnSave" runat="server" CausesValidation="true" OnClick="btnSave_Click"  />
            </div>
        </div>

    </asp:Panel>
</asp:Panel>
