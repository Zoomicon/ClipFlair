//Version: 201207122
#if SILVERLIGHT

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Converters
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