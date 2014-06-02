<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXMenuHTML5" Codebehind="MonoXMenuHTML5.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Controls.HierarchicalRepeater" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls.HierarchicalRepeater" TagPrefix="Mono" %>

<nav id="<%= this.ClientID %>" class='<%= MonoSoftware.MonoX.Utilities.SecurityUtility.IsAdmin() ? "administrator-mode" : "" %>'>
    <Mono:HierarchicalRepeater runat="server" ID="repeater" CssClass="monox-simple-menu" ControlHTMLTag="UL">
        <TemplateCollection>
            <Mono:ItemTemplate ListItemType="HeaderTemplate">
                <Template>
                </Template>
            </Mono:ItemTemplate>
            <Mono:ItemTemplate Depth="-1" ListItemType="DepthHeaderTemplate">
                <Template>
                </Template>
            </Mono:ItemTemplate>
            <Mono:ItemTemplate Depth="0" ListItemType="ItemHeaderTemplate">
                <Template>
                </Template>
            </Mono:ItemTemplate>
            <Mono:ItemTemplate Depth="-1" ListItemType="ItemTemplate">
                <Template>
	                <li class='<%# GetItemCssClass((HierarchyData<NavigationMenuItem>)Container.DataItem) %> <%# Eval("Item.CssClass").ToString() %>' title='<%#Eval("Item.ToolTip") %>'><%# Convert.ToBoolean(Eval("Item.Url").ToString().Length > 0) ? "<a href=\"" + Eval("Item.Url") + "\">" : "<a href=\"#\">"%><%#Eval("Item.Title") %></a>
                    <%# Convert.ToBoolean(Eval("HasChildren")) ? "<span class='sub-nav-desktop'></span>" : String.Empty %>
                    <!--CLIPFLAIR-->
                    <%# Convert.ToBoolean(Eval("HasChildren")) ? "<span class='level0-wrapper'><ul class='level" + Convert.ToInt32(Eval("Item.Depth")).ToString() + "'>" : Convert.ToInt32(Eval("Item.Depth")) > 0 ? "</li>" : "" %>                   
                </Template>               
            </Mono:ItemTemplate>
            <Mono:ItemTemplate Depth="-1" ListItemType="ItemFooterTemplate">
                <Template>
                    <%# Convert.ToBoolean(Eval("HasChildren")) ? "</ul></span></li>" : "" %>
                </Template>
            </Mono:ItemTemplate>
            <Mono:ItemTemplate Depth="-1" ListItemType="DepthFooterTemplate">
                <Template>
                </Template>
            </Mono:ItemTemplate>
            <Mono:ItemTemplate ListItemType="FooterTemplate">
                <Template>
                    <li class="external-link" title="Start ClipFlair Studio"><a class="blue-button" href="http://studio.clipflair.net">Start Studio</a></li>
                    <li class="external-link" title="Browse ClipFlair Gallery"><a class="orange-button" href="http://gallery.clipflair.net">Browse Gallery</a></li>
                </Template>
            </Mono:ItemTemplate>
        </TemplateCollection>       
    </Mono:HierarchicalRepeater>
</nav>
<asp:PlaceHolder ID="panResponsiveDesign" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
            InitResponsiveNav();
            if ( typeof(Sys) !== "undefined"  ) {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (s, e) {
                        InitResponsiveNav();   
                        $(window).trigger("resize"); //Reinit
                    });
                }
            }
        });

    function InitResponsiveNav() {        
        $('#<%= this.ClientID %>').responsinav({ breakpoint: <%= ResponsiveDesignBrakeWidth %> });
    }
</script>
</asp:PlaceHolder>

