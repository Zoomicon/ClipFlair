<%@ Control Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="EventSimpleView.ascx.cs" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.EventSimpleView" %>
    
<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:PlaceHolder ID="plhFilter" runat="server">
    <div class="top-button-holder">
        <div class="date-picker"><telerik:RadDateTimePicker ID="dateFrom" runat="server"></telerik:RadDateTimePicker></div>
        <div class="date-picker"><telerik:RadDateTimePicker ID="dateTo" runat="server"></telerik:RadDateTimePicker></div>
        <MonoX:StyledButton ID="btnFilter" runat="server" />
        <MonoX:StyledButton ID="btnClearFilter" runat="server" />
    </div>
</asp:PlaceHolder>

<div class="list-view">
    <asp:ListView ID="lvItems" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate></ItemTemplate>
    </asp:ListView>

    <mono:Pager runat="server" ID="pager" PageSize="10" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
        <PagerTemplate></PagerTemplate>
    </mono:Pager>
</div>