//Filename: XamlReader.cs
//Version: 20130705
//Author: George Birbilis <zoomicon.com>

namespace WPF_Compatibility
{

  public static class XamlReader
  {
    
    // Summary:
    //     Parses a well-formed XAML fragment and creates a corresponding Silverlight
    //     object tree, and returns the root of the object tree.
    //
    // Parameters:
    //   xaml:
    //     A string that contains a valid XAML fragment.
    //
    // Returns:
    //     The root object of the object tree.
    public static object Load(string xaml) //from Silverlight API
    {
      #if SILVERLIGHT
      return System.Windows.Markup.XamlReader.Load(xaml);
      #else
      return System.Windows.Markup.XamlReader.Parse(xaml);
      #endif
    }

    // Summary:
    //     Reads the XAML input in the specified text string and returns an object that
    //     corresponds to the root of the specified markup.
    //
    // Parameters:
    //   xamlText:
    //     The input XAML, as a single text string.
    //
    // Returns:
    //     The root of the created object tree.
    public static object Parse(string xamlText) //from WPF API
    {
      return Load(xamlText);
    }

  }

}
