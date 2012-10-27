//Filename: WPF_ScaleTransform.cs
//Version: 20120811
//Author: George Birbilis <birbilis@kagi.com>

using System.Windows.Media;

namespace WPFCompatibility
{

  public static class WPF_ScaleTransform
  {

    public static ScaleTransform new_ScaleTransform(double scaleX, double scaleY) //unfortunately there are is no extension method mechanism for contructors in C# yet (and the ScaleTransform class is sealed so we can't create descendent class)
    {
#if SILVERLIGHT
      ScaleTransform result = new ScaleTransform();
      result.ScaleX = scaleX;
      result.ScaleY = scaleY;
#else
      ScaleTransform result = new ScaleTransform(scaleX, scaleY);
#endif
      return result;
    }

    public static ScaleTransform SetScale(this ScaleTransform transform, double scaleX, double scaleY) //can use this in both WPF and Silveright (the last one misses a parametric constructor) to initialize on the same statement on which we construct the ScaleTransform
    {
      transform.ScaleX = scaleX;
      transform.ScaleY = scaleY;
      return transform; //return the transform so that it can be used in the form ScaleTransform t = new ScaleTransform().SetScale(scaleX, scaleY)
    }

    public static ScaleTransform SetScale(this ScaleTransform transform, double scale) //can use this in both WPF and Silveright (the last one misses a parametric constructor) to initialize on the same statement on which we construct the ScaleTransform
    {
      return transform.SetScale(scale, scale);
    }

  }

}
