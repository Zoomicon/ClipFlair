//Filename: TextEditorWindow.xaml.cs
//Version: 20120814

using ClipFlair.Views;

using System;
using System.Windows;

namespace ClipFlair.Components
{
    public partial class TextEditorWindow : FlipWindow
    {
        public TextEditorWindow()
        {
          View = new TextEditorView(); //must set the view first
          InitializeComponent();
        }

        #region View

        public TextEditorView View
        {
          get { return (TextEditorView)DataContext; }
          set { DataContext = value; }
        }

        #endregion

        #region Source

        /// <summary>
        /// Source Dependency Property
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(TextEditorWindow),
                new FrameworkPropertyMetadata((Uri)null, new PropertyChangedCallback(OnSourceChanged)));

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
          TextEditorWindow target = (TextEditorWindow)d;
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
