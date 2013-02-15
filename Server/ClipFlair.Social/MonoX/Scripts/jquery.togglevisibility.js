/* 
* MonoX toggle element visibility scripts.
* NOTE: This script requires jQuery to work.  Download jQuery at www.jquery.com
*/
var monox_tv_ElementState = new Array();

if (!Array.prototype.remove) {
    Array.prototype.remove = function(from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };
}

if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function(elt /*, from*/) {
        var len = this.length;

        var from = Number(arguments[1]) || 0;
        from = (from < 0)
         ? Math.ceil(from)
         : Math.floor(from);
        if (from < 0)
            from += len;

        for (; from < len; from++) {
            if (from in this &&
          this[from] === elt)
                return from;
        }
        return -1;
    };
}

function monox_tv_AddToElementState(elems) {
    if (elems.length > 0) {
        var elIdx = 0;
        for (; elIdx < elems.length; elIdx++) {

            //Find the Id of the element (if there is no id on the element itself the use the first parent with id)
            var elemTemp = $(elems[elIdx]).parent();
            var elemId = $(elemTemp).attr("id");
            while (elemId == '') {
                elemTemp = $(elemTemp).parent();
                if (elemTemp.length == 0) break;
                elemId = $(elemTemp).attr("id");
            }

            var key = elemId + "-" + $(elems[elIdx]).is(":visible");
            //Clear old values
            var from = 0;
            for (; from < monox_tv_ElementState.length; from++) {
                if (from in monox_tv_ElementState) {
                    if ((monox_tv_ElementState[from].indexOf(elemId) > -1) && (monox_tv_ElementState[from] != key))
                        monox_tv_ElementState.remove(from);
                }
            }

            var fromIdx = monox_tv_ElementState.indexOf(key);
            if (fromIdx == -1)
                monox_tv_ElementState.push(key);
        }
    }
}

function monox_tv_InitElementState(elementToToggleClass) {
    var from = 0;
    for (; from < monox_tv_ElementState.length; from++) {
        if (from in monox_tv_ElementState) {
            var keyValue = monox_tv_ElementState[from].split("-");
            var elems = $("#" + keyValue[0]).find("." + elementToToggleClass);
            if (keyValue[1] == 'true')
                $(elems).show();
            else
                $(elems).hide();
        }
    }
}


$.extend({
    /* used in scenarios where there can be only one elementToToggle, otherwise all elements with class elementToToggleClass will be toggled */
    registerToggleSingleElementVisibility: function(clickElementClass, elementToToggleClass, elementToFocusClass, showOnInit) {
        if (!(showOnInit != null && showOnInit == true))
            $("." + elementToToggleClass).hide();
        $.toggleSingleElementVisibility(clickElementClass, elementToToggleClass, elementToFocusClass);
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {

            prm.add_beginRequest(function(s, e) {
                var elems = $("." + elementToToggleClass);
                monox_tv_AddToElementState(elems);
            });

            prm.add_endRequest(function(s, e) {
                monox_tv_InitElementState(elementToToggleClass);
                $.toggleSingleElementVisibility(clickElementClass, elementToToggleClass, elementToFocusClass);
            });
        }
    },

    toggleSingleElementVisibility: function(clickElementClass, elementToToggleClass, elementToFocusClass) {
        $("." + clickElementClass).unbind('click');
        $("." + clickElementClass).bind('click', function() {
            $("." + elementToToggleClass).toggle("normal");
            if (elementToFocusClass != null)
                $("." + elementToToggleClass).children("." + elementToFocusClass).focus();
        });
    },

    /* used in scenarios where there can be multiple elements to toggle, so element with containerElementClass determine which element precisely should be toggled */
    registerToggleMultiElementVisibility: function(clickElementClass, elementToToggleClass, containerElementClass, elementToFocusClass) {
        $("." + containerElementClass).find("." + elementToToggleClass).hide();
        $.toggleMultiElementVisibility(clickElementClass, elementToToggleClass, containerElementClass, elementToFocusClass);
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {

            prm.add_beginRequest(function(s, e) {

                var elems = $("." + containerElementClass).find("." + elementToToggleClass);
                monox_tv_AddToElementState(elems);
            });

            prm.add_endRequest(function(s, e) {
                monox_tv_InitElementState(elementToToggleClass);
                $.toggleMultiElementVisibility(clickElementClass, elementToToggleClass, containerElementClass, elementToFocusClass);
            });
        }
    },

    toggleMultiElementVisibility: function(clickElementClass, elementToToggleClass, containerElementClass, elementToFocusClass) {
        $("." + clickElementClass).unbind('click');
        $("." + clickElementClass).bind('click', function() {
            $(this).parents("." + containerElementClass).find("." + elementToToggleClass).toggle();
            if (elementToFocusClass != null)
                $(this).parents("." + containerElementClass).find("." + elementToToggleClass).children("." + elementToFocusClass).focus();

        });
    },

    /* registers element for Facebook style resize behavior */
    registerAutoGrow: function(autoGrowElementClass) {
        $("." + autoGrowElementClass).growfield();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function(s, e) {
                $("." + autoGrowElementClass).growfield();
            });
        }
    }




});




//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }