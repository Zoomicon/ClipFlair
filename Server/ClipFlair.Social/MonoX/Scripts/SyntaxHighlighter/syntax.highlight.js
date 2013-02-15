
function monox_sh_SaveCaretPosition(controlId) {
    $('#' + controlId).saveCaretPos();
}

function monox_sh_InsertTag(controlId, code) {
    if ((code == null) || (code == '')) return;

    if (document.selection) {
        //Note: We need to fix the IE7 & IE8 issue with the focus beeing moved from the textbox when "Insert code" button is clicked
        var txtarea = $('#' + controlId);
        var monox_sh_textCaretPosition = $(txtarea).loadCaretPos();
        if (monox_sh_textCaretPosition > 0) {
            txtarea.setSelection(monox_sh_textCaretPosition, monox_sh_textCaretPosition);
        }
    }
    $('#' + controlId).insertAtCaret(code);
}

//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }