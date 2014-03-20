change FILE_UPLOADER_STORAGE_URL string at MailPage.xaml.cs with your own hosting address or localhost URL instead of MYSERVER

if you want to change the name of the Uploads folder, make sure you also rename temp/Uploads folder and change the StoredFiles setting at Global.asax.cs accordingly

If running from IIS, need to give "IIS APPPOOL\DefaultAppPool" user (or whatever AppPool you're running the Web Application under at IIS) to Uploads and temp/Uploads folders
If you can't find AppPool related users in your system using the pattern "IIS APPPOOL\<AppPoolName>", see http://serverfault.com/questions/81165/how-to-assign-permissions-to-applicationpoolidentity-account for more info
