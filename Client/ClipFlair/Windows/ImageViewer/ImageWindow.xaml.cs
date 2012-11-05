//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ImageWindow.xaml.cs
//Version: 20121102

using ClipFlair.Windows.Views;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClipFlair.Windows
{
  public partial class ImageWindow : BaseWindow
    {
        public ImageWindow()
        {
          View = new ImageView(); //must set the view first
          InitializeComponent();
        }

        #region View

        public new IImageViewer View //hiding parent property
        {
          get { return (IImageViewer)base.View; } //delegating to parent property
          set { base.View = value; }
        }

        protected override void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
          base.View_PropertyChanged(sender, e);

          if (e.PropertyName == null) //multiple (not specified) properties have changed, consider all as changed
          {
            Source = View.Source;
            //...
          }
          else switch (e.PropertyName) //string equality check in .NET uses ordinal (binary) comparison semantics by default
            {
              case IImageViewerProperties.PropertySource:
                Source = View.Source;
                break;
              //...
            }
        }

        #endregion

        #region Source

        /// <summary>
        /// Source Dependency Property
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(IImageViewerProperties.PropertySource, typeof(Uri), typeof(ImageWindow),
                new FrameworkPropertyMetadata(IImageViewerDefaults.DefaultSource, new PropertyChangedCallback(OnSourceChanged)));

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
          //imgContent.Source = new BitmapImage(Source); //This isn't used since we use data-binding (one-way) to update the image's "Source" property in XAML //TODO: Blog about not binding twp-way to Source (or using dot syntax to bind to ImageSource)
        }

        #endregion
      
    }
}
