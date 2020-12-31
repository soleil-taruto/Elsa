CALL qq
cx **

	CALL DebugRelease.bat /B
rem	CALL Release.bat /B
rem	CALL Release.bat /V 001

Tools\UpdatingCopy.exe out C:\be\Web\DocRoot\Elsa\d20201211_DoremyRockman
Tools\RunOnBatch.exe C:\be\Web Deploy.bat
