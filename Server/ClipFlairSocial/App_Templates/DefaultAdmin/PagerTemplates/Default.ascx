<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Controls.MonoXPagerTemplate" 
    %>

<div class="pager-container">
    <div class="listing-prev">	        
	    <asp:LinkButton CssClass="Paging" id="lnkFirst" runat="server" Visible="<%# !(Pager.IsFirstNumericGroup) %>" Text="<<" ToolTip="<%# FirstText %>"  CommandName="FirstPage"></asp:LinkButton>
        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# PreviousText %>' CommandName="PreviousPage" Visible='<%# !Pager.IsFirstPage %>'></asp:LinkButton>
    </div>
    <div id="listing-pages">
        <span class="txt-light"><asp:Literal runat="server" ID="ltlPage" Text="<%# PageText %>" ></asp:Literal></span>&nbsp;
        <asp:LinkButton ID="LinkButton3" runat="server" Visible="<%# !Pager.IsFirstNumericGroup %>" CommandName="PreviousNumericGroup" Text="..." ToolTip="<%# PreviousGroupText %>"></asp:LinkButton>
        <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Pager.NumericButtonDataSource %>'>
	        <ItemTemplate>
		        <%# Container.ItemIndex > 0 ? " | " : "" %>
		        <asp:LinkButton ID="lbtPage" runat="server" Text='<%# ((int)Container.DataItem + 1).ToString() %>' CommandName="NumericPage" CommandArgument='<%# Container.DataItem %>' Visible='<%# (int)Container.DataItem != Pager.CurrentPageIndex %>'>
		        </asp:LinkButton>
		        <asp:Label ID="ltlCurrentPage" Runat="server" CssClass="current-page" Text='<%# ((int)Container.DataItem + 1).ToString() %>' Visible='<%# (int)Container.DataItem == Pager.CurrentPageIndex %>'></asp:Label>
	        </ItemTemplate>
        </asp:Repeater>
        <asp:LinkButton ID="LinkButton4" runat="server" Visible="<%# !Pager.IsLastNumericGroup %>" CommandName="NextNumericGroup" Text="..." ToolTip="<%# NextGroupText %>"></asp:LinkButton>
    </div>
    <div class="listing-next">
        <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# NextText %>' CommandName="NextPage" Visible='<%# !Pager.IsLastPage %>'></asp:LinkButton>
	    <asp:LinkButton id="lnkLast" CssClass="Paging" runat="server" Visible="<%# !(Pager.IsLastNumericGroup) %>" Text=">>" ToolTip="<%# LastText %>" CommandName="LastPage"></asp:LinkButton>
    </div>
    <div id="listing-hits"><strong><%# Pager.CurrentPageIndex * Pager.PageSize + 1 %>-<%# Pager.IsLastPage ? Pager.DataSourceCount : (Pager.CurrentPageIndex + 1) * Pager.PageSize%></strong> of <strong><%# Pager.DataSourceCount %></strong>
    </div>
</div>		