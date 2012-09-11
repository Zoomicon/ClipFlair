namespace javax.sound.sampled
{
	/// <summary>
	/// The <code>Line</code> interface represents a mono or multi-channel
	/// audio feed.
	/// </summary>
	/// <remarks>
	/// The <code>Line</code> interface represents a mono or multi-channel
	/// audio feed. A line is an element of the digital audio
	/// "pipeline," such as a mixer, an input or output port,
	/// or a data path into or out of a mixer.
	/// <p>
	/// A line can have controls, such as gain, pan, and reverb.
	/// The controls themselves are instances of classes that extend the
	/// base <code>
	/// <see cref="Control">Control</see>
	/// </code> class.
	/// The <code>Line</code> interface provides two accessor methods for
	/// obtaining the line's controls: <code>
	/// <see cref="getControls()">getControls</see>
	/// </code> returns the
	/// entire set, and <code>
	/// <see cref="getControl(Type)">getControl</see>
	/// </code> returns a single control of
	/// specified type.
	/// <p>
	/// Lines exist in various states at different times.  When a line opens, it reserves system
	/// resources for itself, and when it closes, these resources are freed for
	/// other objects or applications. The <code>
	/// <see cref="isOpen()">isOpen()</see>
	/// </code> method lets
	/// you discover whether a line is open or closed.
	/// An open line need not be processing data, however.  Such processing is
	/// typically initiated by subinterface methods such as
	/// <code>
	/// <see cref="SourceDataLine#write">SourceDataLine.write</see>
	/// </code> and
	/// <code>
	/// <see cref="TargetDataLine.read(byte[], int, int)">TargetDataLine.read</see>
	/// </code>.
	/// <p>
	/// You can register an object to receive notifications whenever the line's
	/// state changes.  The object must implement the <code>
	/// <see cref="LineListener">LineListener</see>
	/// </code>
	/// interface, which consists of the single method
	/// <code>
	/// <see cref="LineListener.update(LineEvent)">update</see>
	/// </code>.
	/// This method will be invoked when a line opens and closes (and, if it's a
	/// <see cref="DataLine">DataLine</see>
	/// , when it starts and stops).
	/// <p>
	/// An object can be registered to listen to multiple lines.  The event it
	/// receives in its <code>update</code> method will specify which line created
	/// the event, what type of event it was
	/// (<code>OPEN</code>, <code>CLOSE</code>, <code>START</code>, or <code>STOP</code>),
	/// and how many sample frames the line had processed at the time the event occurred.
	/// <p>
	/// Certain line operations, such as open and close, can generate security
	/// exceptions if invoked by unprivileged code when the line is a shared audio
	/// resource.
	/// </remarks>
	/// <author>Kara Kytle</author>
	/// <version>1.30, 05/11/17</version>
	/// <seealso cref="LineEvent">LineEvent</seealso>
	/// <since>1.3</since>
	public interface Line
	{
		/// <summary>
		/// Obtains the <code>Line.Info</code> object describing this
		/// line.
		/// </summary>
		/// <remarks>
		/// Obtains the <code>Line.Info</code> object describing this
		/// line.
		/// </remarks>
		/// <returns>description of the line</returns>
		LineInfo getLineInfo();

		/// <summary>
		/// Opens the line, indicating that it should acquire any required
		/// system resources and become operational.
		/// </summary>
		/// <remarks>
		/// Opens the line, indicating that it should acquire any required
		/// system resources and become operational.
		/// If this operation
		/// succeeds, the line is marked as open, and an <code>OPEN</code> event is dispatched
		/// to the line's listeners.
		/// <p>
		/// Note that some lines, once closed, cannot be reopened.  Attempts
		/// to reopen such a line will always result in an <code>LineUnavailableException</code>.
		/// <p>
		/// Some types of lines have configurable properties that may affect
		/// resource allocation.   For example, a <code>DataLine</code> must
		/// be opened with a particular format and buffer size.  Such lines
		/// should provide a mechanism for configuring these properties, such
		/// as an additional <code>open</code> method or methods which allow
		/// an application to specify the desired settings.
		/// <p>
		/// This method takes no arguments, and opens the line with the current
		/// settings.  For <code>
		/// <see cref="SourceDataLine">SourceDataLine</see>
		/// </code> and
		/// <code>
		/// <see cref="TargetDataLine">TargetDataLine</see>
		/// </code> objects, this means that the line is
		/// opened with default settings.  For a <code>
		/// <see cref="Clip">Clip</see>
		/// </code>, however,
		/// the buffer size is determined when data is loaded.  Since this method does not
		/// allow the application to specify any data to load, an IllegalArgumentException
		/// is thrown. Therefore, you should instead use one of the <code>open</code> methods
		/// provided in the <code>Clip</code> interface to load data into the <code>Clip</code>.
		/// <p>
		/// For <code>DataLine</code>'s, if the <code>DataLine.Info</code>
		/// object which was used to retrieve the line, specifies at least
		/// one fully qualified audio format, the last one will be used
		/// as the default format.
		/// </remarks>
		/// <exception cref="System.ArgumentException">if this method is called on a Clip instance.
		/// 	</exception>
		/// <exception cref="javax.sound.sampled.LineUnavailableException">
		/// if the line cannot be
		/// opened due to resource restrictions.
		/// </exception>
		/// <exception cref="System.Security.SecurityException">
		/// if the line cannot be
		/// opened due to security restrictions.
		/// </exception>
		/// <seealso cref="close()">close()</seealso>
		/// <seealso cref="isOpen()">isOpen()</seealso>
		/// <seealso cref="LineEvent">LineEvent</seealso>
		/// <seealso cref="DataLine">DataLine</seealso>
		/// <seealso cref="Clip#open(AudioFormat,byte[],int,int)">Clip#open(AudioFormat,byte[],int,int)
		/// 	</seealso>
		/// <seealso cref="Clip#open(AudioInputStream)">Clip#open(AudioInputStream)</seealso>
		void open();

		/// <summary>
		/// Closes the line, indicating that any system resources
		/// in use by the line can be released.
		/// </summary>
		/// <remarks>
		/// Closes the line, indicating that any system resources
		/// in use by the line can be released.  If this operation
		/// succeeds, the line is marked closed and a <code>CLOSE</code> event is dispatched
		/// to the line's listeners.
		/// </remarks>
		/// <exception cref="System.Security.SecurityException">
		/// if the line cannot be
		/// closed due to security restrictions.
		/// </exception>
		/// <seealso cref="open()">open()</seealso>
		/// <seealso cref="isOpen()">isOpen()</seealso>
		/// <seealso cref="LineEvent">LineEvent</seealso>
		void close();

		/// <summary>
		/// Indicates whether the line is open, meaning that it has reserved
		/// system resources and is operational, although it might not currently be
		/// playing or capturing sound.
		/// </summary>
		/// <remarks>
		/// Indicates whether the line is open, meaning that it has reserved
		/// system resources and is operational, although it might not currently be
		/// playing or capturing sound.
		/// </remarks>
		/// <returns><code>true</code> if the line is open, otherwise <code>false</code></returns>
		/// <seealso cref="open()">open()</seealso>
		/// <seealso cref="close()">close()</seealso>
		bool isOpen();

		/// <summary>Obtains the set of controls associated with this line.</summary>
		/// <remarks>
		/// Obtains the set of controls associated with this line.
		/// Some controls may only be available when the line is open.
		/// If there are no controls, this method returns an array of length 0.
		/// </remarks>
		/// <returns>the array of controls</returns>
		/// <seealso cref="getControl(Type)">getControl(Type)</seealso>
		Control[] getControls();

		/// <summary>Indicates whether the line supports a control of the specified type.</summary>
		/// <remarks>
		/// Indicates whether the line supports a control of the specified type.
		/// Some controls may only be available when the line is open.
		/// </remarks>
		/// <param name="control">the type of the control for which support is queried</param>
		/// <returns>
		/// <code>true</code> if at least one control of the specified type is
		/// supported, otherwise <code>false</code>.
		/// </returns>
		bool isControlSupported(Control.Type control);

		/// <summary>
		/// Obtains a control of the specified type,
		/// if there is any.
		/// </summary>
		/// <remarks>
		/// Obtains a control of the specified type,
		/// if there is any.
		/// Some controls may only be available when the line is open.
		/// </remarks>
		/// <param name="control">the type of the requested control</param>
		/// <returns>a control of the specified type</returns>
		/// <exception cref="System.ArgumentException">
		/// if a control of the specified type
		/// is not supported
		/// </exception>
		/// <seealso cref="getControls()">getControls()</seealso>
		/// <seealso cref="isControlSupported(Type)">isControlSupported(Type)</seealso>
		Control getControl(Control.Type control);

		/// <summary>Adds a listener to this line.</summary>
		/// <remarks>
		/// Adds a listener to this line.  Whenever the line's status changes, the
		/// listener's <code>update()</code> method is called with a <code>LineEvent</code> object
		/// that describes the change.
		/// </remarks>
		/// <param name="listener">the object to add as a listener to this line</param>
		/// <seealso cref="removeLineListener(LineListener)">removeLineListener(LineListener)
		/// 	</seealso>
		/// <seealso cref="LineListener.update(LineEvent)">LineListener.update(LineEvent)</seealso>
		/// <seealso cref="LineEvent">LineEvent</seealso>
		void addLineListener(LineListener listener);

		/// <summary>Removes the specified listener from this line's list of listeners.</summary>
		/// <remarks>Removes the specified listener from this line's list of listeners.</remarks>
		/// <param name="listener">listener to remove</param>
		/// <seealso cref="addLineListener(LineListener)">addLineListener(LineListener)</seealso>
		void removeLineListener(LineListener listener);
	}

    /// <summary>A <code>Line.Info</code> object contains information about a line.</summary>
    /// <remarks>
    /// A <code>Line.Info</code> object contains information about a line.
    /// The only information provided by <code>Line.Info</code> itself
    /// is the Java class of the line.
    /// A subclass of <code>Line.Info</code> adds other kinds of information
    /// about the line.  This additional information depends on which <code>Line</code>
    /// subinterface is implemented by the kind of line that the <code>Line.Info</code>
    /// subclass describes.
    /// <p>
    /// A <code>Line.Info</code> can be retrieved using various methods of
    /// the <code>Line</code>, <code>Mixer</code>, and <code>AudioSystem</code>
    /// interfaces.  Other such methods let you pass a <code>Line.Info</code> as
    /// an argument, to learn whether lines matching the specified configuration
    /// are available and to obtain them.
    /// </remarks>
    /// <author>Kara Kytle</author>
    /// <version>1.30, 05/11/17</version>
    /// <seealso cref="Line.getLineInfo()">Line.getLineInfo()</seealso>
    /// <seealso cref="Mixer#getSourceLineInfo">Mixer#getSourceLineInfo</seealso>
    /// <seealso cref="Mixer#getTargetLineInfo">Mixer#getTargetLineInfo</seealso>
    /// <seealso cref="Mixer#getLine"><code>Mixer.getLine(Line.Info)</code></seealso>
    /// <seealso cref="Mixer#getSourceLineInfo(Line.Info)"><code>Mixer.getSourceLineInfo(Line.Info)</code>
    /// 	</seealso>
    /// <seealso cref="Mixer#getSourceLineInfo(Line.Info)"><code>Mixer.getTargetLineInfo(Line.Info)</code>
    /// 	</seealso>
    /// <seealso cref="Mixer#isLineSupported"><code>Mixer.isLineSupported(Line.Info)</code>
    /// 	</seealso>
    /// <seealso cref="AudioSystem#getLine"><code>AudioSystem.getLine(Line.Info)</code></seealso>
    /// <seealso cref="AudioSystem#getSourceLineInfo"><code>AudioSystem.getSourceLineInfo(Line.Info)</code>
    /// 	</seealso>
    /// <seealso cref="AudioSystem#getTargetLineInfo"><code>AudioSystem.getTargetLineInfo(Line.Info)</code>
    /// 	</seealso>
    /// <seealso cref="AudioSystem#isLineSupported"><code>AudioSystem.isLineSupported(Line.Info)</code>
    /// 	</seealso>
    /// <since>1.3</since>
    public class LineInfo
    {
        /// <summary>The class of the line described by the info object.</summary>
        /// <remarks>The class of the line described by the info object.</remarks>
        private readonly System.Type lineClass;

        /// <summary>Constructs an info object that describes a line of the specified class.</summary>
        /// <remarks>
        /// Constructs an info object that describes a line of the specified class.
        /// This constructor is typically used by an application to
        /// describe a desired line.
        /// </remarks>
        /// <param name="lineClass">the class of the line that the new Line.Info object describes
        /// 	</param>
        public LineInfo(System.Type lineClass)
        {
            if (lineClass == null)
            {
                this.lineClass = typeof(Line);
            }
            else
            {
                this.lineClass = lineClass;
            }
        }

        /// <summary>Obtains the class of the line that this Line.Info object describes.</summary>
        /// <remarks>Obtains the class of the line that this Line.Info object describes.</remarks>
        /// <returns>the described line's class</returns>
        public virtual System.Type getLineClass()
        {
            return lineClass;
        }

        /// <summary>Indicates whether the specified info object matches this one.</summary>
        /// <remarks>
        /// Indicates whether the specified info object matches this one.
        /// To match, the specified object must be identical to or
        /// a special case of this one.  The specified info object
        /// must be either an instance of the same class as this one,
        /// or an instance of a sub-type of this one.  In addition, the
        /// attributes of the specified object must be compatible with the
        /// capabilities of this one.  Specifically, the routing configuration
        /// for the specified info object must be compatible with that of this
        /// one.
        /// Subclasses may add other criteria to determine whether the two objects
        /// match.
        /// </remarks>
        /// <param name="info">the info object which is being compared to this one</param>
        /// <returns>
        /// <code>true</code> if the specified object matches this one,
        /// <code>false</code> otherwise
        /// </returns>
        public virtual bool matches(LineInfo info)
        {
            // $$kk: 08.30.99: is this backwards?
            // dataLine.matches(targetDataLine) == true: targetDataLine is always dataLine
            // targetDataLine.matches(dataLine) == false
            // so if i want to make sure i get a targetDataLine, i need:
            // targetDataLine.matches(prospective_match) == true
            // => prospective_match may be other things as well, but it is at least a targetDataLine
            // targetDataLine defines the requirements which prospective_match must meet.
            // "if this Class object represents a declared class, this method returns
            // true if the specified Object argument is an instance of the represented
            // class (or of any of its subclasses)"
            // GainControlClass.isInstance(MyGainObj) => true
            // GainControlClass.isInstance(MySpecialGainInterfaceObj) => true
            // this_class.isInstance(that_object)	=> that object can by cast to this class
            //										=> that_object's class may be a subtype of this_class
            //										=> that may be more specific (subtype) of this
            // "If this Class object represents an interface, this method returns true
            // if the class or any superclass of the specified Object argument implements
            // this interface"
            // GainControlClass.isInstance(MyGainObj) => true
            // GainControlClass.isInstance(GenericControlObj) => may be false
            // => that may be more specific
            if (!(info is Line))
            {
                return false;
            }
            // this.isAssignableFrom(that)  =>  this is same or super to that
            //								=>	this is at least as general as that
            //								=>	that may be subtype of this
            if (!(getLineClass().IsAssignableFrom(info.getLineClass())))
            {
                return false;
            }
            return true;
        }

        /// <summary>Obtains a textual description of the line info.</summary>
        /// <remarks>Obtains a textual description of the line info.</remarks>
        /// <returns>a string description</returns>
        public override string ToString()
        {
            string fullPackagePath = "javax.sound.sampled.";
            string initialString = getLineClass().ToString();
            string finalString;
            int index = initialString.IndexOf(fullPackagePath);
            if (index != -1)
            {
                finalString = cspeex.StringUtil.substring(initialString, 0, index) + cspeex.StringUtil.substring
                    (initialString, (index + fullPackagePath.Length), initialString.Length);
            }
            else
            {
                finalString = initialString;
            }
            return finalString;
        }
    }
    // class Info
}
