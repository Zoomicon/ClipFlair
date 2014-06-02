<%@ Control
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="MessageCenter.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.InMail.MessageCenter" %>

<%@ Register TagPrefix="MonoX" TagName="MessageList" Src="~/MonoX/ModuleGallery/SocialNetworking/InMailMessaging/MessageList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="MessageCreate" Src="~/MonoX/ModuleGallery/SocialNetworking/InMailMessaging/MessageCreate.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="MessageDetails" Src="~/MonoX/ModuleGallery/SocialNetworking/InMailMessaging/MessageDetails.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="MonoSoftware.MonoX.Utilities"  %>

<script type="text/javascript">
    function previewRow(messageId) {
        var btnPreview = $('#<%= btnPreviewRow.ClientID %>');        
        if (btnPreview.length > 0) {
            $('#<%= hfSelectedRow.ClientID %>').val(messageId);
            window.location.href = btnPreview[0].href;
            return true;
        }
    }

    function selectRow(seledtedRow) {
        $('.message-list').removeClass('current');
        seledtedRow.setAttribute("class", "message-list current");
    }
</script>

<asp:UpdatePanel ID="ajaxPanelMain" runat="server" UpdateMode="Always"> 
    <ContentTemplate>
        <div class="messages">
            <!--!!!CLIPFLAIR-->
            <div class="tabs clearfix">
                <ul>
                    <li>
                        <asp:LinkButton ID="btnRecivedMessages" CausesValidation="false" runat="server" CommandArgument="MessageList" OnClick="btnTab_Click" CssClass="selected" ></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnSentMessages" CausesValidation="false" runat="server" CommandArgument="MessageListSent" OnClick="btnTab_Click" ></asp:LinkButton>
                    </li>
                    <li>
                        <div class="main-button-new-message"><asp:LinkButton ID="btnNewMessage" CausesValidation="false" runat="server" CommandArgument="MessageCreate" OnClick="btnTab_Click" ></asp:LinkButton></div>
                    </li>
                    <li>
                        <asp:HiddenField ID="hfSelectedTabs" runat="server" Value="MessageList" />
                    </li>
                </ul>
                <div class="tab-line"></div>
            </div>
            <div class="clearfix">
                <MonoX:MessageList ID="messageList" runat="server" MessageStatusFilter="Received" Title="Received messages" />
                <MonoX:MessageList ID="messageListSent" runat="server" MessageStatusFilter="Sent" Title="Sent messages" />
                <MonoX:MessageDetails runat="server" ID="messageDetails" MessageStatusFilter="Sent" Title="Display messages" AllowFileUpload="true" />
                <MonoX:MessageCreate runat="server" ID="messageCreate" Title="New message" AllowFileUpload="true" />
            </div>
        </div>
        <div style="display: none;">
            <asp:LinkButton ID="btnPreviewRow" runat="server" CausesValidation="false" OnClick="btnPreviewRow_Click" CssClass="preview-row">
            </asp:LinkButton>
            <asp:HiddenField ID="hfSelectedRow" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
