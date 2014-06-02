<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.MainNewsModule" Codebehind="MainNewsModule.ascx.cs" %>

<div class="<%= CssClass %>">
    <asp:DataList ID="newsHolder" runat="server"  
        RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table"  
        OnItemDataBound="newsHolder_ItemDataBound" Width="100%">
        <AlternatingItemStyle />
        <ItemStyle />
        <ItemTemplate>        
        </ItemTemplate>
    </asp:DataList>
    <div id="pagerHolder" runat="server"></div>
</div>