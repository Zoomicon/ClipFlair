<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="LoginRpx.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.LoginRpx" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>
<%@ Register Assembly="MonoSoftware.Web" Namespace="MonoSoftware.Web.Controls" TagPrefix="MonoXControls" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:Panel runat="server" ID="pnlIframe">
    <iframe src="<%= this.RpxEmbedFrameSrc %>" allowtransparency="true" scrolling="no" frameborder="0" style="<%= this.FrameStyle %>"></iframe>
</asp:Panel>
<asp:Panel runat="server" ID="pnlEmail" Visible="false">
    <div class="rpx-login">         
        <div class="form">                        
            <div class="title">                            
                <%= DefaultResources.LoginRpx_Title %>
            </div>
            <div class="content">
                <%= DefaultResources.LoginRpx_EmailDescription %>
                <br /><br />
                <div>
                    <asp:Label ID="lblEmail" AssociatedControlID="txtEmail" runat="server"><%= DefaultResources.LoginRpx_Email %></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vldRequiredEmail" runat="server" ControlToValidate="txtEmail" Text="!" CssClass="validator ValidatorAdapter" Display="Dynamic" />
                    <MonoXControls:RegExValidator ID="regexEmail" CssClass="ValidatorAdapter"  ControlToValidate="txtEmail" Display="Dynamic" Text="!" ValidationType="EMail" runat="server"></MonoXControls:RegExValidator>
                </div>
                <div>
                    <asp:Button ID="btnSaveEmail" CommandName="Login" runat="server" CssClass="CssFormButton" OnClick="btnSaveEmail_Click" Text='<%$ Code:DefaultResources.Button_OK %>'></asp:Button>
                </div>
            
            </div>
        </div>
    </div>    
</asp:Panel>
<asp:Literal runat="server" ID="ltlWarning"></asp:Literal>