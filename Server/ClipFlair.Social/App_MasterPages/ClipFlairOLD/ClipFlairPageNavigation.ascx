<%@ Control 
Language="C#"
AutoEventWireup="true"
%>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>
<%@ Register TagPrefix="MonoX" TagName="Menu" Src="~/MonoX/ModuleGallery/MonoXMenuHTML5.ascx" %>
<nav>
    <div id="navigation-wrapper-bg">
        <div class="navigation-wrapper">
            <div class="navigation">
                <MonoX:Menu runat="server" ID="ctlMenu" UseSpanElementsInMenuItems="false" SelectedItemCssClass="selected" CacheDuration="600" ResponsiveDesignBrakeWidth="959" /> 
            </div>
        </div>
    </div>
<nav>