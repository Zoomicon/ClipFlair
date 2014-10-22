//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: SilverNews.xaml.cs
//Version: 20141020

using System;
using System.IO;
using System.Net;
using System.ServiceModel.Syndication;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml;

namespace SilverNews
{
  public partial class SilverNews : UserControl
  {

    #region --- Constants ---

    public static readonly TimeSpan DEFAULT_REFRESH_INTERVAL = TimeSpan.Zero;

    #endregion

    #region --- Fields ---

    DispatcherTimer timer = new DispatcherTimer(); //do not use "private" here, want to access from static code (that has an instance of SilverNews) in local file

    #endregion

    #region --- Initialization ---
    public SilverNews()
    {
      InitializeComponent();
      //DefaultStyleKey = typeof(SilverNews); //if we convert this to a CustomControl, we need to do this

      InitTimer();
    }

    private void InitTimer()
    {
      timer.Interval = DEFAULT_REFRESH_INTERVAL;
      timer.Tick += Timer_Tick;
      if (DEFAULT_REFRESH_INTERVAL != TimeSpan.Zero) timer.Start(); else timer.Stop();
    }

    #endregion

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
      if (e.OldValue != e.NewValue)
      { //is this really needed?
        SilverNews news = d as SilverNews;
        news.LoadRSS((Uri)e.NewValue);
      }
    }

    #endregion

    #region RefreshInterval

    public TimeSpan RefreshInterval
    {
      get { return (TimeSpan)GetValue(RefreshIntervalProperty); }
      set { SetValue(RefreshIntervalProperty, value); }
    }

    public static readonly DependencyProperty RefreshIntervalProperty =
      DependencyProperty.Register("RefreshInterval", typeof(TimeSpan), typeof(SilverNews), new PropertyMetadata(DEFAULT_REFRESH_INTERVAL, RefreshIntervalPropertyChanged));

    private static void RefreshIntervalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (e.OldValue != e.NewValue)
      {
        SilverNews news = d as SilverNews;
        TimeSpan newValue = (TimeSpan)e.NewValue;
        news.timer.Interval = newValue;
        if (newValue != TimeSpan.Zero) news.timer.Start(); else news.timer.Stop();
      }
    }

    #endregion

    #endregion

    #region --- Methods ---

    public void Refresh()
    {
      if (Source == null) return;
      
      string url = Source.ToString();
      if (!url.Contains("?")) url += "?"; //add first URL parameter
      else if (url.Contains("&")) url += "&"; //add one more URL parameter   

      url += "p" + Guid.NewGuid().ToString() + "=1"; //the URL parameter has for name "p" + a GUID (statistically unique number) and a dummy value (1)

      LoadRSS(new Uri(url, UriKind.Absolute)); //refresh loading the RSS URL with a unique URL parameter, so that it doesn't get cached results
    }

    protected void LoadRSS(Uri uri)
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

    #region --- Events ---

    public void Timer_Tick(object sender, EventArgs e)
    {
      Refresh();
    }

    #endregion
  }

}