//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: App_Extensions.cs
//Version: 20130130

using System;
using System.Reflection;
using System.Windows;

namespace ClipFlair
{

  public partial class App: Application
  {

    #region Properties

    public string ProductName
    {
      get { return GetProductName(); }
    }

    public Version Version
    {
      get { return GetVersion(); }
    }

    #endregion

    #region Methods

    private static string GetProductName() //based on http://stackoverflow.com/questions/5169461/how-to-get-the-applications-productname-in-silverlight
    {
      Assembly callingAssembly = Assembly.GetExecutingAssembly();

      //Get the product name from the AssemblyProductAttribute.
      //Usually defined in AssemblyInfo.cs as: [assembly: AssemblyProduct("Hello World Product")]
      AssemblyProductAttribute assemblyProductAttribute = (AssemblyProductAttribute)callingAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
      return assemblyProductAttribute.Product;
    }

    private static Version GetVersion() //based on http://stackoverflow.com/questions/5169461/how-to-get-the-applications-productname-in-silverlight
    {
      Assembly callingAssembly = Assembly.GetExecutingAssembly();

      //Get the product version from the assembly by using its AssemblyName.
      return new AssemblyName(callingAssembly.FullName).Version;
    }
    
    #endregion

  }

}
