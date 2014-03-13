using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorPickerLib
{
    public class PredefinedColorItem : Control
    {
        public static readonly DependencyProperty ColorProperty = DependencyProperty.RegisterAttached("Color", typeof(Color), typeof(PredefinedColorItem), new PropertyMetadata(new PropertyChangedCallback(PredefinedColorItem.OnColorPropertyChanged)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text", typeof(string), typeof(PredefinedColorItem), new PropertyMetadata(new PropertyChangedCallback(PredefinedColorItem.OnTextPropertyChanged)));

        private static void OnBrushFillPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PredefinedColorItem item = d as PredefinedColorItem;
            if (item != null)
            {
                item.UpdateControls();
            }
        }
        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public PredefinedColorItem()
        {
            this.DefaultStyleKey = typeof(PredefinedColorItem);
        }

        public PredefinedColorItem(Color color, string text)
            : this()
        {
            Color = color;
            Text = text;
        }

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        protected Rectangle rectangleColor;
        protected TextBlock textBlockColor;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            rectangleColor = (Rectangle)GetTemplateChild("RectangleColor");
            textBlockColor = (TextBlock)GetTemplateChild("TextBlockColor");

            UpdateControls();
        }

        private void UpdateControls()
        {
            if (rectangleColor != null)
            {
                if (rectangleColor.Fill is SolidColorBrush)
                {
                    rectangleColor.Fill = ColorHelper.ChangedColor((SolidColorBrush)rectangleColor.Fill, Color);
                }
                else
                {
                    rectangleColor.Fill = new SolidColorBrush(Color);
                }
            }
        }
    }
}
/*
Transparent
Black
White
DimGray
Gray
DarkGray
Silver
LightGray
Gainsboro
WhiteSmoke
Maroon
DarkRed
Red
Brown
Firebrick
IndianRed
Snow
LightCoral
RosyBrown
MistyRose
Salmon
Tomato
DarkSalmon
Coral
OrangeRed
LightSalmon
Sienna
SeaShell
Chocolate
SaddleBrown
SandyBrown
PeachPuff
Peru
Linen
Bisque
DarkOrange
BurlyWood
Tan
AntiqueWhite
NavajoWhite
BlanchedAlmond
PapayaWhip
Moccasin
Orange
Wheat
OldLace
FloralWhite
DarkGoldenrod
Goldenrod
Cornsilk
Gold
Khaki
LemonChiffon
PaleGoldenrod
DarkKhaki
Beige
LightGoldenrodYellow
Olive
Yellow
LightYellow
Ivory
OliveDrab
YellowGreen
DarkOliveGreen
GreenYellow
Chartreuse
LawnGreen
DarkSeaGreen
LightGreen
ForestGreen
LimeGreen
PaleGreen
DarkGreen
Green
Lime
Honeydew
SeaGreen
MediumSeaGreen
SpringGreen
MintCream
MediumSpringGreen
MediumAquamarine
Aquamarine
Turquoise
LightSeaGreen
MediumTurquoise
DarkSlateGray
PaleTurquoise
Teal
DarkCyan
Cyan
Aqua
LightCyan
Azure
DarkTurquoise
CadetBlue
PowderBlue
LightBlue
DeepSkyBlue
SkyBlue
LightSkyBlue
SteelBlue
AliceBlue
DodgerBlue
SlateGray
LightSlateGray
LightSteelBlue
CornflowerBlue
RoyalBlue
MidnightBlue
Lavender
Navy
DarkBlue
MediumBlue
Blue
GhostWhite
SlateBlue
DarkSlateBlue
MediumSlateBlue
MediumPurple
BlueViolet
Indigo
DarkOrchid
DarkViolet
MediumOrchid
Thistle
Plum
Violet
Purple
DarkMagenta
Fuchsia
Magenta
Orchid
MediumVioletRed
DeepPink
HotPink
LavenderBlush
PaleVioletRed
Crimson
Pink
LightPink
*/