<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.ModuleGallery.LanguageChanger" Codebehind="LanguageChanger.ascx.cs" %>
    
<div class="language-changer">
    <asp:DataList ID="languageChooser" runat="server" 
        RepeatColumns="20" RepeatDirection="Horizontal" RepeatLayout="Flow"    
        CellSpacing="1" CellPadding="1" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="middle"
        OnItemDataBound="languageChooser_ItemDataBound" >
        <AlternatingItemStyle VerticalAlign="Top" />
        <ItemStyle VerticalAlign="Top" />
        <ItemTemplate>
        </ItemTemplate>
    </asp:DataList>    
</div> 

<!--check for Euskera and Catalan in order to place the correct image-->
<script type="text/javascript">
    $(document).ready(function() { 
        
        $('.language-changer img').each(function() {
            var title = $(this).attr("title");
            title = title.toString();
            
            if (title.toLowerCase().indexOf("basque") >= 0) {
                $(this).attr("src", function(index, old) {
                    return old.replace("ES","EU");
                });
            }
            if (title.toLowerCase().indexOf("catalan") >= 0) {
                $(this).attr("src", function(index, old) {
                    return old.replace("ES","CA");
                });
            }
            
        });

    });
</script>