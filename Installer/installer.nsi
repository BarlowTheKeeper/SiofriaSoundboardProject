!include "MUI2.nsh"
!include "LogicLib.nsh"
!include "x64.nsh"

; The name of the installer
Outfile "Soundboard of Siofria.exe"

; The default installation directory
InstallDir $PROGRAMFILES\BarlowKeep\SoundboardOfSiofria

; The text to prompt the user for the installation directory
DirText "Please select the installation folder."

; Pages
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_INSTFILES

; Languages (you can add or remove languages here)
!insertmacro MUI_LANGUAGE "English"

Section "Desktop Shortcut"
    SetOutPath $INSTDIR
    CreateShortCut "$DESKTOP\SiofriaSoundboard.lnk" "$INSTDIR\SiofriaSoundboard.exe"
SectionEnd

Section "Taskbar Shortcut"
    SetOutPath $INSTDIR
    CreateShortCut "$QUICKLAUNCH\SiofriaSoundboard.lnk" "$INSTDIR\SiofriaSoundboard.exe"
SectionEnd

Section ".NET Windows Desktop Runtime"
    ${If} ${RunningX64}
        ExecWait '"$INSTDIR\redist\windowsdesktop-runtime-6.0.22-win-x64.exe"'
    ${Else}
        ExecWait '"$INSTDIR\redist\windowsdesktop-runtime-6.0.22-win-x86.exe"'
    ${EndIf}
SectionEnd

Section "Install SiofriaSoundboard"
    SetOutPath $INSTDIR
    File /r "..\SiofriaSoundboard\SiofriaSoundboard\bin\Debug\SoundboardOfSiofria\*.*"
    WriteUninstaller "$INSTDIR\SiofriaUninstall.exe"

SectionEnd

; Uninstaller
UninstallText "This will uninstall Soundboard of Siofria from your system."
UninstPage uninstConfirm
UninstPage instfiles

Section "Uninstall"
    Delete "$INSTDIR\*.*"
    RMDir "$INSTDIR"
    Delete "$DESKTOP\SiofriaSoundboard.lnk"
    Delete "$QUICKLAUNCH\SiofriaSoundboard.lnk"
SectionEnd