::-- Syntax: MERGE 1HeaderWriter 2FilePattern 3ItemWriter 4FooterWriter 5OutputFile 

@echo off

::echo MERGE %*

::--------------------------------------------

::-- Delete any previous output file (don't want for loop below to grab and merge that too)
DEL %5 2> nul

::-- Write header (overwrite previous temp output file - using tilde at both sides to avoid for loop grabbing the temp output file)
CALL %1 > %5~

::-- Write files
FOR %%f IN (%2) DO CALL %3 %%f >> %5~

::-- Write footer
call %4 >> %5~

::-- Rename temp output file (have deleted any previous output file at start)
ren %5~ %~nx5

::pause
