//Filename: ApplicationException.cs
//Version: 20120728
//Author: George Birbilis <birbilis@kagi.com>

#if SILVERLIGHT

namespace System
{
    /// <summary>
    /// Silverlight does not offer support for exceptions of type ApplicationException
    /// see: http://blogs.msdn.com/b/knom/archive/2007/05/15/goodbye-applicationexception.aspx (saying that ApplicationException is obsolete)
    /// using implementation from: http://code.google.com/p/nunit-silverlight/source/browse/trunk/src/NUnit.Silverlight.Framework.Tests/Compatibility/ApplicationException.cs?r=3
    /// </summary>
    public class ApplicationException : Exception
    {

        public ApplicationException()
        {
        }

        public ApplicationException(string message) : base(message)
        {
        }

        public ApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    
    }

}

#endif