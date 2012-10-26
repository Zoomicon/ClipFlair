<%@ Control Language="C#" AutoEventWireup="true" Inherits="MonoSoftware.MonoX.ModuleGallery.MonoXMenuSimple" Codebehind="MonoXMenuSimple.ascx.cs" %>
<%@ Import Namespace="MonoSoftware.MonoX.Controls.HierarchicalRepeater" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.Controls.HierarchicalRepeater" TagPrefix="Mono" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.SearchEngine" TagPrefix="Search" %>
<%@ Register TagPrefix="MonoX" TagName="Search" Src="~/MonoX/ModuleGallery/MonoXSearchBox.ascx" %>

<div class="navigation">
	
        <Mono:HierarchicalRepeater runat="server" ID="repeater" CssClass="MonoXSimpleMenu">
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
                        <ul class="<%# GetItemCssClass((HierarchyData<NavigationMenuItem>)Container.DataItem) %>">
                    </Template>
                </Mono:ItemTemplate>
                <Mono:ItemTemplate Depth="-1" ListItemType="ItemTemplate">
                    <Template>
	                    <li class='<%# SetItemClass(Eval("Item.Url").ToString()) %> <%# Eval("Item.CssClass").ToString() %>' title='<%#Eval("Item.ToolTip") %>'><%# Convert.ToBoolean(Eval("Item.Url").ToString().Length > 0) ? "<a href=\"" + Eval("Item.Url") + "\">" : "<a href=\"#\">"%><%#Eval("Item.Title") %></a><%# Convert.ToBoolean(Eval("HasChildren")) ? "<ul class='level" + Convert.ToInt32(Eval("Item.Depth")).ToString() + "'>" : Convert.ToInt32(Eval("Item.Depth")) > 0 ? "</li>" : "" %>
                    </Template>
                </Mono:ItemTemplate>
                <Mono:ItemTemplate Depth="0" ListItemType="ItemFooterTemplate">
                    <Template>
                        <%# Convert.ToBoolean(Eval("HasChildren")) ? "</ul></li>" : "</li>"%>
                        </ul>
                    </Template>
                </Mono:ItemTemplate>
                <Mono:ItemTemplate Depth="-1" ListItemType="ItemFooterTemplate">
                    <Template>
                        <%# Convert.ToBoolean(Eval("HasChildren")) ? "</ul></li>" : "" %>
                    </Template>
                </Mono:ItemTemplate>
                <Mono:ItemTemplate Depth="-1" ListItemType="DepthFooterTemplate">
                    <Template>
                    </Template>
                </Mono:ItemTemplate>
                
                
                <Mono:ItemTemplate ListItemType="FooterTemplate">            
                    <Template>
                    </Template>
                </Mono:ItemTemplate>
            </TemplateCollection>
        </Mono:HierarchicalRepeater>

        <div class="search-holder">
            <MonoX:Search runat="server" ID="ctlSearch" CssClass="search" TextBoxCssClass="mainSearchBoxInputField" ButtonCssClass="searchImage" ButtonText="" DefaultSearchText="<%$ Code:DefaultResources.SiteSearch_DefaultText %>" />
        </div>
    
</div>
