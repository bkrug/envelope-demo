# Run this script with XAS99 to assemble all files
# See https://endlos99.github.io/xdt99/
#
# If you can't run powershell scripts research this command locally:
# Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned
param([string]$mode='debug')

write-host 'Creating Cartridge'

if ($mode -ne 'release') {
    $mode = 'debug'
}

$fileList = 'VAR', 'MAIN', 'KSCAN', 'SELECT', 'MUSIC', 'HERTZ', 'TUNEMONTEVERDI', 'TUNETURKEY', 'TUNEOLDFOLKS', 'TUNEFARMER', 'VDP', 'DISPLAY', 'CHARPAT', 'TONETABLE'

#Deleting old work files
write-host 'Deleting old work files'
ForEach($file in $fileList) {
    $objFile = $file + '.obj'
    $lstfile = $file + '.lst'
    if (Test-Path $objFile) { Remove-Item $objFile }
    if (Test-Path $lstFile) { Remove-Item $lstFile }
}

#Auto-generate tunes
dotnet build .\MusicXmlParser\MusicXmlParser.sln

write-host 'Generating TUNEFARMER.asm'
.\MusicXmlParser\MusicXmlParser\bin\Debug\netcoreapp3.1\MusicXmlParser.exe `
    --input ".\Schumann_The_Merry_Farmer_Op._68_No._10.musicxml" `
    --output ".\TUNEFARMER.asm" `
    --asmLabel "SCHUMN" `
    --ratio60Hz "5:4" `
    --ratio50Hz "1:1" `
    --repetitionType "RepeatFromBeginning" `
    --displayRepoWarning 'true'
   
write-host 'Generating TUNETURKEY.asm'
.\MusicXmlParser\MusicXmlParser\bin\Debug\netcoreapp3.1\MusicXmlParser.exe `
    --input ".\Turkey_In_The_Straw_-_A_Ragtime_Fantasie.musicxml" `
    --output ".\TUNETURKEY.asm" `
    --asmLabel "OTTO" `
    --ratio60Hz "4:5" `
    --ratio50Hz "2:3" `
    --repetitionType "StopAtEnd" `
    --displayRepoWarning 'true'

write-host 'Generating TUNEOLDFOLKS.asm'
.\MusicXmlParser\MusicXmlParser\bin\Debug\netcoreapp3.1\MusicXmlParser.exe `
    --input ".\Old_Folks_At_Home_-_Theme_and_Variations_by_Stephen_Foster.musicxml" `
    --output ".\TUNEOLDFOLKS.asm" `
    --asmLabel "FOSTER" `
    --ratio60Hz "5:4" `
    --ratio50Hz "1:1" `
    --repetitionType "RepeatFromBeginning" `
    --displayRepoWarning 'true'

write-host 'Generating TUNEMONTEVERDI.asm'
.\MusicXmlParser\MusicXmlParser\bin\Debug\netcoreapp3.1\MusicXmlParser.exe `
    --input ".\Lasciate_i_monti.musicxml" `
    --output ".\TUNEMONTEVERDI.asm" `
    --asmLabel "MONTEV" `
    --ratio60Hz "15:11" `
    --ratio50Hz "75:66" `
    --repetitionType "StopAtEnd" `
    --displayRepoWarning 'true'    

#Assembling files
write-host 'Assembling source code'
ForEach($file in $fileList) {
    $asmFile = $file + '.asm'
    $lstFile = $file + '.lst'
    write-host '    ' $asmFile
    xas99.py $asmFile -S -R -L $lstFile
}

#Exit if assembly errors found
ForEach($file in $fileList) {
    $objFile = $file + '.obj'
    if (-not(Test-Path $objFile)) {
        exit
    }
}

#Link object files into cartridge
write-host 'Creating cartridge'
$outputCartridgeFile = 'musiceffectsC.bin'
xas99.py -b -a ">6000" -o $outputCartridgeFile -l `
    MAIN.obj `
    VAR.obj `
    MUSIC.obj `
    HERTZ.obj `
    TONETABLE.obj `
    KSCAN.obj `
    SELECT.obj `
    VDP.obj `
    DISPLAY.obj `
    CHARPAT.obj `
    TUNEMONTEVERDI.obj `
    TUNEOLDFOLKS.obj `
    TUNETURKEY.obj `
    TUNEFARMER.obj

#Create .rpk file for MAME
$zipFileName = ".\MusicEffects.zip"
$rpkFileName = ".\MusicEffects.rpk"
compress-archive -Path ".\layout.xml",$outputCartridgeFile $zipFileName -compressionlevel optimal
if (Test-Path $rpkFileName) { Remove-Item $rpkFileName }
Rename-Item $zipFileName $rpkFileName    

#Delete work files
write-host 'Deleting work files'
ForEach($file in $fileList) {
    $objFile = $file + '.obj'
    $lstfile = $file + '.lst'
    Remove-Item $objFile
    Remove-Item $lstFile
}
Remove-Item .\move-bank*.bin