<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comments.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.Comments" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register TagPrefix="MonoX" TagName="LightBox" Src="~/MonoX/controls/LightBox.ascx" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
<asp:Panel ID="pnlContainer" runat="server"> 
    <asp:Panel runat="server" ID="pnlCommentBox" CssClass="jq_wallCommentBox">
        <MonoX:LightBox ID="LightBox1" runat="server">
            <ContentTemplate>
                <asp:TextBox runat="server" ID="txtInput" CssClass="jq_expandingTextBoxSmall" TextMode="MultiLine" ></asp:TextBox> 
                <div style="clear: both; height: 25px;">
                    <MonoX:StyledButton ID="btnSave" runat="server" CssClass="CommentButton" OnClick="btnSave_Click" />
                </div>
            </ContentTemplate>
        </MonoX:LightBox>
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