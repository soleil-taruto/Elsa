C:\Factory\Tools\RDMD.exe /RC out

C:\Factory\SubTools\makeDDResourceFile.exe ^
	dat ^
	out\Resource.dat ^
	Tools\MaskGZData.exe

C:\Factory\SubTools\makeDDResourceFile.exe ^
	res ^
	out\res.dat ^
	Tools\MaskGZData.exe

C:\Factory\SubTools\CallConfuserCLI.exe Elsa20200001\Elsa20200001\bin\Release\Elsa20200001.exe out\Elsa20200001.exe
rem COPY /B Elsa20200001\Elsa20200001\bin\Release\Elsa20200001.exe out
COPY /B Elsa20200001\Elsa20200001\bin\Release\DxLib.dll out
COPY /B Elsa20200001\Elsa20200001\bin\Release\DxLib_x64.dll out
COPY /B Elsa20200001\Elsa20200001\bin\Release\DxLibDotNet.dll out

C:\Factory\Tools\xcp.exe doc out
COPY /B AUTHORS out

C:\Factory\SubTools\zip.exe /PE- /RVE- %* /G out e20201006_NovelAdv
