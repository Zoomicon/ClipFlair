/* 
* MonoX Social Networking / Wall Comments utility scripts
* NOTE: This script requires jQuery to work.  Download jQuery at www.jquery.com
*
*/

$(document).ready(function() {
    MonoXSilverlightUpload();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function(s, e) {
            MonoXSilverlightUpload();
        });
    }
});

function MonoXSilverlightUpload() {    
    $(".jq_uploadAction").unbind('click.toggle');
    $(".jq_uploadAction").bind('click.toggle', function() {        
        $(this).parents("div.jq_monoxUploadContainer").find("div.jq_silverlightUploadModuleBox").toggle();
    });

}

//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }