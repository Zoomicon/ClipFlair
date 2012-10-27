<%@ Control
Language="C#"
AutoEventWireup="true"
CodeBehind="DemoIndicator.ascx.cs"
Inherits="MonoSoftware.MonoX.MasterPages.DemoIndicator" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<div class="demo">
    <%= DefaultResources.DemoIndicator %>
    <a href="#" class="close"></a>
</div>

<script type="text/javascript" language="javascript">
    //<![CDATA[
    $('div.demo > .close').click(function() {
        $('div.demo').slideUp('fast', function() {
            $.cookie("DemoIndicatorClosed", "true", { expires: 365 });
        });
    });
    //]]>
</script>

