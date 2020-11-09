C:\Factory\Tools\RDMD.exe /RC out

MD out\Data
MD out\Data\Elsa
MD out\Data\res

ROBOCOPY dat out\Data\Elsa /MIR
ROBOCOPY res out\Data\res /MIR

C:\Factory\SubTools\CallConfuserCLI.exe Elsa20200001\Elsa20200001\bin\Release\Elsa20200001.exe out\Elsa20200001.exe
rem COPY /B Elsa20200001\Elsa20200001\bin\Release\Elsa20200001.exe out
COPY /B Elsa20200001\Elsa20200001\bin\Release\DxLib.dll out
COPY /B Elsa20200001\Elsa20200001\bin\Release\DxLib_x64.dll out
COPY /B Elsa20200001\Elsa20200001\bin\Release\DxLibDotNet.dll out

C:\Factory\Tools\xcp.exe doc out
COPY /B AUTHORS out

C:\Factory\SubTools\zip.exe /PE- /RVE- %* /G out e20201108_YokoActTK
