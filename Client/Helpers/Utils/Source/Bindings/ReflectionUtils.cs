//Project: ClipFlair (http://ClipFlair.codeplex.com)
//Filename: ReflectionUtils.cs
//Version: 20121205

using System;
using System.Reflection;

namespace Utils.Bindings
{
  public static class ReflectionUtils
  {

    public static object GetField(this object obj, string propertyName)
    {
      FieldInfo field = obj.GetType().GetField(propertyName, BindingFlags.Public | BindingFlags.Instance); //only getting public fields //note that BindingFlags.Instance is needed here
      if (field == null) throw new Exception("Couldn't find field: " + propertyName);
      return field.GetValue(obj);
    }

    public static void SetField(this object obj, string propertyName, object value)
    {
      FieldInfo field = obj.GetType().GetField(propertyName, BindingFlags.Public); //only setting public fields //note that BindingFlags.Instance is needed here
      if (field == null) throw new Exception("Couldn't find field: " + propertyName);
      field.SetValue(obj, value);
    }

    public static object GetProperty(this object obj, string propertyName)
    {
      PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance); //only getting public properties //note that BindingFlags.Instance is needed here
      if (property == null) throw new Exception("Couldn't find Property: " + propertyName);
      return property.GetValue(obj, null); //TODO: also checkout the overloaded version which takes binding-related params
    }

    public static void SetProperty(this object obj, string propertyName, object value)
    {
      PropertyInfo property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance); //only setting public properties //note that BindingFlags.Instance is needed here
      if (property == null) throw new Exception("Couldn't find Property: " + propertyName);
      property.SetValue(obj, value, null); //TODO: also checkout the overloaded version which takes binding-related params
    }

  }
}
