CALL qq
cx **

rem	CALL DebugRelease.bat /B
	CALL Release.bat /B

Tools\UpdatingCopy.exe out C:\be\Web\DocRoot\Elsa\d20201122_YokoActTK
Tools\RunOnBatch.exe C:\be\Web Deploy.bat
