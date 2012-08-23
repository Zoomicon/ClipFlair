//Filename: ImageWindow.xaml.cs
//Version: 20120823

using ClipFlair.Views;
using ClipFlair.Models.Views;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClipFlair.Components
{
  public partial class ImageWindow : FlipWindow
    {
        public ImageWindow()
        {
          View = new ImageView(); //must set the view first
          InitializeComponent();
        }

        #region View

        public ImageView View
        {
          get { return (ImageView)DataContext; }
          set {
            //remove property changed handler from old view
            if (DataContext!=null)
              ((INotifyPropertyChanged)DataContext).PropertyChanged -= new PropertyChangedEventHandler(View_PropertyChanged);
            //add property changed handler to new view
            value.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
            //set the new view
            DataContext = value;
          }
        }

        protected void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
          if (e.PropertyName.Equals(IImageViewerProperties.PropertySource))
          {
            Source = View.Source;
          }
        }

        #endregion

        #region Source

        /// <summary>
        /// Source Dependency Property
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(IImageViewerProperties.PropertySource, typeof(Uri), typeof(ImageWindow),
                new FrameworkPropertyMetadata((Uri)IImageViewerDefaults.DefaultSource, new PropertyChangedCallback(OnSourceChanged)));

        /// <summary>
        /// Gets or sets the Source property.
        /// </summary>
        public Uri Source
        {
          get { return (Uri)GetValue(SourceProperty); }
          set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Source property.
        /// </summary>
        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
          ImageWindow target = (ImageWindow)d;
          target.OnSourceChanged((Uri)e.OldValue, target.Source);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the IsAvailable property.
        /// </summary>
        protected virtual void OnSourceChanged(Uri oldSource, Uri newSource)
        {
          View.Source = newSource;
        }

        #endregion
      
    }
}
