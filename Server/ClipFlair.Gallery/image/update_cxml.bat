:: Version: 20161213
:: Author: George Birbilis <birbilis@kagi.com>

@echo off


call :process 2>&1 > update_cxml.log
goto :EOF


:process

ECHO ---- Updating collection and DeepZoom assets

c:\programs\pauthor\pauthor.exe /source cxml images.cxml /html-template template.html /target deepzoom ..\collection\images.cxml && (goto OK) || (goto Fail)
goto :EOF


:OK
echo OK
::start http://gallery.clipflair.net/image
goto :EOF


:Fail
echo FAILED
goto :EOF
