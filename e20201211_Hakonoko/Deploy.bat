CALL qq
cx **

rem	CALL DebugRelease.bat /B
	CALL Release.bat /B

Tools\UpdatingCopy.exe out C:\be\Web\DocRoot\Elsa\d20201210_Hakonoko
Tools\RunOnBatch.exe C:\be\Web Deploy.bat
