call "%VS71COMNTOOLS%vsvars32.bat"

echo %1

if not "%1" == "Debug"  "%VCINSTALLDIR%\PreEmptive Solutions\Dotfuscator Community Edition\dotfuscator.exe"  quickDBExplorer.xml

