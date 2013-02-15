<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Title="Group category manager" Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master"
    AutoEventWireup="True" CodeBehind="SnGroupManager.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.SnGroupManager"
    Theme="DefaultAdmin" %>

<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.LiteGrid" Assembly="MonoSoftware.Web.LiteGrid" %>
<%@ Register Src="~/MonoX/Admin/controls/GridViewEditBox.ascx" TagPrefix="monox" TagName="GridViewEditBox" %>
<%@ Register Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupEdit.ascx" TagPrefix="monox" TagName="GroupEdit" %>
<%@ MasterType VirtualPath="~/MonoX/MasterPages/AdminDefault.master" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">

<asp:ScriptManagerProxy ID="gridScriptManager" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/MonoX/Admin/controls/scripts/JSHelper.js" ScriptMode="Auto" />
    </Scripts>
</asp:ScriptManagerProxy>

<div class="AdminContainer sn-group-manager">
    <monox:GridViewEditBox ID="gridViewBox" runat="server" ShowTopActions="true" >
        <CustomFilterTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20%;">
                        <asp:Literal ID="labCategory" runat="server" Text='<%$ Code: AdminResources.SnGroupManager_labCategory %>'></asp:Literal>&nbsp;
                    </td>
                    <td style="width: 60%;">
                        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="searchselect"
                            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="306px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20%;">&nbsp;</td>
                </tr>
            </table>
        </CustomFilterTemplate>
        <Columns>
            <mono:LiteGridBoundField DataField="Id" Visible="false" />
            <mono:LiteGridBoundField DataField="Name" HeaderText='<%$ Code: AdminResources.SnGroupManager_sortNameButton %>' SortExpression="Name" />
            <mono:LiteGridBoundField DataField="Description" HeaderText='<%$ Code: AdminResources.SnGroupManager_sortDescriptionButton %>' SortExpression="Description" />
            <mono:LiteGridBoundField DataField="MemberCount" HeaderText='<%$ Code: AdminResources.SnGroupManager_sortMemberCountButton %>' SortExpression="MemberCount" />
            <mono:LiteGridBoundField DataField="IsPublic" HeaderText='<%$ Code: AdminResources.SnGroupManager_sortIsPublicButton %>' SortExpression="IsPublic" />
            <mono:LiteGridBoundField DataField="SnGroupCategory.Name" HeaderText='<%$ Code: AdminResources.SnGroupManager_sortSnGroupCategoryButton %>' SortExpression="SnGroupCategoryName" />
            <mono:LiteGridBoundField DataField="Slug" HeaderText='<%$ Code: AdminResources.SnGroupManager_sortSlugButton %>' SortExpression="Slug" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Literal ID="labEmptyMessage" runat="server" Text='<%$ Code: AdminResources.SnGroupManager_labEmptyMessage %>'></asp:Literal>
        </EmptyDataTemplate>
        <CustomActionsTemplate>
        </CustomActionsTemplate>
        <ContentTemplate>
            <div class="AdminGridFooterContent"><monox:GroupEdit ID="groupEdit" runat="server" ShowActionButtons="false"/></div>
        </ContentTemplate>
    </monox:GridViewEditBox>
</div>    
</asp:Content>
