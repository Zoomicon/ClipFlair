<%@ Import Namespace="MonoSoftware.MonoX.Utilities" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Page Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Admin.Admin_Default"
    EnableTheming="true" Theme="DefaultAdmin" CodeBehind="Default.aspx.cs" %>

<%@ Register Src="~/MonoX/Admin/controls/AdminHeader.ascx" TagPrefix="mono" TagName="AdminHeader" %>
<%@ Register Src="~/MonoX/Admin/controls/AdminFooter.ascx" TagPrefix="mono" TagName="AdminFooter" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radW" %>
<%@ Register TagPrefix="MonoXControls" Namespace="MonoSoftware.MonoX.Controls" Assembly="MonoX" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonoX Administration</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="AjaxScriptManager" EnablePageMethods="True" runat="server">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>

    <script type="text/javascript">


        function OpenRadWindow(url, width, height, windowName, windowTitle) {
            var oWindow = window.radopen(url, windowName);
            //oWindow.set_modal(true);
            oWindow.SetSize(width, height);
            oWindow.Center();
            oWindow.OnClientPageLoad = function() {
                oWindow.SetTitle(windowTitle);
            }
        }

        function OpenRadWindowMaximized(url, windowName, windowTitle) {
            var oWindow = window.radopen(url, windowName);
            //oWindow.Maximize();
            oWindow.SetSize(1080, 800);
            oWindow.Center();
            oWindow.OnClientPageLoad = function() {
                oWindow.SetTitle(windowTitle);
            }
        }

        function AddPage() {
            OpenRadWindow("PageManagerPropertiesDialog.aspx", 700, 650, "AddPage", '<%= AdminResources.Default_AddPage %>');
        }

        function AddUser() {
            OpenRadWindowMaximized("UserManager.aspx?<%= MonoSoftware.MonoX.UrlParams.EntityId.Name %>=<%= Guid.Empty.ToString() %>", "AddUser", '<%= AdminResources.Default_AddUser %>');
        }

        function AddNews() {
            OpenRadWindowMaximized("NewsQuickPost.aspx?<%= MonoSoftware.MonoX.UrlParams.EntityId.Name %>=<%= Guid.Empty.ToString() %>", "AddNewsArticle", '<%= AdminResources.Default_AddNews %>');
        }

        function AddRole() {
            OpenRadWindowMaximized("RoleManager.aspx?<%= MonoSoftware.MonoX.UrlParams.EntityId.Name %>=<%= Guid.Empty.ToString() %>", "AddRole", '<%= AdminResources.Default_AddRole %>');
        }

        function AddAd() {
            OpenRadWindowMaximized("AdManager.aspx?<%= MonoSoftware.MonoX.UrlParams.EntityId.Name %>=<%= Guid.Empty.ToString() %>", "AddAdd", '<%= AdminResources.Default_AddAd %>');
        }

        function AddPoll() {
            OpenRadWindowMaximized("PollManager.aspx?<%= MonoSoftware.MonoX.UrlParams.EntityId.Name %>=<%= Guid.Empty.ToString() %>", "AddPoll", '<%= AdminResources.Default_AddPoll %>');
        }

        function AddList() {
            OpenRadWindowMaximized("ListManager.aspx?<%= MonoSoftware.MonoX.UrlParams.EntityId.Name %>=<%= Guid.Empty.ToString() %>", "AddList", '<%= AdminResources.Default_AddList %>');
        }

        function AddBlogPost() {
            OpenRadWindowMaximized("BlogManager.aspx?<%= MonoSoftware.MonoX.UrlParams.EntityId.Name %>=<%= Guid.Empty.ToString() %>", "AddBlogPost", '<%= AdminResources.Default_AddBlogPost %>');
        }

        function AddNewsletter() {
            OpenRadWindowMaximized("NewsletterManager.aspx?<%= MonoSoftware.MonoX.UrlParams.EntityId.Name %>=<%= Guid.Empty.ToString() %>", "AddNewsletter", '<%= AdminResources.Default_AddNewsletter %>');
        }
    

    </script>
    
    <div class="masterDiv">
        <radW:RadWindowManager ID="rwmSingleton" runat="server" Skin="Default" ReloadOnShow="true">
        </radW:RadWindowManager>
        <mono:AdminHeader ID="adminHeader" runat="server" />
        <div class="container">
            <div class="contentt">
                <!-- sub_nav_start -->
                <div class="sub_nav">
                    <asp:Label ID="labQuickTasks" runat="server" CssClass="title" Text='<%$ Code: AdminResources.Default_labQuickTasks %>'></asp:Label>
                    <ul>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkAddPage" NavigateUrl="javascript:AddPage();"><%= AdminResources.Default_AddPage %></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkAddUser" NavigateUrl="javascript:AddUser();"><%= AdminResources.Default_AddUser %></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkAddRole" NavigateUrl="javascript:AddRole();"><%= AdminResources.Default_AddRole %></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkAddNews" NavigateUrl="javascript:AddNews();"><%= AdminResources.Default_AddNews %></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkAddAd" NavigateUrl="javascript:AddAd();"><%= AdminResources.Default_AddAd %></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkAddPoll" NavigateUrl="javascript:AddPoll();"><%= AdminResources.Default_AddPoll %></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkAddList" NavigateUrl="javascript:AddList();"><%= AdminResources.Default_AddList %></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkAddNewsletter" NavigateUrl="javascript:AddNewsletter();"><%= AdminResources.Default_AddNewsletter %></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink runat="server" ID="lnkAddBlogPost" NavigateUrl="javascript:AddBlogPost();"><%= AdminResources.Default_AddBlogPost %></asp:HyperLink></li>
                    </ul>
                    <span class="title" style="visibility: hidden;">Separator</span><br />
                    <asp:Label runat="server" CssClass="title" Text='<%$ Code: AdminResources.Default_labDocumentation %>'></asp:Label>
                    <ul>
                        <li>
                            <asp:HyperLink runat="server" NavigateUrl='http://www.mono-software.com/Downloads/#MonoX' Target="_blank" Text='<%$ Code: AdminResources.Default_lnkMonoXDocumentation %>'></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink runat="server" NavigateUrl='http://www.mono-software.com/Downloads/#MonoX' Target="_blank" Text='<%$ Code: AdminResources.Default_lnkMonoXApi %>'></asp:HyperLink></li>
                    </ul>
                    <span class="title" style="visibility: hidden;">Separator</span><br />
                    <asp:Label ID="labOtherTasks" runat="server" CssClass="title" Text='<%$ Code: AdminResources.Default_labOtherTasks %>'></asp:Label>
                    <ul>
                        <li>
                            <asp:HyperLink ID="lnkLanguageAdmin" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.LanguageManager_aspx) %>' Text='<%$ Code: AdminResources.Default_lnkLanguageAdmin %>'></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="lnkPortalLocalization" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.PortalLocalization_aspx) %>' Text='<%$ Code: AdminResources.Default_lnkPortalLocalization %>'></asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="lnkPortalSettings" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.PortalSettings_aspx) %>' Text='<%$ Code: AdminResources.Default_lnkPortalSettings %>'></asp:HyperLink></li>
                    </ul>
                </div>
                <!-- sub_nav_end -->
                <!-- boxes_start -->
                <div class="boxes">
                    <asp:Panel runat="server" ID="pnlFiles" CssClass="box_files">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.FileAdmin_aspx) %>' Text='<%$ Code: AdminResources.Default_lnkFiles %>' ID="lnkFiles"></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.FileAdmin_aspx) %>' ID="lnkFiles2"><%= AdminResources.Default_Files %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlPages" CssClass="box_pages">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.PageAdmin_aspx) %>' ID="lnkPages" Text='<%$ Code: AdminResources.Default_lnkPages %>'></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.PageAdmin_aspx) %>' ID="lnkPages2"><%= AdminResources.Default_Pages %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlUsers" CssClass="box_users">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.UserManager_aspx) %>' ID="lnkUsers" Text='<%$ Code: AdminResources.Default_lnkUsers %>'></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.UserManager_aspx) %>' ID="lnkUsers2"><%= AdminResources.Default_Users %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlRoles" CssClass="box_roles">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.RoleManager_aspx) %>' ID="lnkRoles" Text='<%$ Code: AdminResources.Default_lnkRoles %>'></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.RoleManager_aspx) %>' ID="lnkRoles2"><%= AdminResources.Default_Roles %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlNews" CssClass="box_news">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsManager_aspx) %>' ID="lnkNews" Text='<%$ Code: AdminResources.Default_lnkNews %>'></asp:HyperLink></h1>
                        <p>
                            <asp:Label runat="server" ID="lblNewsIntro" Text='<%$ Code: AdminResources.Default_lblNewsIntro %>'></asp:Label>
                            <br />
                            <asp:HyperLink ID="lnkNewsQuickPost" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsQuickPost_aspx) %>' Text='<%$ Code: AdminResources.Default_lnkNewsQuickPost %>'></asp:HyperLink>
                            <br />
                            <asp:HyperLink runat="server" ID="lnkNewsCategoryAdmin" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsCategoryManager_aspx) %>' Text='<%$ Code: AdminResources.Default_lnkNewsCategoryAdmin %>'></asp:HyperLink>
                            <br />
                            <asp:HyperLink runat="server" ID="lnkNewsAdmin" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsManager_aspx) %>' Text='<%$ Code: AdminResources.Default_lnkNewsAdmin %>'></asp:HyperLink>
                            <br />
                            <asp:HyperLink runat="server" ID="lnkNewsPublisherAdmin" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsPublisherManager_aspx) %>' Text='<%$ Code: AdminResources.Default_lnkNewsPublisherAdmin %>'></asp:HyperLink>
                        </p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlAds" CssClass="box_add_managment">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.AdManager_aspx) %>' ID="lnkAds" Text='<%$ Code: AdminResources.Default_lnkAds %>'></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.AdManager_aspx) %>' ID="lnkAds2"><%= AdminResources.Default_Ads %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlPolls" CssClass="box_pools">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.PollManager_aspx) %>' ID="lnkPolls" Text='<%$ Code: AdminResources.Default_lnkPolls %>'></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.PollManager_aspx) %>' ID="lnkPolls2"><%= AdminResources.Default_Polls %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlLists" CssClass="box_lists">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.ListManager_aspx) %>' ID="lnkLists" Text='<%$ Code: AdminResources.Default_lnkLists %>'></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.ListManager_aspx) %>' ID="lnkLists22"><%= AdminResources.Default_Lists %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlNewsletters" CssClass="box_newsletter">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsletterManager_aspx) %>' ID="lnkNewsletters" Text='<%$ Code: AdminResources.Default_lnkNewsletters %>'></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.NewsletterManager_aspx) %>' ID="lnkNewsletters2"><%= AdminResources.Default_Newsletters %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlBlog" CssClass="box_blog">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.BlogManager_aspx) %>' ID="lnkBlog" Text='<%$ Code: AdminResources.Default_lnkBlog %>'></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.BlogManager_aspx) %>' ID="lnlBlog2"><%= AdminResources.Default_Blog %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlAmazon" CssClass="box_cloud">
                        <h1>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.FileAdminAmazonS3_aspx) %>' ID="lnkAmazon" Text='<%$ Code: AdminResources.Default_lnkAmazon %>'></asp:HyperLink></h1>
                        <p>
                            <asp:HyperLink runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.FileAdminAmazonS3_aspx) %>' ID="lnkAmazon2"><%= AdminResources.Default_Amazon %></asp:HyperLink></p>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlGroups" CssClass="box_groups">
                        <h1>
                            <asp:HyperLink ID="lnkGroups" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupManager_aspx) %>' ><%= AdminResources.AdminHeader_lnkGroups.ToUpper() %></asp:HyperLink>
                        </h1>
                        <p>
                            <asp:HyperLink ID="lnkGroupCat" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupCategoryManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupCategoryManager_aspx) %>'><%= AdminResources.AdminHeader_lnkGroupCat %></asp:HyperLink>    
                        </p>
                        <p>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupManager_aspx) %>'><%= AdminResources.AdminHeader_lnkGroups %></asp:HyperLink>
                        </p>
                        <p>
                            <asp:HyperLink ID="lnkGroupMembers" runat="server" NavigateUrl='<%# MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupMembershipManager_aspx) %>' Enabled='<%# MonoSoftware.MonoX.Utilities.PageUtility.CanViewAdminPage(MonoSoftware.MonoX.Paths.MonoX.Admin.SnGroupMembershipManager_aspx) %>'><%= AdminResources.AdminHeader_lnkGroupMembers %></asp:HyperLink>
                        </p>
                    </asp:Panel>
                    <asp:PlaceHolder ID="plhCustom" runat="server"></asp:PlaceHolder>
                </div>
                <!-- boxes_end -->
            </div>
            <!-- content_end -->
        </div>
        <mono:AdminFooter ID="adminFooter" runat="server" />
    </div>
    </form>
</body>
</html>
