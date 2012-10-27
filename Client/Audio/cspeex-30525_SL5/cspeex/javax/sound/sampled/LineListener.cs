namespace javax.sound.sampled
{
	/// <summary>
	/// Instances of classes that implement the <code>LineListener</code> interface can register to
	/// receive events when a line's status changes.
	/// </summary>
	/// <remarks>
	/// Instances of classes that implement the <code>LineListener</code> interface can register to
	/// receive events when a line's status changes.
	/// </remarks>
	/// <author>Kara Kytle</author>
	/// <version>1.10 05/11/17</version>
	/// <seealso cref="Line">Line</seealso>
	/// <seealso cref="Line.addLineListener(LineListener)">Line.addLineListener(LineListener)
	/// 	</seealso>
	/// <seealso cref="Line.removeLineListener(LineListener)">Line.removeLineListener(LineListener)
	/// 	</seealso>
	/// <seealso cref="LineEvent">LineEvent</seealso>
	/// <since>1.3</since>
	public interface LineListener //: java.util.EventListener
	{
		/// <summary>Informs the listener that a line's state has changed.</summary>
		/// <remarks>
		/// Informs the listener that a line's state has changed.  The listener can then invoke
		/// <code>LineEvent</code> methods to obtain information about the event.
		/// </remarks>
		/// <param name="event">a line event that describes the change</param>
		void update(LineEvent @event);
	}
}
