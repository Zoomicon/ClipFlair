<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.AdminHeader" Codebehind="AdminHeader.ascx.cs" %>
<%@ Register Src="~/MonoX/controls/LocaleChanger.ascx" TagPrefix="MonoX" TagName="LocaleChanger" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<div class="container">
    <div class="hed"><asp:HyperLink CssClass="logo" runat="server" ID="HyperLink1" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.Default_aspx) %>'><asp:Image runat="server" ImageUrl='<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.All.DefaultAdmin.img.logo_jpg %>' ID="imgLogo" AlternateText='<%$ Code: AdminResources.AdminHeader_imgLogo_AlternateText %>' /></asp:HyperLink><!-- hed_control_start -->
    <table cellpadding="0" cellspacing="0" class="hed_control">
        <tr>
            <td class="left_corner"></td>
            <td class="middle_section">
                <p class="language"><%= AdminResources.AdminHeader_LanguageText %>:</p>
                <div class="form">
                  <MonoX:LocaleChanger ID="ctlLocaleChanger" runat="server" CssClass="select_field" />
                </div>
                <div class="loged_in" ><asp:LoginName ID="loginName1" runat="server" FormatString='<%$ Code: AdminResources.AdminHeader_loginName_FormatString %>' ToolTip='<%$ Code: AdminResources.AdminHeader_loginName_Tooltip %>' /></div>                
                <div class="site_home" ><asp:HyperLink runat="server" ID="lnkSiteHome" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.Default_aspx) %>' Text='<%$ Code: AdminResources.AdminHeader_lnkSiteHome %>'></asp:HyperLink></div>
                <div class="loged_out" ><asp:LoginStatus ID="loginStatus1" runat="server" /></div>
            </td>
            <td class="right_corner"></td>
        </tr>
    </table>
        <div class="navigation" style="width: 100%;">
            <ul>
                <li><asp:HyperLink ID="lnkAdminHome" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.Default_aspx) %>'><%= AdminResources.AdminHeader_lnkAdminHome %></asp:HyperLink></li>
                <li><asp:HyperLink ID="lnkFiles" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.FileAdmin_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.FileAdmin_aspx) %>'><%= AdminResources.AdminHeader_lnkFiles %></asp:HyperLink></li>
                <li><asp:HyperLink ID="lnkPages" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.PageAdmin_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.PageAdmin_aspx) %>'><%= AdminResources.AdminHeader_lnkPages %></asp:HyperLink></li>
                <li><asp:HyperLink ID="lnkUsers" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.UserManager_aspx) %>'><%= AdminResources.AdminHeader_lnkUsers %></asp:HyperLink></li>
                <li><asp:HyperLink ID="lnkRoles" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.RoleManager_aspx) %>'><%= AdminResources.AdminHeader_lnkRoles %></asp:HyperLink></li>
                <li>
                    <asp:HyperLink ID="lnkNews" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsManager_aspx) %>' ><%= AdminResources.AdminHeader_lnkNews %></asp:HyperLink>
                    <ul style="width: 100px;">
                        <li>
                            <asp:HyperLink ID="lnkQuickNews" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsQuickPost_aspx) %>' ><%= AdminResources.AdminHeader_lnkNewsQuickPost %></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="lnkNewsManager" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsManager_aspx) %>' ><%= AdminResources.AdminHeader_lnkNews %></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkNewsCategoryAdmin" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsCategoryManager_aspx) %>' ><%= AdminResources.AdminHeader_lnkNewsCategoryAdmin %></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkNewsPublisherAdmin" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsPublisherManager_aspx) %>' ><%= AdminResources.AdminHeader_lnkNewsPublisherAdmin %></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkNewsMetaKeyWords" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsIgnoredMetaKeywordsManager_aspx) %>' ><%= AdminResources.AdminHeader_lnkNewsMetaKeyWords%></asp:HyperLink>
                        </li>
                    </ul>
                </li>
                <li><asp:HyperLink ID="lnkAds" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.AdManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.AdManager_aspx) %>'><%= AdminResources.AdminHeader_lnkAds %></asp:HyperLink></li>
                <li><asp:HyperLink ID="lnkPolls" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.PollManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.PollManager_aspx) %>'><%= AdminResources.AdminHeader_lnkPolls %></asp:HyperLink></li>
                <li><asp:HyperLink ID="lnlLists" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.ListManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.ListManager_aspx) %>'><%= AdminResources.AdminHeader_lnkLists %></asp:HyperLink></li>
                <li><asp:HyperLink ID="lnkNewsletters" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsletterManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsletterManager_aspx) %>'><%= AdminResources.AdminHeader_lnkNewsletters %></asp:HyperLink></li>
                <li>
                    <asp:HyperLink ID="lnkBlog" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.BlogManager_aspx) %>' ><%= AdminResources.AdminHeader_lnkBlog %></asp:HyperLink>
                    <ul style="width: 100px;">
                        <li>
                            <asp:HyperLink ID="lnkBlogSettings" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.BlogSettingsManager_aspx) %>' ><%= AdminResources.AdminHeader_lnkBlogSettings %></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="lnkBlogPosts" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.BlogManager_aspx) %>' ><%= AdminResources.AdminHeader_lnkBlogPosts %></asp:HyperLink>
                        </li>
                    </ul>
                </li>
                <li><asp:HyperLink ID="lnkAmazonS3" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.FileAdminAmazonS3_aspx) %>'><%= AdminResources.AdminHeader_lnkAmazonS3 %></asp:HyperLink></li>
                <li>
                    <asp:HyperLink ID="lnkSocial" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupManager_aspx) %>'><%= AdminResources.AdminHeader_lnkGroups%></asp:HyperLink>
                    <ul style="width: 100px;">
                        <li>
                            <asp:HyperLink ID="lnkGroupCat" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupCategoryManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupCategoryManager_aspx) %>'><%= AdminResources.AdminHeader_lnkGroupCat %></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="lnkGroups" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupManager_aspx) %>'><%= AdminResources.AdminHeader_lnkGroups %></asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="lnkGroupMembers" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupMembershipManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupMembershipManager_aspx) %>'><%= AdminResources.AdminHeader_lnkGroupMembers %></asp:HyperLink>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
   </div>
</div>