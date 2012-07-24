The Uploading will be handled by FileUpoad.ashx inside the slAudioUpload.Web and the address of this file should be placed in MainPage.xaml.cs 
on the SL project

change FILE_UPLOADER_URL string at MailPage.xaml.cs with your own hosting address or localhost URL to point to "Upload.ashx"

If running from IIS, need to give "IIS APPPOOL\DefaultAppPool" user (or whatever AppPool you're running the Web Application under at IIS) to Uploads folder
If you can't find AppPool related users in your system using the pattern "IIS APPPOOL\<AppPoolName>", see http://serverfault.com/questions/81165/how-to-assign-permissions-to-applicationpoolidentity-account for more info

http://silvoicerecordupload.codeplex.com/ (also http://nokhodian.info/?p=12)
