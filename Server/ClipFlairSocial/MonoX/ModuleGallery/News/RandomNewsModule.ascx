<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.RandomNewsModule" Codebehind="RandomNewsModule.ascx.cs" %>

<table width="100%" class="<%= CssClass %>">
    <tr>
        <td class="NewsData">
            <asp:DataList ID="newsHolder" runat="server"  
                RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table"  HorizontalAlign="left"     
                CellSpacing="0" CellPadding="5" ItemStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="top"
                AlternatingItemStyle-HorizontalAlign="left" AlternatingItemStyle-VerticalAlign="top"
                OnItemDataBound="newsHolder_ItemDataBound" Width="100%"
            >
                <AlternatingItemStyle VerticalAlign="top" Width="50%"/>
                <ItemStyle VerticalAlign="top" Width="50%" />
                <ItemTemplate>        
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
    <tr>
        <td id="pagerHolder" runat="server">
        
        </td>
    </tr>
</table>

