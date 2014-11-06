del /q /f /s ..\installPack
del /q /f /s ..\installPack\*.*
del /q /f /s ..\installPack\*
del /q /f /s ..\installPack\quickDBExplorer
mkdir ..\installPack
mkdir ..\installPack\quickDBExplorer
copy .\bin\release\*.dll ..\installPack\quickDBExplorer
copy .\bin\release\quickDBExplorer.exe ..\installPack\quickDBExplorer
copy .\readme.txt ..\installPack\quickDBExplorer
copy .\license.txt ..\installPack\quickDBExplorer
rem copy .\ƒ‰ƒCƒZƒ“ƒX.rtf   ..\installPack\quickDBExplorer
copy .\AppIcon.ico ..\installPack\quickDBExplorer
copy .\quickDBExplorerHelp.htm ..\installPack\quickDBExplorer
xcopy /S/I/Y .\quickDBExplorerHelp.files ..\installPack\quickDBExplorer\quickDBExplorerHelp.files

pause

