/* MonoX Simple Menu - Parent Selector implementation 
NOTE: This javascript code is strongly tied to MonoXMenuSimple.ascx menu template */

$(document).ready(function() {
    InitMonoXMenuSelectors();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function(s, e) {
        InitMonoXMenuSelectors();
        });
    }
});

jQuery.MonoXMenu_GetParent = function(parent, level) {
    var resultParent = $(parent);
    var i = 0;
    while (i < level) {
        resultParent = resultParent.parent();
        i++;
    }
    return resultParent;
}

jQuery.MonoXMenu_SelectTopLevel = function(parent) {
    $.MonoXMenu_GetParent(parent, 3).addClass('selected');
}

jQuery.MonoXMenu_DeselectTopLevel = function(parent) {
    $.MonoXMenu_GetParent(parent, 3).removeClass('selected');
}

jQuery.MonoXMenu_SelectOtherLevel = function(parent) {
    $($.MonoXMenu_GetParent(parent, 3).find('a')[0]).addClass('selected');
}

jQuery.MonoXMenu_DeselectOtherLevel = function(parent) {
    $($.MonoXMenu_GetParent(parent, 3).find('a')[0]).removeClass('selected');
}

function InitMonoXMenuSelectors() {
    $(".navigation .level0 li a").hover(function() { $.MonoXMenu_SelectTopLevel(this); }, function() { $.MonoXMenu_DeselectTopLevel(this); });
    $(".navigation .level1 li a").hover(
    function() {
        $.MonoXMenu_SelectTopLevel($.MonoXMenu_GetParent(this, 2)); 
        $.MonoXMenu_SelectOtherLevel(this);
    },
    function() {
        $.MonoXMenu_DeselectTopLevel($.MonoXMenu_GetParent(this, 2)); 
        $.MonoXMenu_DeselectOtherLevel(this);
    });
    $(".navigation .level2 li a").hover(
    function() {
        $.MonoXMenu_SelectTopLevel($.MonoXMenu_GetParent(this, 4));
        $.MonoXMenu_SelectOtherLevel($.MonoXMenu_GetParent(this, 2));
        $.MonoXMenu_SelectOtherLevel(this);
    },
    function() {
        $.MonoXMenu_DeselectTopLevel($.MonoXMenu_GetParent(this, 4));
        $.MonoXMenu_DeselectOtherLevel($.MonoXMenu_GetParent(this, 2));
        $.MonoXMenu_DeselectOtherLevel(this);
    });
}

//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }