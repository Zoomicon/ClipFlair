//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ProgrammableHyperlink.cs
//Version: 20140421

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

    public void DoClick() //has to be invoked by user action
    {
      if (TargetName == null) TargetName = "_blank";
      OnClick();
    }

  }
}

#endif
