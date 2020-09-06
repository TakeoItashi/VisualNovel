@echo on 
echo %1
WIX\heat.exe dir ".\output" -var var.output -cg OutputFilesGroup -ke -dr INSTALLFOLDER -gg -sfrag -o dynamic.wxs
WIX\candle build.wxs -o obj\
WIX\candle dynamic.wxs -d"INSTALLFOLDER=%1" -d"output=.\output" -o obj\
WIX\light obj\*.wixobj -b .\output -o installer\%2.msi
set /p=Hit ENTER to continue...