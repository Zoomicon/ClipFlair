//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: BindingUtils.cs
//Version: 20121205


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Utils.Bindings
{
  public static class BindingUtils
  {

    public static List<DependencyObject> Binders = new List<DependencyObject>();

    /// Listen for change of a dependency property (Source: http://www.amazedsaint.com/2009/12/silverlight-listening-to-dependency.html), useful for binding to ancestor properties
    public static void RegisterForNotification(string propertyName, DependencyObject element, PropertyChangedCallback callback)
    {
      //Bind to a depedency property
      Binding b = new Binding(propertyName) { Source = element };
      DependencyProperty prop = DependencyProperty.RegisterAttached(
          "ListenAttached" + propertyName,
          typeof(object),
          typeof(UserControl),
          new PropertyMetadata(callback));
      BindingOperations.SetBinding(element, prop, b);
    }

    /// <summary>
    /// Bind two objects implementing INotifyPropertyChanged over given properties
    /// </summary>
    /// <param name="source">source object</param>
    /// <param name="sourcePropertyName">source property name</param>
    /// <param name="target">target object</param>
    /// <param name="targetPropertyName">target property name</param>
    /// <param name="bindmode">e.g. BindingMode.TwoWay</param>

    /*
    public static DependencyObject BindProperties(INotifyPropertyChanged source, string sourcePropertyName, INotifyPropertyChanged target, string targetPropertyName)
    {
      DependencyObject binder = new UserControl();
      DependencyProperty bindingProperty = DependencyProperty.RegisterAttached("ListenAttached" + sourcePropertyName, typeof(object), typeof(UserControl), new PropertyMetadata(null));

      Binding binding1 = BindProperties(source, sourcePropertyName, binder, bindingProperty, BindingMode.TwoWay); //bind source two-way to helper
      Binding binding2 = BindProperties(target, targetPropertyName, binder, bindingProperty, BindingMode.TwoWay); //bind target two-way to helper

      return binder; //use this to break the bindings (maybe dispose of the object or something). If that isn't easy, maybe return new Binding[]{binding1, binding2}
    }
    */

    public static void BindProperties(INotifyPropertyChanged source, string sourcePropertyName, INotifyPropertyChanged target, string targetPropertyName, BindingMode bindMode = BindingMode.TwoWay)
    {
      switch (bindMode) {
        case BindingMode.OneTime:
          target.SetProperty(targetPropertyName, source.GetProperty(sourcePropertyName));
          break;
        case BindingMode.OneWay:
          source.PropertyChanged += new PropertyChangedEventHandler((s,e)=> { if (e.PropertyName == sourcePropertyName) BindProperties(source, sourcePropertyName, target, targetPropertyName, BindingMode.OneTime);} );
          BindProperties(source, sourcePropertyName, target, targetPropertyName, BindingMode.OneTime); //first bind, then set value (target may have looped back to us and changed our value, in that case we read that latest value from source and set to target)
          break;
        case BindingMode.TwoWay:
          target.PropertyChanged += new PropertyChangedEventHandler((s, e) => { if (e.PropertyName == targetPropertyName) BindProperties(target, targetPropertyName, source, sourcePropertyName, BindingMode.OneTime);} );
          BindProperties(source, sourcePropertyName, target, targetPropertyName, BindingMode.OneWay);
          break;
        default:
          throw new NotSupportedException("Unsupported binding mode");
      }
    }

    /// <summary>
    /// Bind two components over given properties
    /// </summary>
    /// <param name="source">source object</param>
    /// <param name="sourcePropertyPath">source property path</param>
    /// <param name="target">target DependencyObject</param>
    /// <param name="targetProperty">target DependencyProperty</param>
    /// <param name="bindmode">e.g. BindingMode.TwoWay</param>
    public static Binding BindProperties(object source, string sourcePropertyPath, DependencyObject target, DependencyProperty targetProperty, BindingMode bindMode)
    {
      Binding theBinding = new Binding(sourcePropertyPath) { Source = source, Mode = bindMode };
      BindingOperations.SetBinding(target, targetProperty, theBinding);
      return theBinding;
    }

    public static DependencyProperty GetDependencyProperty(Type type, string name) //name usually has the form "SomethingProperty"
    {
      if (type == null) return null;

      FieldInfo fieldInfo = type.GetField(name, BindingFlags.Public | BindingFlags.Static);
      return (fieldInfo != null) ? (DependencyProperty)fieldInfo.GetValue(null) : null;
    }

    public static DependencyProperty GetDependencyProperty(object obj, string name)
    {
      if (obj == null) return null;
      
      return GetDependencyProperty(obj.GetType(), name);
    }

    public static bool IsAssignableTo(this object obj, Type type)
    {
      return type.IsAssignableFrom(obj.GetType());
    }

    public static bool IsAssignableTo(this object obj, string typeName)
    {
      return obj.IsAssignableTo(Type.GetType(typeName));
    }

    public static bool SafeIsAssignableTo(this object obj, string typeName)
    {
      try
      {
        return IsAssignableTo(obj, typeName);
      }
      catch
      {
        return false;
      }
    }

  }
}
