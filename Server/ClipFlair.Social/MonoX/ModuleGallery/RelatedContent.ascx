<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedContent.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.RelatedContent" %>

<%@ Register Namespace="MonoSoftware.Web.Pager" Assembly="MonoSoftware.Web.Pager" TagPrefix="mono" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
<asp:Panel ID="pnlContainer" runat="server"> 
    <div class="related-topics">
        <asp:PlaceHolder ID="plhTitle" runat="server">
            <h4>
                <a id="related" href="#">&#9662;&nbsp;<asp:Label ID="labTitle" runat="server"></asp:Label></a>
            </h4>    
        </asp:PlaceHolder>
        <asp:ListView ID="lvItems" runat="server">
            <LayoutTemplate>
                <ul class="related-content" style="display: none;">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </ul>
            </LayoutTemplate>
            <ItemTemplate></ItemTemplate>
        </asp:ListView>
        <mono:Pager runat="server" ID="pager" PageSize="5" NumericButtonCount="5" AllowCustomPaging="true" AutoPaging="false">
            <PagerTemplate></PagerTemplate>
        </mono:Pager>
    </div>
    <script type="text/javascript">
    
        
        $("#related").click(function () {
            $('.related-content').slideToggle();
        });
     
    </script>
</asp:Panel>
