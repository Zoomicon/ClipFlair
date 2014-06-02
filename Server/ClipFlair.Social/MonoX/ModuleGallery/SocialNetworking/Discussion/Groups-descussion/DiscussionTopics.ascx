<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="DiscussionTopics.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.DiscussionTopics" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionMessages.ascx" TagPrefix="MonoX" TagName="DiscussionMessages" %>
<%@ Register Src="~/MonoX/controls/TagTextBox.ascx" TagPrefix="mono" TagName="TagTextBox" %>

<!--CLIPFLAIR-->
<div class="discussion">
    <asp:UpdatePanel ID="upNewTopic" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:PlaceHolder ID="plhHeader" runat="server">
            <div class="discussion-top-section clearfix">
                <h1><asp:Literal ID="labTitle" runat="server"></asp:Literal></h1>
                <div class="main-options">
                    <div class="inner small-btn">
                        <asp:HyperLink ID="lnkBack" runat="server" CssClass="styled-button back-btn float-left"></asp:HyperLink>
                        <asp:PlaceHolder ID="panNewTopic" runat="server">
                            <ul class="first">
                                <li>
                                    <asp:HyperLink ID="lnkOptions" runat="server" CssClass="options-link styled-button options-btn float-left"></asp:HyperLink>
                                    <ul class="level0">
                                        <li id="pnlClose" runat="server"><asp:LinkButton ID="lnkClose" runat="server"></asp:LinkButton></li>
                                        <li id="pnlEditBoard" runat="server"><asp:LinkButton ID="lnkEditBoard" runat="server"></asp:LinkButton></li>
                                        <li id="pnlDeleteUnApproved" runat="server"><asp:LinkButton ID="lnkDeleteUnApproved" runat="server"></asp:LinkButton></li>
                                        <li id="pnlDeleteSpam" runat="server"><asp:LinkButton ID="lnkDeleteSpam" runat="server"></asp:LinkButton></li>
                                        <li><asp:HyperLink ID="lnkSortNewTopicsOnTop" runat="server"></asp:HyperLink></li>
                                        <li><asp:HyperLink ID="lnkSortUpdatedTopicsOnTop" runat="server"></asp:HyperLink></li>
                                        <li><asp:HyperLink ID="lnkSortTopicsAlphabetically" runat="server"></asp:HyperLink></li>
                                        <li><asp:HyperLink ID="lnkSortHotTopicsOnTop" runat="server"></asp:HyperLink></li>
                                    </ul>
                                </li>
                            </ul>
                            <div class="float-left">
                                <asp:UpdatePanel ID="upSub" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="lnkSubscribe" runat="server" CssClass="styled-button main-button subscribe-btn"></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>                       
                            <MonoX:StyledButton ID="btnNewTopic" runat="server" CssClass="main-button add-btn float-left" />                           
                            <div class="float-right">
                                <asp:UpdateProgress ID="upTop" runat="server" DisplayAfter="0" DynamicLayout="true">
                                    <ProgressTemplate>
                                        <img src='<%= BaseSiteFullUrl %>/App_Themes/<%= Page.Theme %>/img/loading.gif' alt="Progress ..." width="40" class="progress" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>                        
                        </asp:PlaceHolder>
                    </div>    
                </div>
            </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plhNewTopic" runat="server" Visible="false">
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
                    <dd class="button-holder">
                        <!--CLIPFLAIR-->
                        <MonoX:StyledButton ID="btnSaveNewTopic" runat="server" CommandName="Save" CssClass="main-button submit-btn float-left" />
                        <MonoX:StyledButton ID="btnCancelNewTopic" runat="server" CausesValidation="false" CssClass="cancel-btn float-left" />
                    </dd>
                </dl>
            </div>
            </asp:PlaceHolder>
            <asp:ListView ID="lvDT" runat="server">
                <LayoutTemplate>
                    <div>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
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
</div>