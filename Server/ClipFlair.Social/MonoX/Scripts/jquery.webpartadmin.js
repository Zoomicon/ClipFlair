/* 
* MonoX Web part admin script (drag and drop, menu functionality).
* NOTE: This script requires jQuery to work.  Download jQuery at www.jquery.com
*/

$(document).ready(function() {
    SetWebParts();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function(s, e) {
        SetWebParts();
        });
    }
});

function doPostBackAsync(eventName, eventArgs) {
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    if (!Array.contains(prm._asyncPostBackControlIDs, eventName)) {
        prm._asyncPostBackControlIDs.push(eventName);
    }

    if (!Array.contains(prm._asyncPostBackControlClientIDs, eventName)) {
        prm._asyncPostBackControlClientIDs.push(eventName);
    }

    prm._doPostBack(eventName, eventArgs);
}

function SetWebParts() {

    //hides verb menus on outer click
    $('html').click(function() {
        $(".web-part-menu-content").hide();
        $(".web-part-menu-button").removeClass("active");
        $(".web-part-menu-button").css('display', 'none');
    });
    
    //to prevent hiding verb menus on click per above functionality, removed to support hiding
    $(".web-part-menu-content").click(function(event) {
        //event.stopPropagation();
    });

    //toggles menu visibility
    $(".web-part-menu-button").click(function(event) {
        $(".web-part-menu-button").not(this).removeClass("active");
        $(".web-part-menu-button").not(this).hide();
        $(".web-part-menu-content").hide();
        $(this).toggleClass("active").next().slideToggle(100);
        event.stopPropagation();
        //return false; 
    });

    $(function() {
        //set the tabs, if any, in the admin toolbar area
        $(".monoXCatalogTabs").tabs({ cookie: { name: 'monox_catalogTab_cookie', expires: 1} });
    });
    
    //select the checkboxes on click of the part
    $(".draggableCatalog.web-part-box").bind("click", function(e) {
        if (e.target.nodeName.toLowerCase() != "input" && e.target.nodeName.toLowerCase() != "label") {
            var checkbox = $(this).find(":checkbox");
            checkbox.attr("checked", !checkbox.attr("checked"));
        }
    });    
    
    //init the fraggable operation. put the elements that should not be draggable in the cancel area in the line below
    $(".draggable").draggable({
        helper: 'original',
        cancel: ':input,option,div.reContentArea',
        revert: 'invalid',
        zIndex: 1000
    });
    $(".draggableCatalog").draggable({
        helper: 'clone',
        appendTo: 'body',
        cancel: ':input,option,div.reContentArea',
        revert: 'invalid',
        zIndex: 1000,
        start: function(event, ui) {
            var checkbox = $(this).find(":checkbox");
            checkbox.attr("checked", true);
        }
    });
    $(".droppable").droppable({
        tolerance: 'pointer',
        activate: function(event, ui) {
            $(".droppable").addClass("ui-state-active");
            //used with table based zones
            var parentTr = ui.draggable.parents("tr").eq(0);
            var prevDroppable = parentTr.prev().find(".droppable");
            var nextDroppable = parentTr.next().find(".droppable");
            prevDroppable.droppable('disable');
            nextDroppable.droppable('disable');
            prevDroppable.removeClass("ui-state-active");
            nextDroppable.removeClass("ui-state-active");

            //used with tableless zones
            var prevDroppableDiv = ui.draggable.prev(".droppable");
            var nextDroppableDiv = ui.draggable.next(".droppable");
            prevDroppableDiv.droppable('disable');
            nextDroppableDiv.droppable('disable');
            prevDroppableDiv.removeClass("ui-state-active");
            nextDroppableDiv.removeClass("ui-state-active");
        },
        drop: function(event, ui) {

            var webPartZoneId = $(this).parents(".webPartZoneClass").attr("ID").replace(/-/g, '$');
            var index = $(this).attr("wpindex");
            var webPartId = ui.draggable.eq(0).attr("webPartID");
            if (webPartId != null)
                doPostBackAsync(webPartZoneId, 'Drag:' + webPartId + ':' + index);
            else {
                webPartId = ui.draggable.eq(0).attr("catalogWebPartID");
                doPostBackAsync(webPartZoneId, 'CatalogDrag:' + webPartId + ':' + index);
            }
        },
        deactivate: function(event, ui) {
            $(".droppable").droppable('enable');
            $(".droppable").removeClass("ui-state-active");
        },
        over: function(event, ui) {
            $(this).removeClass("ui-state-active");
            $(this).addClass("ui-state-hover");
        },
        out: function(event, ui) {
            $(this).removeClass("ui-state-hover");
            $(this).addClass("ui-state-active");
        }
    });
    
};

//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }