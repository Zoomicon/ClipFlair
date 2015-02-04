//Filename: ScriptableMemberAttribute.cs
//Version: 20150204

#if !SILVERLIGHT

namespace System.Windows.Browser
{

  [AttributeUsageAttribute(
    AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Event, 
    AllowMultiple = false, 
	  Inherited = true)]
  public sealed class ScriptableMemberAttribute : Attribute
  {

    public ScriptableMemberAttribute()
    {
    }

  }

}

#endif