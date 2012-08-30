//Filename: FlipWindow.xaml.cs
//Version: 20120830

using SilverFlow.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace ClipFlair.Views
{
  [ContentProperty("FrontContent")]
  public partial class FlipWindow : FloatingWindow
  {
    public FlipWindow()
    {
      if (Application.Current.Host.Settings.EnableGPUAcceleration) //GPU acceleration can been turned on at HTML/ASPX page or at OOB settings for OOB apps
        CacheMode = new BitmapCache(); //must do this before setting the "Scale" property, since it will set "RenderAtScale" property of the BitmapCache

      ShowMaximizeButton = false; //!!! (till we fix it to resize to current visible view area and to allow moving the window in that case only [when it's not same size as parent])

      InitializeComponent(); //this can override the "ShowMaximize" button, or set the "Scale" property, so must do last

      HelpRequested += (s,e) => {
        MessageBox.Show("Help not available yet - see http://ClipFlair.net for contact info");
      };

      OptionsRequested += (s, e) =>
      {
        FlipPanel.IsFlipped = !FlipPanel.IsFlipped;
      };
    }

    public object FrontContent
    {
      get { return FlipPanel.FrontContent; }
      set { FlipPanel.FrontContent = value; }
    }

    public object BackContent //if one wants to replace the default backcontent that hosts the PropertiesPanel etc.
    {
      get { return FlipPanel.BackContent; }
      set { FlipPanel.BackContent = value; }
    }

    public UIElementCollection PropertyItems
    {
      get { return PropertiesPanel.Children; }
      set
      {
        PropertiesPanel.Children.Clear();
        foreach (UIElement item in value) { PropertiesPanel.Children.Add(item); }
      }
    }


  }

}
