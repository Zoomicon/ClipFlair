//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: UrlDecodedToEncodedStringConverter.cs
//Version: 20130702

#if SILVERLIGHT

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Utils.Converters
{

  [ValueConversion(typeof(Visibility), typeof(bool))]
  public class UrlDecodedToEncodedStringConverter : ReverseConverter
  {
    
    public UrlDecodedToEncodedStringConverter() : base(new UrlEncodedToDecodedStringConverter())
    {

    }

  }

}

#endif