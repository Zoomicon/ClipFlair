<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.ArchiveNewsModule" Codebehind="ArchiveNewsModule.ascx.cs" %>
<div class="<%= CssClass %>">
    <div id="rowNewsTitle" runat="server">
        <h1 id="title" runat="server"></h1>
    </div>
    <div class="NewsData">
        <asp:DataList ID="newsHolder" runat="server"  
                RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table"  HorizontalAlign="left"     
                CellSpacing="0" CellPadding="5" ItemStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="top"
                AlternatingItemStyle-HorizontalAlign="left" AlternatingItemStyle-VerticalAlign="top"
                OnItemDataBound="newsHolder_ItemDataBound" Width="100%"
            >
                <AlternatingItemStyle VerticalAlign="top" Width="50%"/>
                <ItemStyle VerticalAlign="Top" Width="50%" />
                <ItemTemplate>        
                </ItemTemplate>
            </asp:DataList>
    </div>
    <div id="pagerHolder" runat="server">
    </div>
</div>