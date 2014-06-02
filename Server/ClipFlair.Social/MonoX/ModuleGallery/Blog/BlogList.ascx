<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="BlogList.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.Blog.BlogList" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>

<asp:Panel ID="pnlContainer" runat="server">
    <asp:ListView ID="lvItems" runat="server">
        <LayoutTemplate>
            <div class="clearfix">
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </div>
        </LayoutTemplate>
        <ItemTemplate></ItemTemplate>
    </asp:ListView>
    <asp:Literal runat="server" ID="ltlNoData"></asp:Literal>
    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
        <PagerTemplate></PagerTemplate>
    </mono:Pager>
</asp:Panel>