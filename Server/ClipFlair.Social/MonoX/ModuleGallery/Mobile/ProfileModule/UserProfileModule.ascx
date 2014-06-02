﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfileModule.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.ProfileModule.UserProfileModule" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/MonoX/ModuleGallery/Mobile/ProfileModule/UserAvatar.ascx" TagPrefix="monox"
    TagName="UserAvatar" %>
<%@ Register Src="~/MonoX/ModuleGallery/Mobile/ProfileModule/UserProfile.ascx" TagPrefix="monox"
    TagName="UserProfile" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.PrivacyManager" TagPrefix="MonoXPrivacyManager" %> 
    
<asp:PlaceHolder ID="mainHolder" runat="server">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="user-profile">
                <%--User Profile Start --%>
                <asp:Panel id="rowAdmin" runat="server" class="choose-a-user">
                    <asp:Label ID="lblUser" runat="server" CssClass="user-label"></asp:Label>
                    <telerik:RadComboBox ID="ddlUsers" runat="server" AllowCustomText="false" EnableLoadOnDemand="True"
                        MarkFirstMatch="false" ShowDropDownOnTextboxClick="false" ShowToggleImage="true" 
                        ShowWhileLoading="true" AutoPostBack="true" Height="150px" CausesValidation="false">
                    </telerik:RadComboBox>
                </asp:Panel>
                <div class="two-column">
                    <asp:Panel CssClass="status-gravatar side-bar" id="rowSideBar" runat="server" >
                        <monox:UserAvatar ID="userAvatar" runat="server" EnableAvatarCropping="false" />
                        <asp:PlaceHolder ID="plhSideBar" runat="server"></asp:PlaceHolder>
                    </asp:Panel>
                    <asp:Panel id="rowStatus" runat="server" CssClass="profile-status">
                        <div class="profile-status-middle">
                            <div class="profile-status-top">
                                <h2><asp:Literal ID="labName" runat="server"></asp:Literal></h2>
                                <p class="profile-status-text"><asp:Literal ID="labMyStatus" runat="server"></asp:Literal></p>
                            </div>
                        </div>
                    </asp:Panel>
                    
                    <div id="panStatusEdit" runat="server" class="profile-status-bottom">
                        <asp:TextBox runat="server" ID="txtMyStatus" CssClass="jq_swap_value"></asp:TextBox>
                        <span class="delete-icon reset-status"><asp:LinkButton ID="lnkRemoveStatus" runat="server" CausesValidation="false"></asp:LinkButton></span>
                        <asp:LinkButton ID="btnSetMyStatus" runat="server" CssClass="profile-submit" data-role="button" data-theme="b"></asp:LinkButton>
                        <MonoXPrivacyManager:PrivacyEditor id="privacyEditor" runat="server"></MonoXPrivacyManager:PrivacyEditor>
                    </div>
                    <div id="panStatusView" runat="server"  class="profile-status-bottom-end">
                    </div>

                    <div class="buttons">
                        <ul class="button" id="rowSwitch" runat="server">
                            <li class="<%= InEditMode ? String.Empty : "current" %>">
                                <asp:LinkButton ID="lnkViewProfile" runat="server" CausesValidation="false" data-role="button" data-theme="b">
                                    <span id="labViewProfile" runat="server"></span></asp:LinkButton></li>
                            <li class="<%= InEditMode ? "current" : String.Empty %>">
                                <asp:LinkButton ID="lnkEditProfile" runat="server" CausesValidation="false" data-role="button" data-theme="b">
                                    <span id="labEditProfile" runat="server"></span></asp:LinkButton></li>
                        </ul>
                    </div>
                </div>
                <asp:Panel style="position: relative; padding: -15px; margin-top: 30px;" id="rowMainContent" runat="server">
                    <span id="lblTitle" runat="server" style="display: none;"></span>
                    <div style="overflow: hidden; overflow: hidden; position: absolute; top: 0px; right: 0px;">
                        <asp:UpdateProgress ID="upTop" runat="server" AssociatedUpdatePanelID="up" DisplayAfter="0"
                            DynamicLayout="true">
                            <ProgressTemplate>
                                <img src='<%= BaseSiteFullUrl %>/App_Themes/<%= Page.Theme %>/img/loading.gif' alt="Progress ..."
                                    width="20" class="progress" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>                        
                    <monox:UserProfile ID="userProfile" runat="server"/>
                    <asp:PlaceHolder ID="plhMain" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </div>
            <%--User Profile End --%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="rowFooter" runat="server">
        <asp:PlaceHolder ID="plhFooter" runat="server"></asp:PlaceHolder>
    </div>
</asp:PlaceHolder>
