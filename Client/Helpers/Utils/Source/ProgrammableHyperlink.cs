//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ProgrammableHyperlink.cs
//Version: 20121224

//TODO: implement HyperlinkButton (find original code [from Silverlight Toolkit I think] or check if WPF Toolkit exists and has that)

#if SILVERLIGHT

using System;
using System.Windows.Controls;

namespace Utils
{
  public class ProgrammableHyperlink : HyperlinkButton
  {

    public ProgrammableHyperlink()
    {
      //NOP
    }

    public ProgrammableHyperlink(Uri link)
    {
      NavigateUri = link;
    }

    public void DoClick()
    {
      if (TargetName == null) TargetName = "_blank";
      OnClick();
    }

  }
}

#endif
