namespace javax.sound.sampled
{
	/// <summary>
	/// <see cref="Line">Lines</see>
	/// often have a set of controls, such as gain and pan, that affect
	/// the audio signal passing through the line.  Java Sound's <code>Line</code> objects
	/// let you obtain a particular control object by passing its class as the
	/// argument to a
	/// <see cref="Line.getControl(Type)">getControl</see>
	/// method.
	/// <p>
	/// Because the various types of controls have different purposes and features,
	/// all of their functionality is accessed from the subclasses that define
	/// each kind of control.
	/// </summary>
	/// <author>Kara Kytle</author>
	/// <version>1.26, 05/11/17</version>
	/// <seealso cref="Line.getControls()">Line.getControls()</seealso>
	/// <seealso cref="Line.isControlSupported(Type)">Line.isControlSupported(Type)</seealso>
	/// <since>1.3</since>
	public abstract class Control
	{
		/// <summary>The control type.</summary>
		/// <remarks>The control type.</remarks>
		private readonly Control.Type type;

		/// <summary>Constructs a Control with the specified type.</summary>
		/// <remarks>Constructs a Control with the specified type.</remarks>
		/// <param name="type">the kind of control desired</param>
		protected Control(Control.Type type)
		{
			// INSTANCE VARIABLES
			// CONSTRUCTORS
			this.type = type;
		}

		// METHODS
		/// <summary>Obtains the control's type.</summary>
		/// <remarks>Obtains the control's type.</remarks>
		/// <returns>the control's type.</returns>
		public virtual Control.Type getType()
		{
			return type;
		}

		// ABSTRACT METHODS
		/// <summary>Obtains a String describing the control type and its current state.</summary>
		/// <remarks>Obtains a String describing the control type and its current state.</remarks>
		/// <returns>a String representation of the Control.</returns>
		public override string ToString()
		{
			return getType() + " Control";
		}

		/// <summary>
		/// An instance of the <code>Type</code> class represents the type of
		/// the control.
		/// </summary>
		/// <remarks>
		/// An instance of the <code>Type</code> class represents the type of
		/// the control.  Static instances are provided for the
		/// common types.
		/// </remarks>
		public class Type
		{
			/// <summary>Type name.</summary>
			/// <remarks>Type name.</remarks>
			private string name;

			/// <summary>Constructs a new control type with the name specified.</summary>
			/// <remarks>
			/// Constructs a new control type with the name specified.
			/// The name should be a descriptive string appropriate for
			/// labelling the control in an application, such as "Gain" or "Balance."
			/// </remarks>
			/// <param name="name">the name of the new control type.</param>
			protected Type(string name)
			{
				// CONTROL TYPE DEFINES
				// INSTANCE VARIABLES
				// CONSTRUCTOR
				this.name = name;
			}

			// METHODS
			/// <summary>Finalizes the equals method</summary>
			public sealed override bool Equals(object obj)
			{
				return base.Equals(obj);
			}

			/// <summary>Finalizes the hashCode method</summary>
			public sealed override int GetHashCode()
			{
				return base.GetHashCode();
			}

			/// <summary>Provides the <code>String</code> representation of the control type.</summary>
			/// <remarks>
			/// Provides the <code>String</code> representation of the control type.  This <code>String</code> is
			/// the same name that was passed to the constructor.
			/// </remarks>
			/// <returns>the control type name</returns>
			public sealed override string ToString()
			{
				return name;
			}
		}
		// class Type
	}
}
