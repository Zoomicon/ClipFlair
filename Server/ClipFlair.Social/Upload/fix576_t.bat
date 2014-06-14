::-- BATCH file that creates an *_576_5.* file from an *_640_t.* one (copying it)
::-- Author: George Birbilis (http://zoomicon.com)
::-- Credits: String replacement based on http://www.dostips.com/DtTipsStringManipulation.php

@ECHO OFF

::-- Loop for all files recursively --::

FOR /R %%f in (*_640_t.*) DO CALL :process %%f

ECHO(
PAUSE

GOTO :EOF

::-- Per-file actions --::

:process

:: Display progress...
::ECHO Processing %*
<nul (set/p dummy=.)

SETLOCAL ENABLEDELAYEDEXPANSION
SET fromFile="%*"
SET toFile=!fromFile:_640_t=_576_t!

IF NOT EXIST %toFile% CALL :generate %fromFile% %toFile%

GOTO :EOF

::-- Generate missing file --::

:generate

ECHO(
ECHO COPY %*
COPY %*

::PAUSE

GOTO :EOF
