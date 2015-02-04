//Filename: ScriptableType.cs
//Version: 20150204

#if !SILVERLIGHT

namespace System.Windows.Browser
{

  [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Struct, 
                           AllowMultiple = false, 
                           Inherited = false)]
  public sealed class ScriptableTypeAttribute : Attribute
  {

    public ScriptableTypeAttribute()
    {
    }

  }

}

#endif