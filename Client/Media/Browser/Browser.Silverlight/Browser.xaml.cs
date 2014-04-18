//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: Browser.xaml.cs
//Version: 20140418

using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ClipFlair.Browser
{
  public partial class Browser : UserControl
  {
    public Browser()
    {
      InitializeComponent();
    }

    #region --- Properties ---

    #region Source

    public Uri Source
    {
      get { return (Uri)GetValue(SourceProperty); }
      set { SetValue(SourceProperty, value); }
    }

    public static readonly DependencyProperty SourceProperty =
      DependencyProperty.Register("Source", typeof(Uri), typeof(Browser), new PropertyMetadata(null /*, SourcePropertyChanged*/));

    /*
    private static void SourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Uri address = (Uri)e.NewValue;
      \*
      var html = new StringBuilder(@"<meta http-equiv=""X-UA-Compatible"" content=""IE=IE9"" />
                                     <html xmlns=""http://www.w3.org/1999/xhtml"" lang=""EN""> 
                                       <head> 
                                         <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /> 
                                         <style type=""text/css""> 
                                           html {overflow: auto;} 
                                           html, body, div, iframe {margin: 0px; padding: 0px; height: 100%; border: none;} 
                                           iframe {display: block; width: 100%; border: none; overflow-y: auto; overflow-x: hidden;} 
                                         </style> 
                                       </head> 
                                       <body> 
                                         <iframe security=""restricted"" src=""{@PageLink}"" frameborder=""0"" marginheight=""0"" marginwidth=""0"" width=""100%"" height=""100%"" scrolling=""auto""></iframe> 
                                       </body> 
                                     </html>"); //
      html.Replace("{@PageLink}", address.ToString());
     *\ 

      Browser self = (d as Browser);
      //self.web.NavigateToString(html.ToString());
      self.GoTo(address);
    }
    */

    #endregion

    #endregion

    #region --- Methods ---

    public void GoTo(Uri address)
    {
      Dispatcher.BeginInvoke(() => web.Source = address);
    }

    public void GoBack()
    {
      throw new NotImplementedException();
    }

    public void GoForward()
    {
      throw new NotImplementedException();
    }

    public static bool IsFocused(DependencyObject control)
    {
#if SILVERLIGHT
      return FocusManager.GetFocusedElement() == control; //In Silverlight 5, passing an element parameter value that is not of type Window will result in a returned value of null according to http://msdn.microsoft.com/en-us/library/ms604088(v=vs.95).aspx
#else
      return Keyboard.FocusedElement == control;
#endif
    }

    #endregion

    #region --- Events ---

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
      GoBack();
    }

    private void btnForward_Click(object sender, RoutedEventArgs e)
    {
      GoForward();
    }

    private void web_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
      //edAddress.Text = e.Uri.ToString();
    }

    //
    // Summary:
    //     Called before the System.Windows.UIElement.GotFocus event occurs.
    //
    // Parameters:
    //   e:
    //     The data for the event.
    protected override void OnGotFocus(RoutedEventArgs e)
    {
      base.OnGotFocus(e);
      web.Visibility = Visibility.Visible;
      webOverlay.Visibility = Visibility.Collapsed;
    }

    //
    // Summary:
    //     Called before the System.Windows.UIElement.LostFocus event occurs.
    //
    // Parameters:
    //   e:
    //     The data for the event.
    protected override void OnLostFocus(RoutedEventArgs e)
    {
      base.OnLostFocus(e);
      if (!IsFocused(edAddress) && !IsFocused(web) && !IsFocused(btn))
      {
        webOverlay.Visibility = Visibility.Visible;
        webBrush.Redraw();
        web.Visibility = Visibility.Collapsed;
      }
    }
    
    #endregion

  }
}
