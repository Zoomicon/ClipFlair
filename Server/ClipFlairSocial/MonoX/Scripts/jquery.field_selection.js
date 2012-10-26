/*
* jQuery plugin: fieldSelection - v0.1.0 - last change: 2006-12-16
* (c) 2006 Alex Brem <alex@0xab.cd> - http://blog.0xab.cd
*/

(function() {
    var fieldSelection = {
        getSelection: function() {
            var e = this.jquery ? this[0] : this;

            return (
            /* mozilla / dom 3.0 */
                ('selectionStart' in e && function() {
                    var l = e.selectionEnd - e.selectionStart;
                    return {
                        start: e.selectionStart,
                        end: e.selectionEnd,
                        length: l,
                        text: e.value.substr(e.selectionStart, l)
                    };
                })

            /* exploder */
                || (document.selection && function() {
                    e.focus();

                    var r = document.selection.createRange();
                    if (r == null) {
                        return {
                            start: 0,
                            end: e.value.length,
                            length: 0
                        };
                    }

                    var re = e.createTextRange();
                    var rc = re.duplicate();
                    re.moveToBookmark(r.getBookmark());
                    rc.setEndPoint('EndToStart', re);

                    // IE bug - it counts newline as 2 symbols when getting selection coordinates,
                    //  but counts it as one symbol when setting selection
                    var rcLen = rc.text.length,
                        i;
                    for (i = 0; i < rcLen; i++) {
                        if (rc.text.charAt(i) == '\n') rcLen--;
                    }
                    var rLen = r.text.length;
                    for (i = 0; i < rLen; i++) {
                        if (r.text.charAt(i) == '\n') rLen--;
                    }

                    return {
                        start: rcLen,
                        end: rcLen + rLen,
                        length: rLen,
                        text: r.text
                    };
                })

            /* browser not supported */
                || function() {
                    return {
                        start: 0,
                        end: e.value.length,
                        length: 0
                    };
                }

            )();

        },

        setSelection: function(start, end) {
            var e = document.getElementById($(this).attr('id')); // I don't know why... but $(this) don't want to work today :-/
            if (!e) {
                return $(this);
            } else if (e.setSelectionRange) { /* WebKit */
                e.focus(); e.setSelectionRange(start, end);
            } else if (e.createTextRange) { /* IE */
                var range = e.createTextRange();
                range.collapse(true);
                range.moveEnd('character', end);
                range.moveStart('character', start);
                range.select();
            } else if (e.selectionStart) { /* Others */
                e.selectionStart = start;
                e.selectionEnd = end;
            }

            return $(this);
        },

        replaceSelection: function() {
            var e = this.jquery ? this[0] : this;
            var text = arguments[0] || '';

            return (
            /* mozilla / dom 3.0 */
                ('selectionStart' in e && function() {
                    e.value = e.value.substr(0, e.selectionStart) + text + e.value.substr(e.selectionEnd, e.value.length);
                    return this;
                })

            /* exploder */
                || (document.selection && function() {
                    e.focus();
                    document.selection.createRange().text = text;
                    return this;
                })

            /* browser not supported */
                || function() {
                    e.value += text;
                    return this;
                }
            )();
        }
    };

    jQuery.each(fieldSelection, function(i) { jQuery.fn[i] = this; });

})();


$.fn.insertAtCaret = function(myValue) {
    return this.each(function() {
        //IE support
        if (document.selection) {
            this.focus();
            sel = document.selection.createRange();
            sel.text = myValue;
            this.focus();
        }
        //MOZILLA / NETSCAPE support
        else if (this.selectionStart || this.selectionStart == '0') {
            var startPos = this.selectionStart;
            var endPos = this.selectionEnd;
            var scrollTop = this.scrollTop;
            this.value = this.value.substring(0, startPos) + myValue + this.value.substring(endPos, this.value.length);
            this.focus();
            this.selectionStart = startPos + myValue.length;
            this.selectionEnd = startPos + myValue.length;
            this.scrollTop = scrollTop;
        } else {
            this.value += myValue;
            this.focus();
        }
    });
};

(function() {
    var caretfieldSelection = {

        saveCaretPos: function() {
            var range = $(this).getSelection();
            $(this)[0].caretPositionField = range.end;
        },
        loadCaretPos: function() {
            return $(this)[0].caretPositionField;
        }

    };

    jQuery.each(caretfieldSelection, function(i) { jQuery.fn[i] = this; });
})();

//DO NOT REMOVE - Note: Call the notifyScriptLoaded method in all file-based scripts (.js files) to indicate to the ScriptManager object that a referenced script has finished loading. - http://msdn.microsoft.com/en-us/library/bb310952(VS.90).aspx
try { if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); } catch (ex) { }