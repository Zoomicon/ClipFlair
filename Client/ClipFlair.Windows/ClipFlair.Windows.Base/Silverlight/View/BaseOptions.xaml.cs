//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BaseOptions.xaml.cs
//Version: 20141031

using System.Windows;
using System.Windows.Controls;

namespace ClipFlair.Windows.Options
{

  public partial class BaseOptions : UserControl
  {

    #region --- Fields ---

    protected bool isTopLevel;

    #endregion

    #region --- Initialization ---

    public BaseOptions()
    {
      InitializeComponent();
    }

    #endregion

    #region --- Properties ---

    public bool IsTopLevel
    {
      get { return isTopLevel; }
      set
      {
        if (isTopLevel != value)
          isTopLevel = value;

        Visibility visibility = value ? Visibility.Collapsed : Visibility.Visible; //hide backpanel properties not relevant when not being a child window
        propX.Visibility = visibility;
        propY.Visibility = visibility;
        propWidth.Visibility = visibility;
        propHeight.Visibility = visibility;
        propZoom.Visibility = visibility;
        propMoveable.Visibility = visibility;
        propResizable.Visibility = visibility;
        propZoomable.Visibility = visibility;
      }
    }

    #endregion
  }

}
