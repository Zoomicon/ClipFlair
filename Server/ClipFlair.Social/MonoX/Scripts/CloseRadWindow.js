/*
* Closes Rad popup window
*
*/

function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow)
        oWindow = window.radWindow;
    else if (window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    return oWindow;
}

function CloseWindow() {    
    try
    {
        GetRadWindow().close();
        return;
    }
    catch (err) 
    {
    }
    try
    {
      GetRadWindow().Close(); 
    }
    catch (err)
    {
    
    }

}

window.RadWindowOpen = function(windowId, url) {
    setTimeout(function() { { window.radopen(url, windowId); } }, 500); return false;
}

//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }