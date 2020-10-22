C:\Factory\Tools\RDMD.exe /RC out

C:\Factory\SubTools\makeDDResourceFile.exe ^
	C:\Dat\Resource ^
	/SD Fairy\Donut3\General ^
	/SD Etoile\G4NovelAdv ^
	out\Resource.dat ^
	C:\Factory\Program\MaskGZDataForDonut3\MaskGZData.exe

C:\Factory\SubTools\makeDDResourceFile.exe ^
	res ^
	out\res.dat ^
	C:\Factory\Program\MaskGZDataForDonut3\MaskGZData.exe

C:\Factory\SubTools\CallConfuserCLI.exe G4NovelAdv\G4NovelAdv\bin\Release\G4NovelAdv.exe out\G4NovelAdv.exe
rem COPY /B G4NovelAdv\G4NovelAdv\bin\Release\G4NovelAdv.exe out
COPY /B G4NovelAdv\G4NovelAdv\bin\Release\Chocolate.dll out
COPY /B G4NovelAdv\G4NovelAdv\bin\Release\DxLib.dll out
COPY /B G4NovelAdv\G4NovelAdv\bin\Release\DxLib_x64.dll out
COPY /B G4NovelAdv\G4NovelAdv\bin\Release\DxLibDotNet.dll out

C:\Factory\Tools\xcp.exe doc out
C:\Factory\Tools\xcp.exe C:\Dev\Fairy\Donut3\doc out
COPY /B AUTHORS out

C:\Factory\SubTools\zip.exe /PE- /RVE- /G out G4NovelAdv
C:\Factory\Tools\summd5.exe /M out

IF NOT "%1" == "/-P" PAUSE
