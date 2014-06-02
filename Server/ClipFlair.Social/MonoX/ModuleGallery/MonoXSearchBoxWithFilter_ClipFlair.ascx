<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="MonoXSearchBoxWithFilter.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXSearchBoxWithFilter" %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
<asp:panel ID="pnlContainer" runat="server" DefaultButton="btnSearch" CssClass="search-filter-container">
    <div class="search">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" /><MonoX:StyledButton runat="server" ID="lnkSearch" OnClick="btnSearch_Click" CssClass="search-styled-button"></MonoX:StyledButton>
        <div class="holder">
            <asp:TextBox AutoCompleteType="Disabled" runat="server" ID="txtSearch"></asp:TextBox>
        </div>
        <dl class="search-filter-list" style="display: none;">
            <strong><%= MonoSoftware.MonoX.Resources.Search.SearchFilterCaption %></strong>
            <asp:Repeater ID="rptFilters" runat="server">
                <ItemTemplate>
                    <dd>
                        <input type="checkbox" runat="server" value='<%# Eval("ProviderName") %>' checked='<%# Eval("CheckedValue") %>' />
                        <label for='<%# Eval("ProviderLocalization") %>'><%# Eval("ProviderLocalization") %></label>
                    </dd>
                </ItemTemplate>
            </asp:Repeater>
        </dl>
    </div>
</asp:panel>