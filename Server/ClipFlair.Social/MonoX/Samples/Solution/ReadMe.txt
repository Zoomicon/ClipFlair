1. Jump start a custom project

MonoX has a sample project that includes all neccessary references and folder structure, allowing you to jump start a custom project by adding a few pages and Web parts using Visual Studio, compiling it, 
and having a working portal in minutes. More complex projects will always be started this way. 

You can find the sample project in the "MonoX/Samples/Solution". To start using the sample project you need to copy the "ProjectName.sln" and "Portal.csproj" to the root of the web application.

2. Asp.Net 4.0 installation

Since version 4.0.2540 MonoX has a separate installation package for ASP.NET 4.0 and package can be found in standard MonoX download section at http://www.mono-software.com/Downloads/#MonoX.

3. Translate MonoX resources to your language

MonoX now includes a localization solution that will allow you to modify any of the MonoX resources and translate them to your language instantly. Before you start translating the resources we advise you to copy the whole localization solution to your custom location: from "MonoX/Samples/Solution/LocalizationSolution" to "YourCustomFolder/LocalizationSolution" (out of the MonoX CMS folder structure).

New versions of the MonoX CMS that you may upgrade to in the future will overwrite the localization solution folder with the new MonoX resources, and that is why we advise you to copy the whole localization soultion to your custom, protected location. When you do so please open "YourCustomFolder/LocalizationSolution/LocalizationSolution.sln" in the Visual Studio. You can open RESX files and translate the resources as usual. You can find more information on ASP.NET Globalization and Localization here: http://msdn.microsoft.com/en-us/library/c6zyy3s9.aspx

After you have translated the resources you need to build the project and navigate to the localization project's "bin" folder. Copy all files and subfolders (e.g. "en-US", "hr-HR", "tr-TR", "cs-CZ") to the MonoX bin folder and overwrite all of the MonoX original localization folders.

Restart the IIS application pool and refresh the portal page to get new, translated resources.