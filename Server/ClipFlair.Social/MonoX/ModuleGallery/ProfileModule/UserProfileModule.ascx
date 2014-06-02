<%@ Control
    Language="C#"
    AutoEventWireup="true"
    Inherits="MonoSoftware.MonoX.ModuleGallery.UserProfileModule"
    CodeBehind="UserProfileModule.ascx.cs" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/MonoX/ModuleGallery/ProfileModule/UserAvatar.ascx" TagPrefix="monox" TagName="UserAvatar" %>
<%@ Register Src="~/MonoX/ModuleGallery/ProfileModule/UserProfile.ascx" TagPrefix="monox" TagName="UserProfile" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.PrivacyManager" TagPrefix="MonoXPrivacyManager" %>

<asp:PlaceHolder ID="mainHolder" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="user-profile clearfix">
                <asp:Panel id="rowAdmin" runat="server" class="choose-a-user">
                    <asp:Label ID="lblUser" runat="server" CssClass="user-label"></asp:Label>
                    <telerik:RadComboBox ID="ddlUsers" runat="server" AllowCustomText="false" EnableLoadOnDemand="True"
                        MarkFirstMatch="false" ShowDropDownOnTextboxClick="false" ShowToggleImage="true" 
                        ShowWhileLoading="true" AutoPostBack="true" Height="150px" CausesValidation="false">
                    </telerik:RadComboBox>
                </asp:Panel>
                <div class="top-section clearfix">
                    <asp:Panel CssClass="avatar-holder" id="rowSideBar" runat="server" >
                        <monox:UserAvatar ID="userAvatar" runat="server" />
                        <asp:PlaceHolder ID="plhSideBar" runat="server"></asp:PlaceHolder>
                    </asp:Panel>
                    <asp:Panel id="rowStatus" runat="server" CssClass="details">
                        <h1><asp:Literal ID="labName" runat="server"></asp:Literal></h1>
                        <div class="status-text"><asp:Literal ID="labMyStatus" runat="server"></asp:Literal></div>
                        <div id="panStatusEdit" runat="server" class="clearfix">
                            <asp:LinkButton ID="btnSetMyStatus" runat="server" CssClass="styled-button float-right"></asp:LinkButton>
                            <MonoXPrivacyManager:PrivacyEditor id="privacyEditor" runat="server" CssClass="privacy float-right"></MonoXPrivacyManager:PrivacyEditor>
                            <div class="status-composer">
                                <asp:TextBox runat="server" ID="txtMyStatus" CssClass="jq_swap_value"></asp:TextBox>
                                <span class="delete-icon reset-status"><asp:LinkButton ID="lnkRemoveStatus" runat="server" CausesValidation="false"></asp:LinkButton></span>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <asp:Panel id="rowMainContent" runat="server" CssClass="bottom-section">
                    <span id="lblTitle" runat="server" style="display: none;"></span>
                    <% if (rowSwitch.Visible)
                        { %>
                        <div class="option-holder clearfix">
                            <strong><%= MonoSoftware.MonoX.Resources.UserProfileResources.UserProfileModule_CompleteProfile%></strong>
                            <div class='options <%= InEditMode ? "edit-mode" : "view-mode" %>'>
                                <ul id="rowSwitch" runat="server">
                                    <li class="<%= InEditMode ? "current" : String.Empty %>">
                                        <asp:LinkButton ID="lnkEditProfile" runat="server" CausesValidation="false">
                                            <span id="labEditProfile" runat="server"></span>
                                        </asp:LinkButton>
                                    </li>
                                    <li class="<%= InEditMode ? String.Empty : "current" %>">
                                        <asp:LinkButton ID="lnkViewProfile" runat="server" CausesValidation="false">
                                            <span id="labViewProfile" runat="server"></span>
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                                <div class="progress-load">
                                    <asp:UpdateProgress ID="upTop" runat="server" AssociatedUpdatePanelID="up" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <img src='<%= BaseSiteFullUrl %>/App_Themes/<%= Page.Theme %>/img/loading.gif' alt="Progress ..." width="24" class="progress" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                        </div>
                    <% } %>
                    <monox:UserProfile ID="userProfile" runat="server" />
                    <asp:PlaceHolder ID="plhMain" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="rowFooter" runat="server">
        <asp:PlaceHolder ID="plhFooter" runat="server"></asp:PlaceHolder>
    </div>
</asp:PlaceHolder>
