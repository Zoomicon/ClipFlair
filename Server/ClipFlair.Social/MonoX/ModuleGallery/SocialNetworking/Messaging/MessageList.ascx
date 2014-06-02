<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageList.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.MessageList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<telerik:RadGrid ID="radMail" ValidationSettings-ValidationGroup="MessageGrid" runat="server" Width="100%" PageSize="10" 
    AllowSorting="True" Skin="Default" AllowMultiRowSelection="False" AllowPaging="True" AllowCustomPaging="True" 
    ShowGroupPanel="false" GridLines="None" AutoGenerateColumns="False" OnItemDataBound="radMail_ItemDataBound"    
    >
    <PagerStyle Mode="NextPrevAndNumeric" EnableSEOPaging="False" PageButtonCount="5">
    </PagerStyle>
    <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id" DataMember="SnMessage" AllowMultiColumnSorting="False" AllowNaturalSort="false"
        Width="100%" TableLayout="Fixed">
        <Columns>
            <telerik:GridTemplateColumn UniqueName="TemplateColumn1" Groupable="False">
                <HeaderStyle Width="5%"></HeaderStyle>
                <ItemStyle Height="35px"></ItemStyle>
                <ItemTemplate>
                    <asp:ImageButton CausesValidation="false" runat="server" ID="imgView" BorderWidth="0px" CommandArgument='<%# Eval("Id") %>' CommandName="ViewMessage" ImageUrl='<%# MonoSoftware.MonoX.Utilities.UrlUtility.ResolveThemeUrl("img/view.png") %>' AlternateText='<%# SocialNetworkingResources.Messaging_MessageList_View %>' Style="float: right; cursor: pointer;"></asp:ImageButton>
                 </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn UniqueName="AspnetUser.UserName" SortExpression="AspnetUser.UserName" DataField="AspnetUser.UserName">
                <HeaderStyle Width="25%"></HeaderStyle>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn UniqueName="ToListShort" SortExpression="ToListShort" DataField="ToListShort">
                <HeaderStyle Width="25%"></HeaderStyle>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn UniqueName="Subject" SortExpression="Subject" DataField="Subject">
                <HeaderStyle Width="40%"></HeaderStyle>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn UniqueName="DateCreated" SortExpression="DateCreated" DataField="DateCreated" DataFormatString="{0:G}">
                <HeaderStyle Width="25%"></HeaderStyle>
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateColumn3" Groupable="False">
                <HeaderStyle Width="5%"></HeaderStyle>
                <ItemStyle Height="35px"></ItemStyle>
                <ItemTemplate>
                    <asp:ImageButton runat="server" ID="btnDelete" CausesValidation="false" BorderWidth="0px" OnClientClick="javascript:if(!confirm(ResourceManager.GetString('DeleteConfirmationMessage'))){return false;}" CommandArgument='<%# Eval("Id") %>' CommandName="DeleteMessage" ImageUrl='<%# MonoSoftware.MonoX.Utilities.UrlUtility.ResolveThemeUrl("img/delete.png") %>' AlternateText='<%# DefaultResources.Button_Delete %>' Style="float: right; cursor: pointer;"></asp:ImageButton>
                 </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings AllowDragToGroup="false" AllowColumnsReorder="false" >
        <Selecting AllowRowSelect="True"></Selecting>
        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="450px"></Scrolling>
        <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
    </ClientSettings>
</telerik:RadGrid>
<div style="text-align:right;margin: 5px 10px 5px 20px; white-space:nowrap;width:97%">
    <asp:Label ID="lblFilter" CssClass="simpleLabel"  runat="server"></asp:Label> 
    <asp:TextBox ID="txtSearch" runat="server" style="vertical-align: middle" CssClass="simpleTextBox" ></asp:TextBox>
    <asp:ImageButton ID="btnSearch" CausesValidation="false" runat="server" ImageAlign="Middle" OnClick="btnSearch_Click" />
    <asp:ImageButton ID="btnShowAll" CausesValidation="false" runat="server" ImageAlign="Middle" OnClick="btnShowAll_Click" />
</div>

            