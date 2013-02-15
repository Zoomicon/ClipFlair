<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiscussionTopics.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.DiscussionTopics" %>
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager"
    TagPrefix="mono" %>
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionMessages.ascx"
    TagPrefix="MonoX" TagName="DiscussionMessages" %>
<%@ Register Src="~/MonoX/controls/TagTextBox.ascx" TagPrefix="mono" TagName="TagTextBox" %>


<asp:UpdatePanel ID="upNewTopic" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <h1>
            <asp:Literal ID="labTitle" runat="server"></asp:Literal>
        </h1>
        <div class="message-sep-line">
            <asp:HyperLink ID="lnkBack" runat="server" CssClass="discussion-link back-link"></asp:HyperLink>
            <asp:PlaceHolder ID="panNewTopic" runat="server">
                <div class="float-left">
                    <asp:UpdatePanel ID="upSub" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:LinkButton ID="lnkSubscribe" runat="server" CssClass="discussion-link"></asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="discussion-container-action">
                    <ul class="first">
                        <li>
                            <asp:HyperLink ID="lnkOptions" runat="server" CssClass="discussion-link"></asp:HyperLink>
                            <ul class="level0">
                                <li id="pnlClose" runat="server"><asp:LinkButton ID="lnkClose" runat="server"></asp:LinkButton></li>
                                <li id="pnlDeleteUnApproved" runat="server"><asp:LinkButton ID="lnkDeleteUnApproved" runat="server"></asp:LinkButton></li>
                                <li id="pnlDeleteSpam" runat="server"><asp:LinkButton ID="lnkDeleteSpam" runat="server"></asp:LinkButton></li>
                                <li><asp:HyperLink ID="lnkSortNewTopicsOnTop" runat="server"></asp:HyperLink></li>
                                <li><asp:HyperLink ID="lnkSortUpdatedTopicsOnTop" runat="server"></asp:HyperLink></li>
                                <li><asp:HyperLink ID="lnkSortTopicsAlphabetically" runat="server"></asp:HyperLink></li>
                                <li><asp:HyperLink ID="lnkSortHotTopicsOnTop" runat="server"></asp:HyperLink></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div class="float-left">
                    <asp:UpdateProgress ID="upTop" runat="server" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <img src='<%= BaseSiteFullUrl %>/App_Themes/<%= Page.Theme %>/img/loading.gif' alt="Progress ..."
                                width="20px" class="progress" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <MonoX:StyledButton ID="btnNewTopic" runat="server" CssClass="discussion-button" />
            </asp:PlaceHolder>
        </div>        
        <asp:PlaceHolder ID="plhNewTopic" runat="server" Visible="false">
            <div class="discussion" style="overflow: hidden; clear: both;">
                <div class="input-form discussion-form">
                    <dl>
                        <dd>
                             <asp:ValidationSummary ID="summary" runat="server" DisplayMode="List" ShowSummary="true" />
                        </dd>
                        <dd>
                            <asp:Label ID="lblTopicTitle" runat="server" AssociatedControlID="txtTopicTitle" CssClass="label"></asp:Label>
                            <asp:TextBox ID="txtTopicTitle" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vldRequiredTopicTitle" runat="server" ControlToValidate="txtTopicTitle" Text="!" CssClass="validator ValidatorAdapter" Display="Dynamic" />
                        </dd>
                        <dd class="message-padding-override">
                            <MonoX:DiscussionMessages ID="discussionMessages" runat="server" ShowHeader="false">
                            </MonoX:DiscussionMessages>
                        </dd>
                        <dd>
                            <mono:TagTextBox id="tags" runat="server"></mono:TagTextBox>
                        </dd>
                        <dd>
                            <MonoX:StyledButton ID="btnSaveNewTopic" runat="server" CommandName="Save" CssClass="discussion-styled-button" />
                            <MonoX:StyledButton ID="btnCancelNewTopic" runat="server" CausesValidation="false" CssClass="discussion-styled-button" />
                        </dd>
                    </dl>
                </div>
            </div>
        </asp:PlaceHolder>
        <asp:ListView ID="lvDT" runat="server">
            <LayoutTemplate>
                <div class="discussion board-container topic-container">
                    <div class="board topic">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </table>
                    </div>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>           
            <EmptyDataTemplate>
                <div class="empty-discussion"><%= MonoSoftware.MonoX.Resources.SocialNetworkingResources.Discussion_Topic_Empty %></div>
            </EmptyDataTemplate>
        </asp:ListView>
        <div style="clear: both">
            <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true"
                AutoPaging="false">
                <PagerTemplate>
                </PagerTemplate>
            </mono:Pager>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

