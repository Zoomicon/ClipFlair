<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageDetails.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.MessageDetails" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager"
    TagPrefix="mono" %>
<%@ Register TagPrefix="Mono" Assembly="MonoX" Namespace="MonoSoftware.MonoX.Blog" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<!--CLIPFLAIR--><div>
    <div style="font-size: 150%; font-weight: bold; padding: 10px;">
        <asp:Literal runat="server" ID="ltlSubject"></asp:Literal></div>
    <div style="padding: 5px; height: 100%; width: 100%;">
        <asp:ListView ID="lvPosts" runat="server">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </LayoutTemplate>
            <ItemTemplate>
                <table cellpadding="0" cellspacing="0" width="95%" class="message-details">
                    <tr>
                        <td style="width: 0%; text-align: center;">
                            <Mono:Gravatar ID="ctlGravatar" runat="server" Size="100" Email='<%# Eval("AspnetUser.Email") %>'
                                UserName='<%# Eval("AspnetUser.UserName") %>' /><br />
                            <strong>
                                <asp:Literal ID="lblAuthor" runat="server" Text='<%# Eval("AspnetUser.UserName") %>'></asp:Literal></strong>
                            <div class="date">
                                <asp:Literal ID="lblDate" runat="server" Text='<%# Eval("DateCreated") %>'></asp:Literal></div>
                        </td>
                        <td style="width: 100%;">
                            <asp:Literal ID="ltlMessage" runat="server" Text='<%# Eval("Body") %>'></asp:Literal>
                            <MonoX:FileGallery ID="ctlFileGalleryItem" runat="server" ParentEntityType="Message"
                                ParentEntityId='<%# Eval("Id") %>' Visible='<%# AllowFileUpload %>' ViewFilePageId='<%# ViewFilePageId %>'
                                ViewFilePageUrl='<%# ViewFilePageUrl %>' UsePrettyPhoto='<%# this.UsePrettyPhoto %>'
                                Width="100%" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <ItemSeparatorTemplate>
                <div style="width: 94%; clear: both; border-bottom: 1px dashed silver; padding-top: 2px;
                    padding-bottom: 2px; height: 5px; margin: 10px auto;">
                </div>
            </ItemSeparatorTemplate>
        </asp:ListView>
    </div>
    <div style="padding-left: 15px;">
        <Mono:Pager runat="server" ID="pager" PageSize="20" NumericButtonCount="5" AllowCustomPaging="true"
            AutoPaging="false">
            <pagertemplate></pagertemplate>
        </Mono:Pager>
    </div>
    <asp:Panel runat="server" ID="pnlContainer">
        <div class="input-form reply-message">
            <dl>
                <dd>
                    <asp:Label ID="lblMessage" runat="server" AssociatedControlID="txtMessage"></asp:Label>
                    <asp:TextBox ID="txtMessage" Rows="8" TextMode="MultiLine" runat="server"></asp:TextBox>
                </dd>
                <asp:Panel runat="server" ID="pnlUpload" Style="border: none;">
                    <dd>
                        <asp:Label ID="lblUpload" AssociatedControlID="ctlUpload" runat="server"></asp:Label>
                        <MonoX:SilverlightUpload runat="server" ID="ctlUpload" Width="100%" EnableFileGallery="false" />
                        <br />
                    </dd>
                    <dd>
                        <asp:Label ID="lblFileGallery" AssociatedControlID="ctlFileGallery" runat="server">&nbsp;</asp:Label>
                        <MonoX:FileGallery ID="ctlFileGallery" runat="server" ParentEntityType="Message"
                            CssClass="rightLabel" />
                    </dd>
                </asp:Panel>
                <asp:PlaceHolder ID="plhSendMail" runat="server">
                    <dd class="checkbox-messaging">
                        <asp:CheckBox BorderStyle="None" BorderWidth="0px" ID="chkSendMail" runat="server"
                            TextAlign="Right" Checked="true"></asp:CheckBox>
                        <%= SocialNetworkingResources.Messaging_MessageDetails_SendMailCopy %>
                    </dd>
                </asp:PlaceHolder>
            </dl>
            <!--CLIPFLAIR--><MonoX:StyledButton ID="btnSend" runat="server" CausesValidation="false" CssClass="CssFormButton styled-button-clipflair_green"
                OnClick="btnSend_Click" />
            <MonoX:StyledButton ID="btnCancel" runat="server" CausesValidation="false" CssClass="CssFormButton"
                OnClick="btnCancel_Click" />
        </div>
        <asp:Label runat="server" ForeColor="Red" ID="lblWarning"></asp:Label>
    </asp:Panel>
</div>
