﻿//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: Controls.cs
//Version: 20131104

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ClipFlair.Gallery
{
  public static class UI
  {

    public static void LoadCheckBoxList(CheckBoxList list, string[] values)
    {
      foreach (ListItem item in list.Items)
        item.Selected = values.Contains(item.Text);
    }

    public static void LoadCheckBox(CheckBox checkbox, bool value)
    {
      checkbox.Checked = value;
    }
    
    public static void LoadLabel(Label label, string value)
    {
      label.Text = value;
    }

    public static void LoadTextBox(TextBox textbox, string value)
    {
      textbox.Text = value;
    }

    public static void LoadHyperlink(HyperLink hyperlink, Uri url)
    {
      string s = url.ToString();
      hyperlink.Text = s;
      hyperlink.NavigateUrl = s;
    }

    public static void LoadTextBox(TextBox textbox, string[] values)
    {
      textbox.Text = string.Join(",", values);
    }

    public static string[] GetCommaSeparated(TextBox txt)
    {
      return (string.IsNullOrWhiteSpace(txt.Text))? null : txt.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static string[] GetSelected(CheckBoxList clist)
    {
      List<string> result = new List<string>();
      foreach (ListItem item in clist.Items)
        if (item.Selected && !string.IsNullOrWhiteSpace(item.Text))
          result.Add(item.Text);
      return result.ToArray();
    }

  }
  
}