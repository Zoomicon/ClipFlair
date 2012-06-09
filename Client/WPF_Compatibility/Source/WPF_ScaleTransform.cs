//Filename: WPF_ScaleTransform
//Version: 20120610
//Author: George Birbilis <birbilis@kagi.com>

using System.Windows.Media;

namespace WPFCompatibility
{
    
    public static class WPF_ScaleTransform
    {

      public static ScaleTransform new_ScaleTransform(double scaleX, double scaleY) //unfortunately there are is no extension method mechanism for contructors in C# yet (and the ScaleTransform class is sealed so we can't extend it)
        {
                    ScaleTransform result = new ScaleTransform();
                    result.ScaleX = scaleX;
                    result.ScaleY = scaleY;
                    return result;
        }

    }

}
