//Version: 20140317

using SliderExtLib;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorPickerLib
{
  public class ColorBoard : Control
  {
   
    #region --- Initialization ---

    public ColorBoard()
    {
      DefaultStyleKey = typeof(ColorBoard);
    }
    
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      rootElement = (FrameworkElement)GetTemplateChild("RootElement"); //TODO: use part constants and attributes

      canvasHSV = (Canvas)GetTemplateChild("CanvasHSV");
      rectangleRootHSV = (Rectangle)GetTemplateChild("RectangleRootHSV");
      gradientStopHSVColor = (GradientStop)GetTemplateChild("GradientStopHSVColor");
      rectangleHSV = (Rectangle)GetTemplateChild("RectangleHSV");
      ellipseHSV = (Ellipse)GetTemplateChild("EllipseHSV");
      SliderExtHSV = (SliderExt)GetTemplateChild("SliderExtHSV");

      SliderExtA = (SliderExt)GetTemplateChild("SliderExtA");
      gradientStopA0 = (GradientStop)GetTemplateChild("GradientStopA0");
      gradientStopA1 = (GradientStop)GetTemplateChild("GradientStopA1");
      SliderExtR = (SliderExt)GetTemplateChild("SliderExtR");
      gradientStopR0 = (GradientStop)GetTemplateChild("GradientStopR0");
      gradientStopR1 = (GradientStop)GetTemplateChild("GradientStopR1");
      SliderExtG = (SliderExt)GetTemplateChild("SliderExtG");
      gradientStopG0 = (GradientStop)GetTemplateChild("GradientStopG0");
      gradientStopG1 = (GradientStop)GetTemplateChild("GradientStopG1");
      SliderExtB = (SliderExt)GetTemplateChild("SliderExtB");
      gradientStopB0 = (GradientStop)GetTemplateChild("GradientStopB0");
      gradientStopB1 = (GradientStop)GetTemplateChild("GradientStopB1");

      textBoxA = (TextBox)GetTemplateChild("TextBoxA");
      textBoxR = (TextBox)GetTemplateChild("TextBoxR");
      textBoxG = (TextBox)GetTemplateChild("TextBoxG");
      textBoxB = (TextBox)GetTemplateChild("TextBoxB");

      comboBoxColor = (ComboBox)GetTemplateChild("ComboBoxColor");
      rectangleColor = (Rectangle)GetTemplateChild("RectangleColor");
      brushColor = (SolidColorBrush)GetTemplateChild("BrushColor");
      textBoxColor = (TextBox)GetTemplateChild("TextBoxColor");
      buttonDone = (Button)GetTemplateChild("ButtonDone");

      rectangleHSV.MouseLeftButtonDown += new MouseButtonEventHandler(HSV_MouseLeftButtonDown);
      rectangleHSV.MouseMove += new MouseEventHandler(HSV_MouseMove);
      rectangleHSV.MouseLeftButtonUp += new MouseButtonEventHandler(HSV_MouseLeftButtonUp);
      rectangleHSV.LostMouseCapture += new MouseEventHandler(HSV_LostMouseCapture);

      SliderExtHSV.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderExtHSV_ValueChanged);

      SliderExtA.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderExtA_ValueChanged);
      SliderExtR.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderExtR_ValueChanged);
      SliderExtG.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderExtG_ValueChanged);
      SliderExtB.ValueChanged += new RoutedPropertyChangedEventHandler<double>(SliderExtB_ValueChanged);

      textBoxA.LostFocus += new RoutedEventHandler(textBoxA_LostFocus);
      textBoxR.LostFocus += new RoutedEventHandler(textBoxR_LostFocus);
      textBoxG.LostFocus += new RoutedEventHandler(textBoxG_LostFocus);
      textBoxB.LostFocus += new RoutedEventHandler(textBoxB_LostFocus);

      comboBoxColor.SelectionChanged += new SelectionChangedEventHandler(comboBoxColor_SelectionChanged);
      textBoxColor.GotFocus += new RoutedEventHandler(textBoxColor_GotFocus);
      textBoxColor.LostFocus += new RoutedEventHandler(textBoxColor_LostFocus);
      buttonDone.Click += new RoutedEventHandler(buttonDone_Click);

      InitializePredefined();
      UpdateControls(Color, true, true, true);
    }

    private void InitializePredefined()
    {
      if (dictionaryColor != null)
      {
        return;
      }

      List<PredefinedColor> list = PredefinedColor.All;
      dictionaryColor = new Dictionary<Color, PredefinedColorItem>();
      foreach (PredefinedColor color in list)
      {
        PredefinedColorItem item = new PredefinedColorItem(color.Value, color.Name);
        comboBoxColor.Items.Add(item);

        if (!dictionaryColor.ContainsKey(color.Value))
        {
          dictionaryColor.Add(color.Value, item);
        }
      }
    }

    #endregion

    #region --- Fields ---

    #region UI controls

    private FrameworkElement rootElement = null;

    private Canvas canvasHSV;
    private Rectangle rectangleRootHSV;
    private GradientStop gradientStopHSVColor;
    private Rectangle rectangleHSV;
    private Ellipse ellipseHSV;

    private SliderExt SliderExtHSV;

    private SliderExt SliderExtA;
    private GradientStop gradientStopA0;
    private GradientStop gradientStopA1;

    private SliderExt SliderExtR;
    private GradientStop gradientStopR0;
    private GradientStop gradientStopR1;

    private SliderExt SliderExtG;
    private GradientStop gradientStopG0;
    private GradientStop gradientStopG1;

    private SliderExt SliderExtB;
    private GradientStop gradientStopB0;
    private GradientStop gradientStopB1;

    private TextBox textBoxA;
    private TextBox textBoxR;
    private TextBox textBoxG;
    private TextBox textBoxB;

    private ComboBox comboBoxColor;
    private Rectangle rectangleColor;
    private SolidColorBrush brushColor;
    private TextBox textBoxColor;

    private Button buttonDone;

    #endregion

    private Dictionary<Color, PredefinedColorItem> dictionaryColor;
    private bool trackingHSV;
    private int isUpdating;

    #endregion
    
    #region --- Properties ---

    #region Color

    public Color Color
    {
      get { return (Color)GetValue(ColorProperty); }
      set { SetValue(ColorProperty, value); }
    }
    
    public static readonly DependencyProperty ColorProperty = 
      DependencyProperty.RegisterAttached("Color", typeof(Color), typeof(ColorBoard), 
                                          new PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(OnColorPropertyChanged)));
    
    private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ColorBoard control = d as ColorBoard;
      if (control != null && control.rootElement != null)
      {
        if (control.Updating) return;

        Color color = (Color)e.NewValue;
        control.UpdateControls(color, true, true, true);
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
      DependencyProperty.RegisterAttached("IsDirectlyUpdating", typeof(bool), typeof(ColorBoard),
                         new PropertyMetadata(true, new PropertyChangedCallback(OnIsDirectlyUpdatingPropertyChanged)));

    private static void OnIsDirectlyUpdatingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ColorBoard control = d as ColorBoard;
      if (control != null && (bool)e.NewValue)
        control.OnColorChanged();
    }

    #endregion

    #endregion

    #region --- Events ---

    public event RoutedEventHandler ColorChanged;
    public event RoutedEventHandler DoneClicked;

    private void OnColorChanged()
    {
      if (ColorChanged != null)
        ColorChanged(this, new RoutedEventArgs());
    }
    
    private void OnDoneClicked()
    {
      if (DoneClicked != null)
        DoneClicked(this, new RoutedEventArgs());
    }

    private void HSV_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      rectangleHSV.CaptureMouse(); //there is always a chance this returns false, so ignore its result
      trackingHSV = true; //set this after CaptureMouse since on WPF it seems to always fire LostMouseCapture

      Point point = e.GetPosition(rectangleHSV);

      Size size = ellipseHSV.RenderSize;

      ellipseHSV.SetValue(Canvas.LeftProperty, point.X - ellipseHSV.ActualWidth / 2);
      ellipseHSV.SetValue(Canvas.TopProperty, point.Y - ellipseHSV.ActualHeight / 2);

      if (Updating)
        return;

      Color color = GetHSVColor();
      UpdateControls(color, false, true, true);
    }

    private void HSV_MouseMove(object sender, MouseEventArgs e)
    {
      if (trackingHSV && 
          new Rect(0, 0, rectangleHSV.ActualWidth, rectangleHSV.ActualHeight).Contains(e.GetPosition(rectangleHSV)))
      {
        Point point = e.GetPosition(rectangleHSV);
        Size size = ellipseHSV.RenderSize;

        ellipseHSV.SetValue(Canvas.LeftProperty, point.X - ellipseHSV.ActualWidth / 2);
        ellipseHSV.SetValue(Canvas.TopProperty, point.Y - ellipseHSV.ActualHeight / 2);

        if (Updating)
          return;

        Color color = GetHSVColor();
        UpdateControls(color, false, true, true);
      }
    }
    private void HSV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      rectangleHSV.ReleaseMouseCapture();
      trackingHSV = false; //always clear, since we might have not gotten mouse capture in the first place
    }

    private void HSV_LostMouseCapture(object sender, MouseEventArgs e)
    {
      trackingHSV = false;
    }
    
    private void SliderExtHSV_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (Updating) return;

      gradientStopHSVColor.Color = ColorHelper.HSV2RGB(e.NewValue, 1d, 1d);

      Color color = GetHSVColor();
      UpdateControls(color, false, true, true);
    }

    private void SliderExtA_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (Updating) return;

      Color color = GetRGBColor();
      UpdateControls(color, true, true, true);
    }
    private void SliderExtR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (Updating) return;

      Color color = GetRGBColor();
      UpdateControls(color, true, true, true);
    }
    private void SliderExtG_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (Updating) return;

      Color color = GetRGBColor();
      UpdateControls(color, true, true, true);
    }

    private void SliderExtB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (Updating) return;

      Color color = GetRGBColor();
      UpdateControls(color, true, true, true);
    }

    private void textBoxA_LostFocus(object sender, RoutedEventArgs e)
    {
      if (Updating) return;

      int value = 0;
      if (int.TryParse(textBoxA.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
      {
        SliderExtA.Value = value;
      }
    }

    private void textBoxR_LostFocus(object sender, RoutedEventArgs e)
    {
      if (Updating) return;

      int value = 0;
      if (int.TryParse(textBoxR.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
        SliderExtR.Value = value;
    }

    private void textBoxG_LostFocus(object sender, RoutedEventArgs e)
    {
      if (Updating) return;

      int value = 0;
      if (int.TryParse(textBoxG.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
        SliderExtG.Value = value;
    }
    private void textBoxB_LostFocus(object sender, RoutedEventArgs e)
    {
      if (Updating) return;

      int value = 0;
      if (int.TryParse(textBoxB.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
        SliderExtB.Value = value;
    }

    private void comboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (Updating) return;
 
      PredefinedColorItem coloritem = comboBoxColor.SelectedItem as PredefinedColorItem;
      if (coloritem != null)
        Color = coloritem.Color;
    }
    private void textBoxColor_GotFocus(object sender, RoutedEventArgs e)
    {
      if (Updating) return;

      try
      {
        BeginUpdate();

        comboBoxColor.SelectedItem = null;
        textBoxColor.Text = Color.ToString();
      }
      finally
      {
        EndUpdate();
      }
    }

    private void textBoxColor_LostFocus(object sender, RoutedEventArgs e)
    {
      if (Updating)
        return;

      string text = textBoxColor.Text.TrimStart('#');
      uint value = 0;
      if (uint.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
      {
        byte b = (byte)(value & 0xFF);
        value >>= 8;
        byte g = (byte)(value & 0xFF);
        value >>= 8;
        byte r = (byte)(value & 0xFF);
        value >>= 8;
        byte a = (byte)(value & 0xFF);

        if (text.Length <= 6)
        {
          a = 0xFF;
        }

        Color color = Color.FromArgb(a, r, g, b);
        Color = color;
      }
      else
        Color = Colors.White;
    }
    private void buttonDone_Click(object sender, RoutedEventArgs e)
    {
      OnDoneClicked();
    }

    #endregion

    #region --- Colorspaces ---

    private Color GetHSVColor()
    {
      double h = SliderExtHSV.Value;

      double x = (double)ellipseHSV.GetValue(Canvas.LeftProperty) + ellipseHSV.ActualWidth / 2;
      double y = (double)ellipseHSV.GetValue(Canvas.TopProperty) + ellipseHSV.ActualHeight / 2;

      double s = x / (rectangleHSV.ActualWidth - 1);
      if (s < 0d)
        s = 0d;
      else if (s > 1d)
        s = 1d;

      double v = 1 - y / (rectangleHSV.ActualHeight - 1);
      if (v < 0d)
        v = 0d;
      else if (v > 1d)
        v = 1d;

      return ColorHelper.HSV2RGB(h, s, v);
    }
   
    private Color GetRGBColor()
    {
      byte a = (byte)SliderExtA.Value;
      byte r = (byte)SliderExtR.Value;
      byte g = (byte)SliderExtG.Value;
      byte b = (byte)SliderExtB.Value;

      return Color.FromArgb(a, r, g, b);
    }

    #endregion

    #region --- Updating ---

    private void UpdateControls(Color color, bool hsv, bool rgb, bool predifined)
    {
      if (Updating)
      {
        return;
      }

      try
      {
        BeginUpdate();

        // HSV
        if (hsv)
        {
          double h = ColorHelper.GetHSV_H(color);
          double s = ColorHelper.GetHSV_S(color);
          double v = ColorHelper.GetHSV_V(color);

          SliderExtHSV.Value = h;
          gradientStopHSVColor.Color = ColorHelper.HSV2RGB(h, 1d, 1d);

          double x = s * (rectangleHSV.ActualWidth - 1);
          double y = (1 - v) * (rectangleHSV.ActualHeight - 1);

          ellipseHSV.SetValue(Canvas.LeftProperty, x - ellipseHSV.ActualWidth / 2);
          ellipseHSV.SetValue(Canvas.TopProperty, y - ellipseHSV.ActualHeight / 2);
        }

        if (rgb)
        {
          byte a = color.A;
          byte r = color.R;
          byte g = color.G;
          byte b = color.B;

          SliderExtA.Value = a;
          gradientStopA0.Color = Color.FromArgb(0, r, g, b);
          gradientStopA1.Color = Color.FromArgb(255, r, g, b);
          textBoxA.Text = a.ToString("X2");

          SliderExtR.Value = r;
          gradientStopR0.Color = Color.FromArgb(255, 0, g, b);
          gradientStopR1.Color = Color.FromArgb(255, 255, g, b);
          textBoxR.Text = r.ToString("X2");

          SliderExtG.Value = g;
          gradientStopG0.Color = Color.FromArgb(255, r, 0, b);
          gradientStopG1.Color = Color.FromArgb(255, r, 255, b);
          textBoxG.Text = g.ToString("X2");

          SliderExtB.Value = b;
          gradientStopB0.Color = Color.FromArgb(255, r, g, 0);
          gradientStopB1.Color = Color.FromArgb(255, r, g, 255);
          textBoxB.Text = b.ToString("X2");
        }

        if (predifined)
        {
          brushColor.Color = color;
          if (dictionaryColor.ContainsKey(color))
          {
            comboBoxColor.SelectedItem = dictionaryColor[color];
            textBoxColor.Text = "";
          }
          else
          {
            comboBoxColor.SelectedItem = null;
            textBoxColor.Text = color.ToString();
          }
        }

        Color = color;
      }
      finally
      {
        EndUpdate();
      }

      if (IsDirectlyUpdating)
        OnColorChanged();
    }

    private bool Updating
    {
      get { return isUpdating != 0; }
    }
    
    private void BeginUpdate()
    {
      isUpdating++;
    }
    private void EndUpdate()
    {
      isUpdating--;
    }

   #endregion

  }
}
