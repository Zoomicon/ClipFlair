<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.Controls.MonoXPagerTemplate" %>

<div class="pager">
	<strong><asp:Literal runat="server" ID="ltlPage" Text="<%# PageText %>" ></asp:Literal></strong>
	<a href="javascript:void(0);" title="<%# Pager.CurrentPageIndex %>" name="<%# ConstructPermalinkSufix() %>" style="padding:0px;border:0px;height:25px;"></a>
	<asp:HyperLink id="Linkbutton1" runat="server" Visible="<%# !(Pager.IsFirstNumericGroup) %>" Text="<<" NavigateUrl='<%# ConstructParameterlessPagerUrl(0) %>'>
	</asp:HyperLink>
	<asp:HyperLink id="Linkbutton2" runat="server" Visible="<%# !(Pager.IsFirstNumericGroup) %>" Text="..." NavigateUrl='<%# ConstructParameterlessPagerUrl(Convert.ToInt32(Pager.CurrentPageIndex / Pager.NumericButtonCount) * Pager.NumericButtonCount - Pager.NumericButtonCount) %>'>
	</asp:HyperLink>
	<asp:Repeater id="Repeater1" runat="server" DataSource="<%# Pager.NumericButtonDataSource %>">
		<ItemTemplate>			
			<asp:hyperlink ID="Hyperlink1" runat="server" Text='<%# Convert.ToInt32(Container.DataItem) + 1 %>' NavigateURL='<%# ConstructParameterlessPagerUrl(Container.DataItem) %>' Visible='<%# Convert.ToInt32(Container.DataItem) != Pager.CurrentPageIndex %>'></asp:hyperlink>
			<span id="Span1" class="selected" runat="server" Visible='<%# (Convert.ToInt32(Container.DataItem) == Pager.CurrentPageIndex) && (Pager.PageCount > 1) %>'><%# Convert.ToInt32(Container.DataItem) + 1 %></span>
		</ItemTemplate>
	</asp:Repeater>
	<asp:HyperLink id="Linkbutton4" runat="server" Visible="<%# !(Pager.IsLastNumericGroup) %>" Text="..." NavigateUrl='<%# ConstructParameterlessPagerUrl(Convert.ToInt32(Pager.CurrentPageIndex / Pager.NumericButtonCount) * Pager.NumericButtonCount + Pager.NumericButtonCount) %>'>
	</asp:HyperLink>
	<asp:HyperLink id="Linkbutton5" runat="server" Visible="<%# !(Pager.IsLastNumericGroup) %>" Text=">>" NavigateUrl='<%# ConstructParameterlessPagerUrl(Pager.PageCount - 1) %>'>
	</asp:HyperLink>
</div>
