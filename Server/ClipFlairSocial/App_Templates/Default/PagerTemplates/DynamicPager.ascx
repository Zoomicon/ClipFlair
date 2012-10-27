<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Controls.DynamicPagerTemplate" 
    %>
 
<div class="pager">  
    <asp:HiddenField ID="fieldState" runat="server" Value="0" />
	<asp:LinkButton id="lnkMore" runat="server" CommandName="NumericPage" CommandArgument='<%# Pager.CurrentPageIndex + 1 %>' >
	</asp:LinkButton>
<%--<asp:LinkButton id="lnkLess" runat="server" CommandName="NumericPage" CommandArgument='<%# Pager.CurrentPageIndex - 1 %>' Visible="<%# this.CurrentPageIndex > 0  %>">
	</asp:LinkButton>--%>
</div>

