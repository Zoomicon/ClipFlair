//Version: 20140321

//Based on: http://reflector.webtropy.com/default.aspx/DotNET/DotNET/8@0/untmp/WIN_WINDOWS/lh_tools_devdiv_wpf/Windows/wcp/Framework/System/Windows/Data/ValueConversionAttribute@cs/1/ValueConversionAttribute@cs
//a simpler implementation (though at other namespace) can be found at: https://github.com/rhwilburn/MVVM-for-Mono/blob/master/MVVM.Common/Binding/Converter/ValueConversionAttribute.cs

#if SILVERLIGHT

namespace System.Windows.Data
{

  /// <summary>
  ///     This attribute allows the author of a <seealso cref="System.Windows.Data.IValueConverter">
  ///     to specify what source and target property types the ValueConverter is capable of converting. 
  ///     This meta data is useful for designer tools to help categorize and match ValueConverters.
  /// </seealso></summary> 
  /// <remarks> 
  /// Add this custom attribute to your IValueConverter class definition.
  /// <code> 
  ///     [ValueConversion(typeof(Employee), typeof(Brush))]
  ///     class MyConverter : IValueConverter
  ///     {
  ///         public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
  ///         {
  ///             if (value is Dev)       return Brushes.Beige; 
  ///             if (value is Employee)  return Brushes.Salmon; 
  ///             return Brushes.Yellow;
  ///         } 
  ///     }
  /// </code>
  /// </remarks>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public sealed class ValueConversionAttribute : Attribute
  {

    #region Fields

    private Type _sourceType;
    private Type _targetType;
    private Type _parameterType;

    #endregion

    /// <summary> 
    ///     Creates a new ValueConversionAttribute to indicate between
    ///     what types of a data binding source and target this ValueConverter can convert. 
    /// </summary> 
    /// <param name="sourceType">the expected source type this ValueConverter can handle
    /// <param name="targetType">the target type to which this ValueConverter can convert to 
    public ValueConversionAttribute(Type sourceType, Type targetType)
    {
      if (sourceType == null)
        throw new ArgumentNullException("sourceType");
      if (targetType == null)
        throw new ArgumentNullException("targetType");
      _sourceType = sourceType;
      _targetType = targetType;
    }

    /// <summary>
    ///     The expected source type this ValueConverter can handle.
    /// </summary> 
    public Type SourceType
    {
      get { return _sourceType; }
    }

    /// <summary>
    ///     The target type to which this ValueConverter can convert to.
    /// </summary>
    public Type TargetType
    {
      get { return _targetType; }
    }

    /// <summary> 
    ///     The type of the optional ValueConverter Parameter object.
    /// </summary>

    public Type ParameterType
    {
      get { return _parameterType; }
      set { _parameterType = value; }
    }

    ///<summary> 
    ///     Returns the unique identifier for this Attribute.
    ///</summary>
    // Type ID is used to remove redundant attributes by
    // putting all attributes in a dictionary of [TypeId, Attribute]. 
    // If you want AllowMultiple attributes to work with designers,
    // you must override TypeId.  The default implementation returns 
    // this.GetType(), which is appropriate for AllowMultiple = false, but 
    // not for AllowMultiple = true;

    public /*override*/ object TypeId
    {
      // the attribute itself will be used as a key to the dictionary
      get { return this; }
    }

    ///<summary> 
    ///     Returns the hash code for this instance. 
    ///</summary>

    override public int GetHashCode()
    {
      // the default implementation does some funky enumeration over its fields
      // we can do better and use the 2 mandatory fields source/targetType's hash codes
      return _sourceType.GetHashCode() + _targetType.GetHashCode();
    }

  }

} 

#endif