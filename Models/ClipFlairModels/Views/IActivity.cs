//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: IActivity.cs
//Version: 20121004

using System;

namespace ClipFlair.Models.Views
{

  public static class IActivityProperties
  {
    public const string PropertySource = "Source";
    public const string PropertyTime = "Time";
  }

  public static class IActivityDefaults
  {
    public static Uri DefaultSource = null;
    public static TimeSpan DefaultTime = TimeSpan.Zero;
  }
  
  public interface IActivity: IView
  {
    Uri Source { get; set; }
    TimeSpan Time { get; set; }
  }

}
