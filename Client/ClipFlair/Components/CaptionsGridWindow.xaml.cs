//Version: 20120730

using ClipFlair.Views;

using System;
using System.ComponentModel;

namespace ClipFlair.Components
{
  public partial class CaptionsGridWindow : FlipWindow
  {

    public CaptionsGridWindow()
    {
      InitializeComponent();
      DataContext = new CaptionsGridView();
    }

  }

}