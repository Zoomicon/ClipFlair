//Filename: ActivityContainer.xaml.cs
//Version: 20120827

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Markup;
using SilverFlow.Controls;

namespace ClipFlair.Views
{
  public partial class ActivityContainer : UserControl
  {
    public ActivityContainer()
    {
      InitializeComponent();

      //floatHost.Add((FloatingWindow)XamlReader.Load("<clipflair:MediaPlayerWindow Width=\"Auto\" Height=\"Auto\" MinWidth=\"100\" MinHeight=\"100\" Title=\"4 Media Player\" IconText=\"1Media Player\" Tag=\"1MediaPlayer\" xmlns:clipflair=\"clr-namespace:ClipFlair;assembly=ClipFlair\"/>"));
      //floatHost.Add(new MediaPlayerWindow()).Show();

      //floatHost.FloatingWindows.First<FloatingWindow>().Show();
    }

  }
}
