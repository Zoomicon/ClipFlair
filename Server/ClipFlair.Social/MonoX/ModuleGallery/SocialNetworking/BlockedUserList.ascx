<%@ Control Language="C#" 
    AutoEventWireup="true"     
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.BlockedUserList" 
    CodeBehind="BlockedUserList.ascx.cs" 
%>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="MonoX" %>

<asp:Panel ID="pnlSearch" runat="server" CssClass="PeopleSearch" DefaultButton="btnFilter">
<div class="block-user-list clearfix">
    <div class="search-textbox input-form">
        <asp:TextBox ID="tbUsersFilter" runat="server" />
    </div>
    <div class="button-holder small-btn">
        <MonoX:StyledButton runat="server" ID="btnFilter" CausesValidation="false" OnClick="btnFilterUsers_Click" Text="Filter" CssClass="filter-btn float-left" />
        <MonoX:StyledButton runat="server" ID="btnClear" CausesValidation="false" OnClick="btnClearFilterUsers_Click" Text="Clear" CssClass="cancel-btn float-left" />
    </div>
</div><br /><br />
</asp:Panel>
<asp:Panel ID="pnlContainer" runat="server">
    <asp:ListView ID="lvItems" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate></ItemTemplate>
    </asp:ListView>
</asp:Panel>
<asp:Label runat="server" ID="lblMessage" CssClass="empty-list"></asp:Label>
<div style="clear:both">
    <MonoX:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
        <PagerTemplate></PagerTemplate>
    </MonoX:Pager>
</div>
