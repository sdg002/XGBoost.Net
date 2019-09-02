Set-StrictMode -Version "2.0"
cls

<#
    Overview
    --------
    This script will do a bulk coversion of SVG files to PNG
    InkScape should have been installed and the variable $pathInkScape initialized

    Why do we need this script?
    ---------------------------
    CodeProject does not allow linking of HTML files to SVG. Therefore these need to be coverted to PNG before publishing and HTML anchor tags updated


    Directory assumption:
    ---------------------

    root
        |
        -Images
                cool.svg
        |
    root
        |
        -powershell
                SvgToPng.ps1
        |
#>
$ErrorActionPreference="Stop"
#$folderWithPng=Read-Host -Prompt "Select folder with SVB files"
$pathInkscape="C:\Program Files\Inkscape\inkscape.exe"
$folderScript=$PSScriptRoot;
$folderWithPng=Join-Path -Path $folderScript -ChildPath "..\images"
$filesSVG = Get-ChildItem -Path $folderWithPng -Filter *.svg -Recurse
#$filesSVG
$count=0;
foreach ($fileSVG in $filesSVG)
{
    $count++;
    "Processing SVG '{0}'" -f $fileSVG.Name
    $folderOutput=[System.IO.Path]::GetDirectoryName($fileSVG.FullName)
    $fileName=("{0}.png" -f $fileSVG.Name)
    $pathOutputPng=(Join-Path -Path $folderOutput -ChildPath  $fileName)
    $arg=("{0} --export-png={1}" -f $fileSVG.FullName,$pathOutputPng)
    Start-Process -Wait -FilePath $pathInkscape -ArgumentList $arg
}
"{0} SVG files were converted to PNG" -f $count
#Read-Host -Prompt "Select folder with SVB files"
#