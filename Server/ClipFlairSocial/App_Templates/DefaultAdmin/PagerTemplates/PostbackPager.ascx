<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Controls.MonoXPagerTemplate" 
    %>

<div class="pager">
	<strong><asp:Literal runat="server" ID="ltlPage" Text="<%# PageText %>" ></asp:Literal></strong>
	<asp:LinkButton id="Linkbutton1" runat="server" Visible="<%# !(Pager.IsFirstNumericGroup) %>" Text="<<" CommandName="FirstPage">
	</asp:LinkButton>
	<asp:LinkButton id="Linkbutton2" runat="server" Visible="<%# !(Pager.IsFirstNumericGroup) %>" Text="..." CommandName="PreviousPage">
	</asp:LinkButton>
	<asp:Repeater id="Repeater1" runat="server" DataSource="<%# Pager.NumericButtonDataSource %>">
		<ItemTemplate>			
			<asp:LinkButton ID="Hyperlink1" runat="server" Text='<%# Convert.ToInt32(Container.DataItem) + 1 %>' CommandName="NumericPage" CommandArgument='<%# Container.DataItem %>' Visible='<%# Convert.ToInt32(Container.DataItem) != Pager.CurrentPageIndex %>'></asp:LinkButton>
			<span id="Span1" class="selected" runat="server" Visible='<%# (Convert.ToInt32(Container.DataItem) == Pager.CurrentPageIndex) && (Pager.PageCount > 1) %>'><%# Convert.ToInt32(Container.DataItem) + 1 %></span>
		</ItemTemplate>
	</asp:Repeater>
	<asp:LinkButton id="Linkbutton4" runat="server" Visible="<%# !(Pager.IsLastNumericGroup) %>" Text="..." CommandName="NextPage">
	</asp:LinkButton>
	<asp:LinkButton id="Linkbutton5" runat="server" Visible="<%# !(Pager.IsLastNumericGroup) %>" Text=">>" CommandName="LastPage">
	</asp:LinkButton>
</div>
	