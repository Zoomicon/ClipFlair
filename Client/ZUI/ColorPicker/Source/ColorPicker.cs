//Version: 20140417

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorPickerLib
{
  public class ColorPicker : Control
  {

    #region --- Fields ---

    private FrameworkElement rootElement;

    private Button buttonDropDown;
    private Rectangle rectangleColor;
    private TextBlock textBlockColor;

    private Popup popupDropDown;
    private Canvas canvasOutside;
    private Canvas canvasOutsidePopup;
    private ColorBoard colorBoard; //field only accessible by code in this file

    #endregion

    #region --- Initialization ---

    public ColorPicker()
    {
      DefaultStyleKey = typeof(ColorPicker);
      InitializeColorBoard();
      #if SILVERLIGHT
      TabNavigation = KeyboardNavigationMode.Once;
      #else
      SetValue(KeyboardNavigation.TabNavigationProperty, KeyboardNavigationMode.Once); //in WPF, should expose this as a DependencyProperty of the control so that we can set it in XAML in the same way for both Silverlight and WPF
      #endif
    }

    private void InitializeColorBoard()
    {
      colorBoard = new ColorBoard();
      colorBoard.IsTabStop = true;
      //colorBoard.MouseLeftButtonDown += new MouseButtonEventHandler(ColorBoard_MouseLeftButtonDown);
      //colorBoard.KeyDown += new KeyEventHandler(ColorBoard_KeyDown);
      colorBoard.SizeChanged += new SizeChangedEventHandler(ColorBoard_SizeChanged);
      colorBoard.ColorChanged += new RoutedEventHandler(ColorBoard_ColorChanged);
      colorBoard.DoneClicked += new RoutedEventHandler(ColorBoard_DoneClicked);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      rootElement = GetTemplateChild("Root") as FrameworkElement; //TODO: use part constants and attributes

      buttonDropDown = GetTemplateChild("DropDownButton") as Button;
      popupDropDown = GetTemplateChild("Popup") as Popup;

      rectangleColor = (Rectangle)GetTemplateChild("RectangleColor");
      textBlockColor = (TextBlock)GetTemplateChild("TextBlockColor");

      if (buttonDropDown != null)
        buttonDropDown.Click += new RoutedEventHandler(buttonDropDown_Click);

      if (popupDropDown != null)
      {
        #if !SILVERLIGHT
        popupDropDown.PlacementTarget = buttonDropDown; //todo: this doesn't help either to show the popup in WPF
        popupDropDown.Placement = PlacementMode.Bottom;
        #endif

        if (canvasOutside == null)
        {
          canvasOutsidePopup = new Canvas();
          canvasOutsidePopup.Background = new SolidColorBrush(Colors.Transparent);
          canvasOutsidePopup.MouseLeftButtonDown += new MouseButtonEventHandler(canvasOutsidePopup_MouseLeftButtonDown);

          canvasOutside = new Canvas();
          canvasOutside.Children.Add(canvasOutsidePopup);
          canvasOutside.Children.Add(colorBoard);
        }
        popupDropDown.Child = canvasOutside;
      }

      UpdateControls();
    }

    #endregion

    #region --- Properties ---

    #region Color

    public Color Color
    {
      get { return (Color)GetValue(ColorProperty); }
      set { SetValue(ColorProperty, value); }
    }

    public static readonly DependencyProperty ColorProperty = DependencyProperty.RegisterAttached("Color", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(OnColorPropertyChanged)));

    private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ColorPicker item = d as ColorPicker;
      if (item != null)
      {
        item.UpdateControls();
        item.OnColorChanged();
      }
    }

    #endregion

    #region IsDirectlyUpdating

    public bool IsDirectlyUpdating
    {
      get { return (bool)GetValue(IsDirectlyUpdatingProperty); }
      set { SetValue(IsDirectlyUpdatingProperty, value); }
    }

    public static readonly DependencyProperty IsDirectlyUpdatingProperty = 
      DependencyProperty.RegisterAttached("IsDirectlyUpdating", typeof(bool), typeof(ColorPicker),
                                          new PropertyMetadata(true, new PropertyChangedCallback(OnIsDirectlyUpdatingPropertyChanged)));

    private static void OnIsDirectlyUpdatingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ColorPicker control = d as ColorPicker;
      if (control != null)
        control.colorBoard.IsDirectlyUpdating = (bool)e.NewValue;
    }

    #endregion

    #endregion

    #region --- Events ---

    public event RoutedEventHandler ColorChanged;

    private void OnColorChanged()
    {
      if (ColorChanged != null)
        ColorChanged(this, new RoutedEventArgs());
    }

    private void ColorBoard_ColorChanged(object sender, RoutedEventArgs e)
    {
      if (IsDirectlyUpdating) //ColorBoard should not be sending ColorChanged event when its IsDirectlyUpdating is set to fale, so this is just an extra safety check
        Color = colorBoard.Color;
    }

    private void ColorBoard_DoneClicked(object sender, RoutedEventArgs e)
    {
      Color = colorBoard.Color;
      popupDropDown.IsOpen = false;
    }

/*
    private void ColorBoard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
    }

    private void ColorBoard_KeyDown(object sender, KeyEventArgs e)
    {
    }
*/

    private void ColorBoard_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      SetPopupPosition();
    }

    private void SetPopupPosition()
    {
      if (colorBoard != null
          #if SILVERLIGHT
          && Application.Current != null && Application.Current.Host != null && Application.Current.Host.Content != null
          #endif
         )
      {
        #if SILVERLIGHT
        double contentheight = Application.Current.Host.Content.ActualHeight;
        double contentwidth = Application.Current.Host.Content.ActualWidth;
        #else
        double contentheight = Window.GetWindow(this).ActualHeight;
        double contentwidth = Window.GetWindow(this).ActualWidth;
        #endif

        double colorboardheight = colorBoard.ActualHeight;
        double colorboardwidth = colorBoard.ActualWidth;
        
        double pickerheight = ActualHeight;
        double pickerwidth = ActualWidth;

        if (rootElement != null)
        {
          GeneralTransform transform = new MatrixTransform();

          if (transform != null)
          {
            Point point00 = transform.Transform(new Point(0.0, 0.0));
            Point point10 = transform.Transform(new Point(1.0, 0.0));
            Point point01 = transform.Transform(new Point(0.0, 1.0));

            double x00 = point00.X;
            double y00 = point00.Y;
            double x = x00;
            double y = y00 + pickerheight;

            if (contentheight < (y + colorboardheight))
              y = y00 - colorboardheight;
            if (contentwidth < (x + colorboardwidth))
              x = x00 + pickerwidth - colorboardwidth;

            popupDropDown.HorizontalOffset = 0.0;
            popupDropDown.VerticalOffset = 0.0;

            canvasOutsidePopup.Width = contentwidth;
            canvasOutsidePopup.Height = contentheight;

            colorBoard.HorizontalAlignment = HorizontalAlignment.Left;
            colorBoard.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetLeft(colorBoard, x - x00);
            Canvas.SetTop(colorBoard, y - y00);

            Matrix identity = Matrix.Identity;
            identity.M11 = point10.X - point00.X;
            identity.M12 = point10.Y - point00.Y;
            identity.M21 = point01.X - point00.X;
            identity.M22 = point01.Y - point00.Y;
            identity.OffsetX = point00.X;
            identity.OffsetY = point00.Y;

            MatrixTransform matrixtransform = new MatrixTransform() { Matrix = identity };
            canvasOutsidePopup.RenderTransform = (Transform)matrixtransform.Inverse;
          }
        }
      }    
    }

    private void buttonDropDown_Click(object sender, RoutedEventArgs e)
    {
      colorBoard.Color = Color;
      popupDropDown.IsOpen = true;
    }

    private void canvasOutsidePopup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      popupDropDown.IsOpen = false;
    }

    #endregion

    #region --- Updating ---
    
    private void UpdateControls()
    {
      if (rectangleColor != null)
        if (rectangleColor.Fill is SolidColorBrush)
          rectangleColor.Fill = ColorHelper.ChangedColor((SolidColorBrush)rectangleColor.Fill, Color);
        else
          rectangleColor.Fill = new SolidColorBrush(Color);

      if (textBlockColor != null)
        textBlockColor.Text = PredefinedColor.GetColorName(Color);
    }

    #endregion

  }
}
