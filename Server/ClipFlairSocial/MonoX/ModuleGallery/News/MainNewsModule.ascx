<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.MainNewsModule" Codebehind="MainNewsModule.ascx.cs" %>

<table width="100%" class="<%= CssClass %>">
    <tr>
        <td class="NewsData">
            <asp:DataList ID="newsHolder" runat="server"  
                RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table"  
                OnItemDataBound="newsHolder_ItemDataBound" Width="100%"
            >
                <AlternatingItemStyle Width="50%" />
                <ItemStyle Width="50%" />
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

