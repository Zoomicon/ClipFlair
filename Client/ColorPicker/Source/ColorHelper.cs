﻿using System;
using System.Windows.Media;

namespace ColorPickerLib
{
  public static class ColorHelper
  {

    public static SolidColorBrush ChangedColor(SolidColorBrush brush, Color newColor)
    {
      return new SolidColorBrush() //Note: original code was setting Color property of the existing brush, but WPF treats that field as Read-Only at runtime (brush object must be Frozen)
      {
        Color = newColor,
        Opacity = brush.Opacity,
        RelativeTransform = brush.RelativeTransform,
        Transform = brush.Transform
      };
    }

    public static Color HSB2RGB(double h, double s, double l)
    {
      double q = (l < 0.5f) ? (l * (1f + s)) : (l + s - l * s);
      double p = 2 * l - q;
      double hk = h / 360f;
      double tr = hk + 1f / 3f;
      double tg = hk;
      double tb = hk - 1f / 3f;

      byte r = GetColorItem(q, p, tr);
      byte g = GetColorItem(q, p, tg);
      byte b = GetColorItem(q, p, tb);

      return Color.FromArgb(255, r, g, b);
    }
    private static byte GetColorItem(double q, double p, double t)
    {
      if (t < 0f)
      {
        t += 1f;
      }
      else if (t > 1f)
      {
        t -= 1f;
      }

      double f16 = 1f / 6f;
      double f12 = 1f / 2f;
      double f23 = 2f / 3f;

      double color;
      if (t < f16)
      {
        color = p + (q - p) * 6 * t;
      }
      else if (t < f12)
      {
        color = q;
      }
      else if (t < f23)
      {
        color = p + (q - p) * 6 * (f23 - t);
      }
      else
      {
        color = p;
      }

      return (byte)(color * 255 + 0.5f);
    }

    public static Color HSV2RGB(double h, double s, double v)
    {
      double c = v * s;
      double h0 = h / 60;
      double x = c * (1 - Math.Abs(h0 % 2 - 1));

      double r = 0d;
      double g = 0d;
      double b = 0d;

      if (h0 < 0)
      {
        // do nothing;
      }
      else if (h0 < 1d)
      {
        r = c;
        g = x;
        b = 0;
      }
      else if (h0 < 2d)
      {
        r = x;
        g = c;
        b = 0;
      }
      else if (h0 < 3d)
      {
        r = 0;
        g = c;
        b = x;
      }
      else if (h0 < 4d)
      {
        r = 0;
        g = x;
        b = c;
      }
      else if (h0 < 5d)
      {
        r = x;
        g = 0;
        b = c;
      }
      else if (h0 < 6d)
      {
        r = c;
        g = 0;
        b = x;
      }
      else
      {
        // do nothing;
      }

      double m = v - c;

      r = r + m;
      g = g + m;
      b = b + m;

      return Color.FromArgb(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
    }

    public static double GetHSB_H(Color color)
    {
      return GetHue(color);
    }
    public static double GetHSB_S(Color color)
    {
      double r = color.R / 255d;
      double g = color.G / 255d;
      double b = color.B / 255d;

      double max = Math.Max(r, Math.Max(g, b));
      double min = Math.Min(r, Math.Min(g, b));
      if (max == min)
      {
        return 0f;
      }

      double brightness = (max + min) / 2d;
      if (brightness <= 0.5)
      {
        return (max - min) / (max + min);
      }
      else
      {
        return (max - min) / (2d - max - min);
      }
    }
    public static double GetHSB_B(Color color)
    {
      double r = color.R / 255d;
      double g = color.G / 255d;
      double b = color.B / 255d;

      double max = Math.Max(r, Math.Max(g, b));
      double min = Math.Min(r, Math.Min(g, b));

      return (max + min) / 2d;
    }

    public static double GetHSV_H(Color color)
    {
      return GetHue(color);
    }
    public static double GetHSV_S(Color color)
    {
      double r = color.R / 255d;
      double g = color.G / 255d;
      double b = color.B / 255d;

      double max = Math.Max(r, Math.Max(g, b));
      if (max < double.Epsilon)
      {
        return 0d;
      }

      double min = Math.Min(r, Math.Min(g, b));

      return (max - min) / max;
    }
    public static double GetHSV_V(Color color)
    {
      double r = color.R / 255d;
      double g = color.G / 255d;
      double b = color.B / 255d;

      double max = Math.Max(r, Math.Max(g, b));

      return max;
    }

    private static double GetHue(Color color)
    {
      if (color.R == color.G && color.G == color.B)
      {
        return 0f;
      }

      double r = color.R / 255d;
      double g = color.G / 255d;
      double b = color.B / 255d;

      double max = Math.Max(r, Math.Max(g, b));
      double min = Math.Min(r, Math.Min(g, b));
      double c = max - min;

      double h0 = 0d;
      if (r == max)
      {
        h0 = (g - b) / c;
      }
      else if (g == max)
      {
        h0 = 2d + ((b - r) / c);
      }
      else if (b == max)
      {
        h0 = 4d + ((r - g) / c);
      }

      h0 *= 60d;
      if (h0 < 0d)
      {
        h0 += 360d;
      }

      return h0;
    }
  }
}
