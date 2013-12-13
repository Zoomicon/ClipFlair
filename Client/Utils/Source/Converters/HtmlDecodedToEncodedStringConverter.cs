//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: HtmlDecodedToEncodedStringConverter.cs
//Version: 20130701

#if SILVERLIGHT

using System.Windows;
using System.Windows.Data;

namespace Utils.Converters
{

  [ValueConversion(typeof(Visibility), typeof(bool))]
  public class HtmlDecodedToEncodedStringConverter : ReverseConverter
  {
    
    public HtmlDecodedToEncodedStringConverter() : base(new HtmlEncodedToDecodedStringConverter())
    {

    }

  }

}

#endif