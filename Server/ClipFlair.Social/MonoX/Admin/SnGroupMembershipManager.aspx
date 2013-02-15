<%@ Assembly Name="MonoSoftware.MonoX.DAL" %>
<%@ Import Namespace="MonoSoftware.MonoX.DAL.EntityClasses" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<%@ Page Title="Group category manager" Language="C#" MasterPageFile="~/MonoX/MasterPages/AdminDefault.master"
    AutoEventWireup="True" CodeBehind="SnGroupMembershipManager.aspx.cs" Inherits="MonoSoftware.MonoX.Admin.SnGroupMembershipManager"
    Theme="DefaultAdmin" %>

<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls" TagPrefix="mono" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/MonoX/controls/UserPicker.ascx" TagPrefix="MonoX" TagName="UserSearch" %>
<%@ Register TagPrefix="mono" Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" %>
<%@ MasterType VirtualPath="~/MonoX/MasterPages/AdminDefault.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="AdminContainer">
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <div class="AdminHeaderTop"></div>
                <div class="AdminHeaderBottom">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 20%; padding-bottom: 2px;">
                                <asp:Label ID="labCategory" runat="server" AssociatedControlID="ddlCategory" Text='<%$ Code: AdminResources.SnGroupMembershipManager_labCategory %>'></asp:Label>
                            </td>
                            <td style="width: 60%; padding-bottom: 2px;">
                                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="searchselect"
                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="306px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%;">
                                <asp:Button ID="btnAdd" runat="server" CausesValidation="false" Text='<%$ Code: AdminResources.SnGroupMembershipManager_btnAdd %>'
                                CssClass="AdminLargeButton" OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; padding-bottom: 2px;">
                                <asp:Label ID="labGroup" runat="server" AssociatedControlID="ddlGroup" Text='<%$ Code: AdminResources.SnGroupMembershipManager_labGroup %>'></asp:Label>
                            </td>
                            <td style="width: 60%; padding-bottom: 2px;">
                                <telerik:RadComboBox ID="ddlGroup" runat="server" EnableLoadOnDemand="true" MarkFirstMatch="true"
                                    AllowCustomText="false" ShowDropDownOnTextboxClick="false" Width="306px" Height="200"
                                    AutoPostBack="true">
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 20%;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">
                                <asp:Label ID="labUser" runat="server" AssociatedControlID="userSearch" Text='<%$ Code: AdminResources.SnGroupMembershipManager_labUser %>'></asp:Label>
                            </td>
                            <td style="width: 60%;">
                                <div class="PeopleSearch">
                                    <MonoX:UserSearch ID="userSearch" runat="server" Width="306px"></MonoX:UserSearch>
                                </div>
                            </td>
                            <td style="width: 20%;">&nbsp;</td>
                        </tr>
                    </table>
                </div>
                <div class="AdminGridFooterContent">                                        
                    <div>
                        <div style="float: left;">
                            <asp:Label ID="labUserInGroup" runat="server" AssociatedControlID="lstUserInGroup" Text='<%$ Code: AdminResources.SnGroupMembershipManager_labUserInGroup %>'></asp:Label>
                            <br />
                            <telerik:RadListBox ID="lstUserInGroup" runat="server" SelectionMode="Single" AllowReorder="true"
                                AllowDelete="true" Width="470px" Height="200px" Skin="Web20" AutoPostBackOnDelete="true">
                            </telerik:RadListBox><br />
                            <asp:Button ID="btnSetAsAdmin" runat="server" CausesValidation="false" Text='<%$ Code: AdminResources.SnGroupMembershipManager_btnSetAsAdmin %>'
                            CssClass="AdminLargeButton" OnClick="btnSetAsAdmin_Click" />
                            <mono:Pager ID="pager" runat="server" AutoPaging="False" AllowCustomPaging="True"
                                PageSize="5" NumericButtonCount="5">
                                <PagerTemplate>
                                </PagerTemplate>
                            </mono:Pager>
                        </div>                        
                        <div style="float: right;">
                            <asp:Label ID="labGroupRequests" runat="server" AssociatedControlID="lstGroupRequests" Text='<%$ Code: AdminResources.SnGroupMembershipManager_labGroupRequests %>'></asp:Label>
                            <br />
                            <telerik:RadListBox ID="lstGroupRequests" runat="server" SelectionMode="Single" AllowReorder="true"
                                AllowDelete="true" Width="470px" Height="200px" Skin="Web20" AutoPostBackOnDelete="true">
                            </telerik:RadListBox><br />
                            <asp:Button ID="btnApprove" runat="server" CausesValidation="false" Text='<%$ Code: AdminResources.SnGroupMembershipManager_btnApprove %>'
                            CssClass="AdminLargeButton" OnClick="btnApprove_Click" />
                            <mono:Pager ID="pagerGroupRequests" runat="server" AutoPaging="False" AllowCustomPaging="True"
                                PageSize="5" NumericButtonCount="5">
                                <PagerTemplate>
                                </PagerTemplate>
                            </mono:Pager>
                        </div>
                    </div>                    
                    <div>
                        <strong style="color: Red;">
                            <asp:Literal ID="labError" runat="server"></asp:Literal></strong>
                    </div>                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
