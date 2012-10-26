/* 
* MonoX watermatk text utility for textbox and textarea fields.
* NOTE: This script requires jQuery to work.  Download jQuery at www.jquery.com
*
*/

$(document).ready(function() {
    PrettyPhotoStartup();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function(s, e) {
            PrettyPhotoStartup();
        });
    }

});



function PrettyPhotoStartup() {
    $(".ppt").remove();
    $(".pp_overlay").remove();
    $(".pp_pic_holder").remove();

    $("a[rel^='prettyPhoto']").prettyPhoto({
        social_tools: ''
    });
}




//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }