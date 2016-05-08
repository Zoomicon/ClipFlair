//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: UI.cs
//Version: 20160509

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ClipFlair.Gallery
{
  public static class UI
  {

    public static string[] GetCommaSeparated(TextBox txt)
    {
      return (string.IsNullOrWhiteSpace(txt.Text)) ? null : txt.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static string[] GetSelected(ListControl clist)
    {
      List<string> result = new List<string>();
      foreach (ListItem item in clist.Items)
        if (item.Selected && !string.IsNullOrWhiteSpace(item.Text))
          result.Add(item.Text);
      return result.ToArray();
    }

    //

    public static void Load(ListControl list, string[] values)
    {
      foreach (ListItem item in list.Items)
        item.Selected = values.Contains(item.Text);
    }

    public static void Load(ListControl list, string value)
    {
      list.SelectedValue = value;
    }

    public static void Load(CheckBox checkbox, bool value)
    {
      checkbox.Checked = value;
    }

    public static void Load(Label label, string value)
    {
      label.Text = value;
    }

    public static void Load(HyperLink hyperlink, Uri url)
    {
      string s = url.ToString();
      hyperlink.Text = s;
      hyperlink.NavigateUrl = s;
    }

    public static void Load(TextBox textbox, string value)
    {
      textbox.Text = value;
    }

    public static void Load(TextBox textbox, string[] values)
    {
      textbox.Text = string.Join(",", values);
    }

  }

}