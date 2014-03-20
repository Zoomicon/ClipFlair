namespace javax.sound.sampled
{
	public class AudioSystem
	{
		/// <summary>An integer that stands for an unknown numeric value.</summary>
		/// <remarks>
		/// An integer that stands for an unknown numeric value.
		/// This value is appropriate only for signed quantities that do not
		/// normally take negative values.  Examples include file sizes, frame
		/// sizes, buffer sizes, and sample rates.
		/// A number of Java Sound constructors accept
		/// a value of <code>NOT_SPECIFIED</code> for such parameters.  Other
		/// methods may also accept or return this value, as documented.
		/// </remarks>
		public const int NOT_SPECIFIED = -1;
	}
}
