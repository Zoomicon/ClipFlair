<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.LanguageChanger" Codebehind="LanguageChanger.ascx.cs" %>
    
<div class="language-changer">
    <asp:DataList ID="languageChooser" runat="server" 
        RepeatColumns="10" RepeatDirection="Horizontal" RepeatLayout="Table"    
        CellSpacing="1" CellPadding="1" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="middle"
        OnItemDataBound="languageChooser_ItemDataBound" 
    >
        <AlternatingItemStyle VerticalAlign="Middle" />
        <ItemStyle VerticalAlign="Middle" />
        <ItemTemplate>        
            
        </ItemTemplate>
    </asp:DataList>    
</div>    