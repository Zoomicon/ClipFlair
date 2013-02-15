<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiscussionMessages.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.DiscussionMessages" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager"
    TagPrefix="mono" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<%@ Register Src="~/MonoX/controls/CustomRadEditor.ascx" TagPrefix="mono" TagName="CustomRadEditor" %>
<%@ Register Src="~/MonoX/controls/SyntaxHighlighter/TextBoxSyntaxHighlighter.ascx"
    TagPrefix="mono" TagName="TextBoxSyntaxHighlighter" %>
<%@ Register tagPrefix="telerik" namespace="Telerik.Web.UI" assembly="Telerik.Web.UI" %>
<%@ Register Src="~/MonoX/ModuleGallery/RelatedContent.ascx" TagPrefix="MonoX" TagName="RelatedContent" %>

<telerik:radwindowmanager id="rwm" runat="server">
</telerik:radwindowmanager>
            
<asp:UpdatePanel ID="pnlMain" runat="server" UpdateMode="Conditional"> 
    <ContentTemplate>
        <asp:PlaceHolder ID="panHeader" runat="server">
            <h1 class="discussion-title">
                <asp:Label ID="labBoardTitle" runat="server"></asp:Label>
                <asp:Literal ID="labTopicTitle" runat="server"></asp:Literal>
            </h1>            
            <div class="message-info">
                <span class="views-posts">
                    <asp:Label ID="labStats" runat="server"></asp:Label>&nbsp;,&nbsp;
                    <asp:Label ID="labStatsPosts" runat="server"></asp:Label>
                </span>&nbsp;                
                <asp:Label ID="labDate" runat="server"></asp:Label>&nbsp;
                <asp:HyperLink ID="lnkAuthor" runat="server"></asp:HyperLink>&nbsp;
            </div>
            <div class="message-sep-line" style="overflow: hidden;">
                <asp:HyperLink ID="lnkBack" runat="server" class="discussion-link back-link"></asp:HyperLink>
                <asp:UpdatePanel ID="upSub" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:LinkButton ID="lnkSubscribe" runat="server" CssClass="discussion-link"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:LinkButton ID="lnkClose" runat="server" class="discussion-link"></asp:LinkButton>
                <asp:LinkButton ID="lnkDeleteUnApproved" runat="server" CssClass="discussion-link"></asp:LinkButton>
                <asp:LinkButton ID="lnkDeleteSpam" runat="server" CssClass="discussion-link"></asp:LinkButton>                
                <div class="share-icons"><asp:Panel ID="panTellAFriend" runat="server"></asp:Panel></div>
                <asp:UpdateProgress ID="upTop" runat="server" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <img src='<%= BaseSiteFullUrl %>/App_Themes/<%= Page.Theme %>/img/loading.gif' alt="Progress ..."
                            width="20px" class="progress" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </asp:PlaceHolder>        
        <asp:PlaceHolder ID="plhMessages" runat="server">
            <MonoX:RelatedContent id="relatedContent" runat="server" CssClass="discussion"></MonoX:RelatedContent>
            <div class="discussion">
                <asp:ListView ID="lvM" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <div class="empty-discussion"><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.Discussion_Posts_Empty %></div>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div style="clear: both">
                    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true"
                        AutoPaging="false">
                        <PagerTemplate>
                        </PagerTemplate>
                    </mono:Pager>
                </div>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhReply" runat="server">
            <div class="discussion">
                <div class="input-form discussion-form">
                    <dl>
                        <dd>
                             <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ShowSummary="true" />
                        </dd>
                        <dd>
                            <strong class="topic-label-font-color-telerik">
                                <asp:Literal ID="litReply" runat="server"></asp:Literal></strong><br />
                            <mono:TextBoxSyntaxHighlighter ID="textBoxSyntaxHighlighter" runat="server" />
                        </dd>
                        <dd>
                            <asp:TextBox ID="txtReply" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
                            <div style="width: 95%;">
                                <mono:CustomRadEditor Width="100%" ID="radReply" EditModes="Design" StripFormattingOptions="AllExceptNewLines" runat="server"
                                    ToolBarMode="ShowOnFocus"  ContentAreaMode="Iframe" ></mono:CustomRadEditor>
                            </div>                            
                        </dd>
                        <dd>
                            <asp:Label ID="labApprovalNote" runat="server" CssClass="approval-font" ></asp:Label>
                        </dd>
                        <dd>
                            <asp:UpdatePanel ID="upS" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="panSubscribe" runat="server">
                                        <asp:CheckBox ID="chkSubscribe" runat="server" Checked="true" />
                                        <asp:Label ID="labSubscribe" runat="server" AssociatedControlID="chkSubscribe"></asp:Label>
                                    </asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </dd>
                        <dd>
                            <table cellpadding="0" cellspacing="0" width="100%" class="snNoteTable">
                                <tr class="snUploadRow">
                                    <td>
                                        <MonoX:SilverlightUpload runat="server" ID="ctlUpload" EnableFileGallery="false"  />
                                        <MonoX:FileGallery ID="ctlFileGallery" runat="server" EnableFileEdit="false" UsePrettyPhoto="true"  />
                                    </td>
                                </tr>
                            </table>
                        </dd>
                        <dd>
                            <MonoX:StyledButton ID="btnPost" runat="server" CssClass="discussion-styled-button" />
                            <MonoX:StyledButton ID="btnCancel" runat="server" Visible="false" CausesValidation="false" CssClass="discussion-styled-button" />
                        </dd>
                    </dl>
                </div>
            </div>
        </asp:PlaceHolder>
    </ContentTemplate>
</asp:UpdatePanel>
