/*
Project: Amnesia of Who
Filename: ApplicationExtensions.cs
Version: 20150319
*/

#if WINDOWS_PHONE || SILVERLIGHT
using System;
using System.Reflection;
using System.Windows;
#if !WINDOWS_PHONE
using System.Windows.Browser;
#endif
#endif

namespace System.Windows
{
  public static class ApplicationExtensions
  {

    #if WINDOWS_PHONE || SILVERLIGHT

    public static void Shutdown(this Application app)
    {
      #if WINDOWS_PHONE
      
      if (Environment.OSVersion.Version.Major < 8) //try to load XNA assemblies (only working on WP7)
      {
        Assembly asmb = Assembly.Load("Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553");
        asmb = Assembly.Load("Microsoft.Xna.Framework.Game, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553");
        Type type = asmb.GetType("Microsoft.Xna.Framework.Game");
        object obj = type.GetConstructor(new Type[] { }).Invoke(new object[] { });

        type.GetMethod("Exit").Invoke(obj, new object[] { });
      }
      else //=> WP8
      {
        Type type = Application.Current.GetType();
        type.GetMethod("Terminate").Invoke(Application.Current, new object[] { });
      }

      #else //assuming !WINDOWS_PHONE && SILVERLIGHT

      if (Application.Current.IsRunningOutOfBrowser && Application.Current.HasElevatedPermissions) //TODO: see how it can be done without elevated permissions
        Application.Current.MainWindow.Close();
      else
        HtmlPage.Window.Eval("window.open('','_self'); window.close();"); //TODO: does this work?

      #endif
    }
    #endif

  }
}