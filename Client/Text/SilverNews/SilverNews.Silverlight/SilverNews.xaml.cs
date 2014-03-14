//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: SilverNews.xaml.cs
//Version: 20140314

using System;
using System.IO;
using System.Net;
using System.ServiceModel.Syndication;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace SilverNews
{
  public partial class SilverNews : UserControl
  {
    public SilverNews()
    {
      InitializeComponent();
      //DefaultStyleKey = typeof(SilverNews); //if we convert this to Control, need to do this
    }

    #region --- Properties ---

    #region Source
    public Uri Source
    {
      get { return (Uri)GetValue(SourceProperty); }
      set { SetValue(SourceProperty, value); }
    }

    public static readonly DependencyProperty SourceProperty =
      DependencyProperty.Register("Source", typeof(Uri), typeof(SilverNews), new PropertyMetadata(null, SourcePropertyChanged));

    private static void SourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
 	    if (e.OldValue != e.NewValue) { //is this really needed?
        SilverNews news = d as SilverNews;
        news.LoadRSS((Uri)e.NewValue);
      }
    }

    #endregion
    
    #region --- Methods ---

    public void LoadRSS(Uri uri)
    {
      if (uri == null) return;

      WebClient client = new WebClient();
      client.DownloadStringCompleted += (s, e) =>
      {
        if (!e.Cancelled) //note sure if this also handles download error cases
          try
          {
            using (XmlReader reader = XmlReader.Create(new StringReader(e.Result))) //Silverlight's XmlReader can only read content from inside the .xap distribution file if you pass a Uri to it
              RSSList.ItemsSource = SyndicationFeed.Load(reader).Items;
          }
          catch
          {
            RSSList.ItemsSource = null;
          }
        else
          RSSList.ItemsSource = null;
      };
      client.DownloadStringAsync(uri);
    }

    #endregion

    #endregion

  }

}