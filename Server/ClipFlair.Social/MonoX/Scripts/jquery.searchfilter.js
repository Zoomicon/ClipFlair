/* 
* MonoX search box filter utility.
* NOTE: This script requires jQuery to work.  Download jQuery at www.jquery.com
*
*/

$.extend({
    searchBoxFilter: function (containerId) {
        SetupMonoXSearchFilter(containerId);
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (s, e) {
                SetupMonoXSearchFilter(containerId);
            });
        }
    }

});

function SetupMonoXSearchFilter(containerId) {
        /**
        * the element
        */
    
        var ui = $("#" + containerId);
        /**
        * on focus and on click display the dropdown
        */
        ui.find('input:text').bind('click', function () {
            ui.find('.search-filter-list').show();
        });

        /**
        * on mouse leave hide the dropdown, 
        */
        ui.bind('mouseleave', function () {
            ui.find('.search-filter-list').hide();
        });

        /**
        * selecting all checkboxes
        */
        ui.find('.search-filter-list').find('input:checkbox:first').bind('click', function () {
            $(this).parent().siblings().find(':checkbox').attr('checked', this.checked).attr('disabled', this.checked);
        });

        /**
        * deselecting first checkbox on any other click
        */
        ui.find('.search-filter-list').find('input:checkbox').not(':first').bind('click', function () {
            ui.find('.search-filter-list').find('input:checkbox:first').attr('checked', false);
        });

}

//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }