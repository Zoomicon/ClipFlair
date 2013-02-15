function ToggleEditorDisplay(divClientID, imgClientUrl, expandImageUrl, minimizeImageUrl) {

    var el = document.getElementById(divClientID) ;
    if( el.style.display=='none' ) {
        el.style.display='';
        document.images[imgClientUrl].src= minimizeImageUrl;
    } else {
        el.style.display='none';
        document.images[imgClientUrl].src= expandImageUrl;
    }
}




function DisplayDialog(url, opts, name) {
	if( opts == null || opts == "undefined" )
		opts = "width=300,height=250,resizable=yes,status=no,scrollbars=yes" ;
		
	var winName = (name) ? winName : "" ;
	
	if( DisplayDialog.arguments.length >= 2 )
		opts =  DisplayDialog.arguments[1] ;
		
	var hwnd = window.open( url, winName, opts ) ;
	if( (document.window != null ) && (!hwnd.opener) )
		hwnd.opener = document.window ;
		
	hwnd.moveTo( 80, 80 ) ;
}



function DoCatalogPostBack(postBackReference, returnValue) {
    eval(postBackReference.replace("[[WEBPART]]", returnValue));
}


//addEvent function found at http://www.scottandrew.com/weblog/articles/cbs-events
function addEvent(obj, evType, fn) 
{
    if (obj.addEventListener) {
	    obj.addEventListener(evType, fn, true);
	    return true;
    } else if (obj.attachEvent) {
	    var r = obj.attachEvent("on"+evType, fn);
	    return r;
    } else {
	    return false;
    }
}
    
function mouseLeaves (element, evt) 
{
    if (typeof evt.toElement != 'undefined' && typeof element.contains !='undefined') 
    {
        return !element.contains(evt.toElement);
    }
    else if (typeof evt.relatedTarget != 'undefined' && evt.relatedTarget) 
    {
        return !contains(element, evt.relatedTarget);
    }
}

function containsElement (container, element) 
{
    while (element) 
    {
        if (container == element) 
        {
            return true;
        }
        element = element.parentNode;
    }
    return false;
}

function hideElementById(elementId) 
{
    var element = document.getElementById(elementId);
    if (element)
        hideElement(element);
}

function hideElement (element) 
{
    if (element.style) 
    {
        element.style.visibility = 'hidden';
    }
}

function showElementById(elementId) 
{
    var element = document.getElementById(elementId);
    if (element)
        showElement(element);
}

function showElement (element) 
{
    if (element.style) 
    {
        element.style.visibility = 'visible';
    }
}

function hideElementDisplayById(elementId) 
{
    var element = document.getElementById(elementId);
    if (element)
        hideElementDisplay(element);
}

function hideElementDisplay (element) 
{
    if (element.style) 
    {
        element.style.display = 'none';
    }
}

function showElementDisplayById(elementId) 
{
    var element = document.getElementById(elementId);
    if (element)
        showElementDisplay(element);
}

function showElementDisplay (element) 
{
    if (element.style) 
    {
        element.style.display = 'block';
    }
}


function setFocusDelayed(element, delayedTime)
{
    setTimeout(function () { setFocus(element); }, delayedTime);
}

function setFocus(element)
{
    var elementObj = document.getElementById(element);  
    if (elementObj)
    {
        elementObj.setActive()
        try
        {
            elementObj.focus();
        }
        catch(er){ }
    }
}

window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oTitle) {
    var ev = mozEvent ? mozEvent : window.event; //Moz support requires passing the event argument manually 
    //Cancel the event 
    ev.cancelBubble = true;
    ev.returnValue = false;
    if (ev.stopPropagation) ev.stopPropagation();
    if (ev.preventDefault) ev.preventDefault();

    //Determine who is the caller 
    var callerObj = ev.srcElement ? ev.srcElement : ev.target;

    //Call the original radconfirm and pass it all necessary parameters 
    if (callerObj) {
        //Show the confirm, then when it is closing, if returned value was true, automatically call the caller's click method again. 
        var callBackFn = function(arg) {
            if (arg) {
                callerObj["onclick"] = "";                
                if ((callerObj.click) && (callerObj.tagName != "A")) {                    
                    $(callerObj).click(); //Works fine every time in IE, but does not work for links in Moz 
                }
                else if (callerObj.tagName == "A") //We assume it is a link button! 
                {
                    try {
                        eval(callerObj.href)
                    }
                    catch (e) { }
                }
            }
        }

        radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oTitle);
    }
    return false;
}

function monox_CloseRadPopup(radPopup) {
    radPopup.hide();
    return false;
}

function monox_OpenRadPopup(radPopup) {
    radPopup.show();
    return false;
}


//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try {if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }