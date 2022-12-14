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

$fileList = 'VAR', 'MAIN', 'KSCAN', 'SELECT', 'MUSIC', 'TUNEMARIOW', 'TUNEFURELISE', 'VDP', 'DISPLAY', 'GROM', 'TONETABLE'

#Deleting old work files
write-host 'Deleting old work files'
ForEach($file in $fileList) {
    $objFile = $file + '.obj'
    $lstfile = $file + '.lst'
    if (Test-Path $objFile) { Remove-Item $objFile }
    if (Test-Path $lstFile) { Remove-Item $lstFile }
}

#Auto-generate tune
.\MuseScoreParser\MuseScoreParser\bin\Debug\netcoreapp3.1\MuseScoreParser.exe `
    ".\Fr_Elise_SN76489.musicxml" `
    ".\TUNEFURELISE.asm" `
    "BEETHV" `
    "2:1" `
    "10:6"

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
xas99.py -b -a ">6000" -o musiceffectsC.bin -l `
    MAIN.obj `
    VAR.obj `
    MUSIC.obj `
    TONETABLE.obj `
    TUNEMARIOW.obj `
    KSCAN.obj `
    SELECT.obj `
    VDP.obj `
    DISPLAY.obj `
    GROM.obj `
    TUNEFURELISE.obj

#Delete work files
write-host 'Deleting work files'
ForEach($file in $fileList) {
    $objFile = $file + '.obj'
    $lstfile = $file + '.lst'
    Remove-Item $objFile
    Remove-Item $lstFile
}
Remove-Item .\move-bank*.bin