//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: VisibilityToBooleanConverter.cs
//Version: 20121030

#if SILVERLIGHT

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ClipFlair.Utils.Converters
{

  [ValueConversion(typeof(Visibility), typeof(bool))]
  public class VisibilityToBooleanConverter : ReverseConverter
  {
    
    public VisibilityToBooleanConverter() : base(new BooleanToVisibilityConverter())
    {

    }

  }

}

#endif