; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{9155E3BC-5803-44E7-88A0-45D9279F7DCD}
AppName=RGBDemoLinux
AppVersion=1.0
;AppVerName=RGBDemoLinux 1.0
AppPublisher=bencochoco
DefaultDirName={pf}\RGBDemoMac
DisableProgramGroupPage=yes
OutputDir=C:\Users\User\Desktop\RGB\Setup
OutputBaseFilename=SetupRGBDemoLinux
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\User\Desktop\RGB\Builds\Builds Linux\RGBDemo.x86_64"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\Desktop\RGB\Builds\Builds Linux\RGBDemo.x86"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\User\Desktop\RGB\Builds\Builds Linux\RGBDemo_Data\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\RGBDemoLinux"; Filename: "{app}\RGBDemo.x86_64"
Name: "{commondesktop}\RGBDemoLinux"; Filename: "{app}\RGBDemo.x86_64"; Tasks: desktopicon

[UninstallDelete]
Type: files; Name: "{app}\RGBDemo_Data\save.gd"

[Run]
Filename: "{app}\RGBDemo.x86_64"; Description: "{cm:LaunchProgram,RGBDemoLinux}"; Flags: shellexec postinstall skipifsilent

