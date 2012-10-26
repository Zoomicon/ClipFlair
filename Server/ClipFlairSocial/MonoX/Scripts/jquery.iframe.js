//Copyright Nathan Smith: http://sonspring.com/journal/jquery-iframe-sizing

$(document).ready(function() {
    monox_resizeFrame();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function(s, e) {
        monox_resizeFrame();
        });
    }
});

function monox_resizeFrame() {
    // Set specific variable to represent all iframe tags.
    var iFrames = document.getElementsByTagName('iframe');

    // Resize heights.
    function iResize() {
        // Iterate through all iframes in the page.
        for (var i = 0, j = iFrames.length; i < j; i++) {
            // Set inline style to equal the body height of the iframed content.
            try {
                iFrames[i].style.width = '100%';
                iFrames[i].style.height = iFrames[i].contentWindow.document.body.offsetHeight + 'px';
            } catch (ex) { }
        }
    }

    // Check if browser is Safari or Opera.
    if ($.browser.safari || $.browser.opera) {
        // Start timer when loaded.
        $('iframe').load(function() {
            setTimeout(iResize, 0);
        }
			);

        // Safari and Opera need a kick-start.
        for (var i = 0, j = iFrames.length; i < j; i++) {
            var iSource = iFrames[i].src;
            iFrames[i].src = '';
            iFrames[i].src = iSource;
        }
    }
    else {
        // For other good browsers.
        $('iframe').load(function() {
            // Set inline style to equal the body height of the iframed content.
        try {
                this.style.width = '100%';
                this.style.height = this.contentWindow.document.body.offsetHeight + 'px';
            } catch (ex) { }
        });
    }
}

//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }