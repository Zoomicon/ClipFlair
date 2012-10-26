/* 
* MonoX watermatk text utility for textbox and textarea fields.
* NOTE: This script requires jQuery to work.  Download jQuery at www.jquery.com
*
*/

$(document).ready(function() {
    SwapValues();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function(s, e) {
            SwapValues();
        });
    }
});

function SwapValues() {
    swapValues = [];
    $(".jq_swap_value").each(
        function(i) {
            swapValues[i] = $(this).val();
            $(this).focus(function() {
                if ($(this).val() == swapValues[i]) {
                    $(this).val("")
                }
            }
            ).blur(function() {
                if ($.trim($(this).val()) == "") {
                    $(this).val(swapValues[i])
                }
            }
            )
        }
    )
}

//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }