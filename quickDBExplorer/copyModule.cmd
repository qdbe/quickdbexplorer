del /q /f /s ..\installPack

del /q /f /s ..\installPack\*.*

del /q /f /s ..\installPack\*

del /q /f /s ..\installPack\quickDBExplorer
del /q /f /s ..\installPack\quickDBExplorer\net35
del /q /f /s ..\installPack\quickDBExplorer\net40

mkdir ..\installPack
mkdir ..\installPack\quickDBExplorer
mkdir ..\installPack\quickDBExplorer\net35
mkdir ..\installPack\quickDBExplorer\net40


copy .\bin\net35\release\*.dll ..\installPack\quickDBExplorer\net35\
copy .\bin\net35\release\quickDBExplorer.exe ..\installPack\quickDBExplorer\net35\
copy .\readme.txt ..\installPack\quickDBExplorer\net35\
copy .\license.txt ..\installPack\quickDBExplorer\net35\
rem copy .\ライセンス.rtf   ..\installPack\quickDBExplorer\net35\
copy .\AppIcon.ico ..\installPack\quickDBExplorer\net35\
copy .\quickDBExplorerHelp.htm ..\installPack\quickDBExplorer\net35\
xcopy /S/I/Y .\quickDBExplorerHelp.files ..\installPack\quickDBExplorer\net35\quickDBExplorerHelp.files

copy .\bin\net40\release\*.dll ..\installPack\quickDBExplorer\net40\
copy .\bin\net40\release\quickDBExplorer.exe ..\installPack\quickDBExplorer\net40\
copy .\readme.txt ..\installPack\quickDBExplorer\net40\
copy .\license.txt ..\installPack\quickDBExplorer\net40\
rem copy .\ライセンス.rtf   ..\installPack\quickDBExplorer\net40\
copy .\AppIcon.ico ..\installPack\quickDBExplorer\net40\
copy .\quickDBExplorerHelp.htm ..\installPack\quickDBExplorer\net40\
xcopy /S/I/Y .\quickDBExplorerHelp.files ..\installPack\quickDBExplorer\net40\quickDBExplorerHelp.files

pause

