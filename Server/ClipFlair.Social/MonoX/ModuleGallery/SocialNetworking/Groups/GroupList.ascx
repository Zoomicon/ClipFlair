<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="GroupList.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupList" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>

<asp:Panel ID="pnlContainer" runat="server">
    <div class="top-section clearfix">
        <h1><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h1>
        <div class="main-options">
            <!--CLIPFLAIR-->
            <asp:HyperLink runat="server" CssClass="styled-button list-btn float-left" ID="lnkMyGroups"></asp:HyperLink>
            <asp:HyperLink runat="server" CssClass="styled-button list-btn float-left" ID="lnkAllGroups"></asp:HyperLink>
            <asp:HyperLink runat="server" CssClass="styled-button main-button add-btn float-left" ID="lnkAddNew"></asp:HyperLink></a>
            <!--/CLIPFLAIR-->
        </div>
    </div>
    <div class="clearfix">
        <asp:ListView ID="lvItems" runat="server">
            <LayoutTemplate>
                <div class="clearfix">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </div>
            </LayoutTemplate>
            <ItemTemplate></ItemTemplate>
        </asp:ListView>
    </div>
    <asp:Literal runat="server" ID="ltlNoData"></asp:Literal>
    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
        <PagerTemplate></PagerTemplate>
    </mono:Pager>
</asp:Panel>