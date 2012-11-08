//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BindingUtils.cs
//Version: 20121108

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ClipFlair.Utils.Bindings
{
    public static class BindingUtils
    {

      /// Listen for change of a dependency property (Source: http://www.amazedsaint.com/2009/12/silverlight-listening-to-dependency.html), useful for binding to ancestor properties
      public static void RegisterForNotification(string propertyName, FrameworkElement element, PropertyChangedCallback callback)
      {
        //Bind to a depedency property
        Binding b = new Binding(propertyName) { Source = element };
        DependencyProperty prop = System.Windows.DependencyProperty.RegisterAttached(
            "ListenAttached" + propertyName,
            typeof(object),
            typeof(UserControl),
            new System.Windows.PropertyMetadata(callback));
        element.SetBinding(prop, b);
      }

      /// <summary>
      /// Bind two components together over given properties
      /// </summary>
      /// <param name="source">source object</param>
      /// <param name="sourcePropertyPath">source property path</param>
      /// <param name="target">target DependencyObject</param>
      /// <param name="targetProperty">target DependencyProperty</param>
      /// <param name="bindmode">e.g. BindingMode.TwoWay</param>
      public static void BindProperties(object source, string sourcePropertyPath, DependencyObject target, DependencyProperty targetProperty, BindingMode bindmode)
      {
        Binding theBinding = new Binding(sourcePropertyPath);
        theBinding.Source = source;
        theBinding.Mode = bindmode;
        BindingOperations.SetBinding(target, targetProperty, theBinding);
      }

    }
}
