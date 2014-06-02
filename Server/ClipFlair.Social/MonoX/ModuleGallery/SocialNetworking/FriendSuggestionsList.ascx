<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="FriendSuggestionsList.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.FriendSuggestionsList" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="MonoXControls" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %> 

<asp:UpdatePanel ID="upFriendList" runat="server">
    <ContentTemplate>
        <div class="clearfix">
            <asp:PlaceHolder runat="server" ID="plhFriendSuggestions">
                <asp:Panel ID="pnlSearchBox" runat="server">
                    <div class="search-box">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtweSearch" runat="server" TargetControlID="txtSearch" 
                            WatermarkText="<%$ Code:MonoSoftware.MonoX.Resources.SocialNetworkingResources.FriendSuggestionList_WatermarkSearch_Text%>" WatermarkCssClass="focus" />
                        <asp:LinkButton ID="btnSearchPeople" runat="server" CssClass="search-button"></asp:LinkButton>
                        <br clear="all" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlContainer" runat="server">
                    <asp:ListView ID="lvItems" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate></ItemTemplate>
                    </asp:ListView>
                </asp:Panel>
                <asp:Label runat="server" ID="lblEmpty" CssClass="empty-list"></asp:Label>
                <div style="clear:both">
                    <mono:Pager runat="server" ID="pager" PageSize="9" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
                        <PagerTemplate></PagerTemplate>
                    </mono:Pager>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plhSendMessage">
                <div class="input-form">
                    <dl>
                        <dd>
                            <asp:Label runat="server" Style="float:left;margin-bottom:20px;" ID="lblPersonalMessage"></asp:Label>
                            <asp:TextBox runat="server" ID="txtRequestMessage" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox>
                        </dd>
                        <!--!!!CLIPFLAIR-->
                        <div class="button-holder">
                            <div class="small-btn">
                                <MonoX:StyledButton runat="server" CssClass="cancel-btn float-right" ID="btnCancelRequest" />
                                <MonoX:StyledButton runat="server" CssClass="main-button submit-btn float-right" ID="btnSendRequest" />
                            </div>
                        </div>
                        <!--/!!!CLIPFLAIR-->
                    </dl>
                </div>
            </asp:PlaceHolder>
            <div style="clear: both; display: none;">
                <asp:Label runat="server" ID="lblStatusMessage"></asp:Label>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>