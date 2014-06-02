<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="DiscussionMessages.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.DiscussionMessages" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register TagPrefix="MonoX" TagName="SilverlightUpload" Src="~/MonoX/ModuleGallery/SilverlightUploadModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FileGallery" Src="~/MonoX/ModuleGallery/SocialNetworking/FileGallery.ascx" %>
<%@ Register Src="~/MonoX/controls/CustomRadEditor.ascx" TagPrefix="mono" TagName="CustomRadEditor" %>
<%@ Register Src="~/MonoX/controls/SyntaxHighlighter/TextBoxSyntaxHighlighter.ascx" TagPrefix="mono" TagName="TextBoxSyntaxHighlighter" %>
<%@ Register tagPrefix="telerik" namespace="Telerik.Web.UI" assembly="Telerik.Web.UI" %>
<%@ Register Src="~/MonoX/ModuleGallery/RelatedContent.ascx" TagPrefix="MonoX" TagName="RelatedContent" %>

<telerik:radwindowmanager id="rwm" runat="server"></telerik:radwindowmanager>
            
<asp:UpdatePanel ID="pnlMain" runat="server" UpdateMode="Always"> 
    <ContentTemplate>
        <asp:PlaceHolder ID="panHeader" runat="server">
            <!--CLIPFLAIR-->
            <div class="discussion-top-section clearfix">
                <h1>
                    <asp:Label ID="labBoardTitle" runat="server" CssClass="board-title"></asp:Label>
                    <asp:Literal ID="labTopicTitle" runat="server"></asp:Literal>
                    <asp:PlaceHolder ID="plhTopicInfo" runat="server">
                        <strong class="message-info">
                            <asp:Label ID="labStats" runat="server"></asp:Label>,
                            <asp:Label ID="labStatsPosts" runat="server"></asp:Label>,
                            <asp:Label ID="labDate" runat="server"></asp:Label> -
                            <asp:HyperLink ID="lnkAuthor" runat="server"></asp:HyperLink>
                        </strong>
                    </asp:PlaceHolder>
                </h1>            
                <div class="main-options">
                    <div class="inner padding-bottom-10">
                        <asp:HyperLink ID="lnkBack" runat="server" CssClass="styled-button back-btn float-left"></asp:HyperLink>
                        <div class="float-left">
                            <asp:UpdatePanel ID="upSub" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkSubscribe" runat="server" CssClass="styled-button main-button subscribe-btn"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <asp:LinkButton ID="lnkClose" runat="server" CssClass="styled-button float-left"></asp:LinkButton>
                        <asp:LinkButton ID="lnkEditTopic" runat="server" CssClass="styled-button main-button edit-btn-b float-left"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDeleteUnApproved" runat="server" CssClass="styled-button delete-btn float-left"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDeleteSpam" runat="server" CssClass="styled-button delete-btn float-left"></asp:LinkButton>                
                        <asp:UpdateProgress ID="upTop" runat="server" DisplayAfter="0" DynamicLayout="true">
                            <ProgressTemplate>
                                <img src='<%= BaseSiteFullUrl %>/App_Themes/<%= Page.Theme %>/img/loading.gif' alt="Progress ..."
                                    width="40" class="progress" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <div class="float-right share"><asp:Panel ID="panTellAFriend" runat="server"></asp:Panel></div>
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>        
        <asp:PlaceHolder ID="plhMessages" runat="server">
            <MonoX:RelatedContent id="relatedContent" runat="server" CssClass="discussion"></MonoX:RelatedContent>
            <div class="discussion">
                <asp:ListView ID="lvM" runat="server">
                    <LayoutTemplate>
                        <div>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </div>
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
        <div class="discussion-form input-form">
            <dl>
                <dd>
                    <asp:PlaceHolder ID="plhSummary" runat="server">
                        <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ShowSummary="true" CssClass="validation-summary" />
                    </asp:PlaceHolder>
                </dd>
                <dd class="h2 label">
                    <asp:Literal ID="litReply" runat="server"></asp:Literal>
                    <mono:TextBoxSyntaxHighlighter ID="textBoxSyntaxHighlighter" runat="server" />
                </dd>
                <dd class="editor-height">
                    <asp:TextBox ID="txtReply" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
                    <mono:CustomRadEditor Width="100%" ID="radReply" CssClass="custom-rad-editor" EditModes="Design" StripFormattingOptions="AllExceptNewLines" runat="server" ToolBarMode="ShowOnFocus" ContentAreaMode="Div" ></mono:CustomRadEditor>
                </dd>
                <dd class="legend-no-margin-small margins">
                    <asp:Label ID="labApprovalNote" runat="server" CssClass="approval-font" ></asp:Label>
                </dd>
                <dd class="my_checkbox margins float-none">
                    <asp:UpdatePanel ID="upS" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="panSubscribe" runat="server">
                                <asp:CheckBox ID="chkSubscribe" runat="server" Checked="true" />
                                <asp:Label ID="labCheck" runat="server" AssociatedControlID="chkSubscribe"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="labSubscribe" runat="server" CssClass="my_label"></asp:Label>
                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </dd>
                <dd class="margins">
                    <table cellpadding="0" cellspacing="0" width="60%" class="snNoteTable" style="">
                        <tr class="snUploadRow float-left">
                            <td>
                                <MonoX:SilverlightUpload runat="server" ID="ctlUpload" EnableFileGallery="false" />
                                <MonoX:FileGallery CssClass="attached-files file-gallery" ID="ctlFileGallery" runat="server" EnableFileEdit="false" UsePrettyPhoto="true"  />
                            </td>
                        </tr>
                    </table>
                    <div style="float:right;">
                        <!--CLIPFLAIR-->
                        <MonoX:StyledButton ID="btnPost" runat="server" CssClass="main-button submit-btn float-left" />
                        <MonoX:StyledButton ID="btnCancel" runat="server" Visible="false" CausesValidation="false" CssClass="cancel-btn float-left" />
                    </div>
                </dd>              
            </dl>
        </div>
        </asp:PlaceHolder>
    </ContentTemplate>
</asp:UpdatePanel>
