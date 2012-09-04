//Filename: ActivityContainer.xaml.cs
//Version: 20120904

//using SilverFlow.Controls;

using System.Windows.Controls;
using System.Windows.Data;

namespace ClipFlair.Views
{
  public partial class ActivityContainer : UserControl
  {
    public ActivityContainer()
    {
      InitializeComponent();

      //zuiContainer.Add((FloatingWindow)XamlReader.Load("<clipflair:MediaPlayerWindow Width=\"Auto\" Height=\"Auto\" MinWidth=\"100\" MinHeight=\"100\" Title=\"4 Media Player\" IconText=\"1Media Player\" Tag=\"1MediaPlayer\" xmlns:clipflair=\"clr-namespace:ClipFlair;assembly=ClipFlair\"/>"));
      //zuiContainer.Add(new MediaPlayerWindow()).Show();

      //zuiContainer.FloatingWindows.First<FloatingWindow>().Show();

      BindWindows(); //Bind MediaPlayerWindows's Time to CaptionGridWindows's Time programmatically (since using x:Name on the controls doesn't seem to work [components are resolved to null when searched by name - must have to do with the implementation of the Windows property of ZUI container])
    }

    private void BindWindows()
    {
      Binding timeBinding = new Binding("Time");
      timeBinding.Source = FindWindow("MediaPlayer");
      timeBinding.Mode = BindingMode.TwoWay;
      BindingOperations.SetBinding(FindWindow("Captions"), CaptionsGridWindow.TimeProperty, timeBinding);
    }

    public FlipWindow FindWindow(string tag) //need this since floating windows are not added in the XAML visual tree by the FloatingWindowHostZUI.Windows property (maybe should have FloatingWindowHostZUI inherit 
    {
      foreach (FlipWindow w in zuiContainer.Windows)
        if (tag == (string)w.Tag) return w; //must cast to string to compare (else we compare object references, since Tag property is of type object, not string)
      return null;
    }

  }
  
}
