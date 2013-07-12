//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: FlowDirectionToBooleanConverter.cs
//Version: 20130606

#if SILVERLIGHT

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Utils.Converters
{

  [ValueConversion(typeof(FlowDirection), typeof(bool))]
  public class FlowDirectionToBooleanConverter : ReverseConverter
  {
    
    public FlowDirectionToBooleanConverter() : base(new BooleanToFlowDirectionConverter())
    {

    }

  }

}

#endif