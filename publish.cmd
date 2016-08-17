@ECHO OFF

set PROJECTPATH=%1
set WEBSITENAME=%2
set WEBUSERNAME=%3
set WEBPASSWORD=%4

set SERVERNAME=https://%WEBSITENAME%.scm.azurewebsites.net/msdeploy.axd?site=%WEBSITENAME%

"C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe" -verb:sync -source:contentPath="%PROJECTPATH%" -dest:contentPath="%WEBSITENAME%",ComputerName="%SERVERNAME%",UserName="%WEBUSERNAME%",Password="%WEBPASSWORD%",IncludeAcls="False",AuthType="Basic" -enablerule:AppOffline
