namespace javax.sound.sampled
{
	/// <summary>
	/// <code>DataLine</code> adds media-related functionality to its
	/// superinterface, <code>
	/// <see cref="Line">Line</see>
	/// </code>.  This functionality includes
	/// transport-control methods that start, stop, drain, and flush
	/// the audio data that passes through the line.  A data line can also
	/// report the current position, volume, and audio format of the media.
	/// Data lines are used for output of audio by means of the
	/// subinterfaces <code>
	/// <see cref="SourceDataLine">SourceDataLine</see>
	/// </code> or
	/// <code>
	/// <see cref="Clip">Clip</see>
	/// </code>, which allow an application program to write data.  Similarly,
	/// audio input is handled by the subinterface <code>
	/// <see cref="TargetDataLine">TargetDataLine</see>
	/// </code>,
	/// which allows data to be read.
	/// <p>
	/// A data line has an internal buffer in which
	/// the incoming or outgoing audio data is queued.  The
	/// <code>
	/// <see cref="drain()">drain()</see>
	/// </code> method blocks until this internal buffer
	/// becomes empty, usually because all queued data has been processed.  The
	/// <code>
	/// <see cref="flush()">flush()</see>
	/// </code> method discards any available queued data
	/// from the internal buffer.
	/// <p>
	/// A data line produces <code>
	/// <see cref="Type.START">START</see>
	/// </code> and
	/// <code>
	/// <see cref="Type.STOP">STOP</see>
	/// </code> events whenever
	/// it begins or ceases active presentation or capture of data.  These events
	/// can be generated in response to specific requests, or as a result of
	/// less direct state changes.  For example, if <code>
	/// <see cref="start()">start()</see>
	/// </code> is called
	/// on an inactive data line, and data is available for capture or playback, a
	/// <code>START</code> event will be generated shortly, when data playback
	/// or capture actually begins.  Or, if the flow of data to an active data
	/// line is constricted so that a gap occurs in the presentation of data,
	/// a <code>STOP</code> event is generated.
	/// <p>
	/// Mixers often support synchronized control of multiple data lines.
	/// Synchronization can be established through the Mixer interface's
	/// <code>
	/// <see cref="Mixer#synchronize">synchronize</see>
	/// </code> method.
	/// See the description of the <code>
	/// <see cref="Mixer">Mixer</see>
	/// </code> interface
	/// for a more complete description.
	/// </summary>
	/// <author>Kara Kytle</author>
	/// <version>1.34, 05/11/17</version>
	/// <seealso cref="LineEvent">LineEvent</seealso>
	/// <since>1.3</since>
	public interface DataLine : Line
	{
		/// <summary>
		/// Drains queued data from the line by continuing data I/O until the
		/// data line's internal buffer has been emptied.
		/// </summary>
		/// <remarks>
		/// Drains queued data from the line by continuing data I/O until the
		/// data line's internal buffer has been emptied.
		/// This method blocks until the draining is complete.  Because this is a
		/// blocking method, it should be used with care.  If <code>drain()</code>
		/// is invoked on a stopped line that has data in its queue, the method will
		/// block until the line is running and the data queue becomes empty.  If
		/// <code>drain()</code> is invoked by one thread, and another continues to
		/// fill the data queue, the operation will not complete.
		/// This method always returns when the data line is closed.
		/// </remarks>
		/// <seealso cref="flush()">flush()</seealso>
		void drain();

		/// <summary>Flushes queued data from the line.</summary>
		/// <remarks>
		/// Flushes queued data from the line.  The flushed data is discarded.
		/// In some cases, not all queued data can be discarded.  For example, a
		/// mixer can flush data from the buffer for a specific input line, but any
		/// unplayed data already in the output buffer (the result of the mix) will
		/// still be played.  You can invoke this method after pausing a line (the
		/// normal case) if you want to skip the "stale" data when you restart
		/// playback or capture. (It is legal to flush a line that is not stopped,
		/// but doing so on an active line is likely to cause a discontinuity in the
		/// data, resulting in a perceptible click.)
		/// </remarks>
		/// <seealso cref="stop()">stop()</seealso>
		/// <seealso cref="drain()">drain()</seealso>
		void flush();

		/// <summary>Allows a line to engage in data I/O.</summary>
		/// <remarks>
		/// Allows a line to engage in data I/O.  If invoked on a line
		/// that is already running, this method does nothing.  Unless the data in
		/// the buffer has been flushed, the line resumes I/O starting
		/// with the first frame that was unprocessed at the time the line was
		/// stopped. When audio capture or playback starts, a
		/// <code>
		/// <see cref="Type.START">START</see>
		/// </code> event is generated.
		/// </remarks>
		/// <seealso cref="stop()">stop()</seealso>
		/// <seealso cref="isRunning()">isRunning()</seealso>
		/// <seealso cref="LineEvent">LineEvent</seealso>
		void start();

		/// <summary>Stops the line.</summary>
		/// <remarks>
		/// Stops the line.  A stopped line should cease I/O activity.
		/// If the line is open and running, however, it should retain the resources required
		/// to resume activity.  A stopped line should retain any audio data in its buffer
		/// instead of discarding it, so that upon resumption the I/O can continue where it left off,
		/// if possible.  (This doesn't guarantee that there will never be discontinuities beyond the
		/// current buffer, of course; if the stopped condition continues
		/// for too long, input or output samples might be dropped.)  If desired, the retained data can be
		/// discarded by invoking the <code>flush</code> method.
		/// When audio capture or playback stops, a <code>
		/// <see cref="Type.STOP">STOP</see>
		/// </code> event is generated.
		/// </remarks>
		/// <seealso cref="start()">start()</seealso>
		/// <seealso cref="isRunning()">isRunning()</seealso>
		/// <seealso cref="flush()">flush()</seealso>
		/// <seealso cref="LineEvent">LineEvent</seealso>
		void stop();

		/// <summary>Indicates whether the line is running.</summary>
		/// <remarks>
		/// Indicates whether the line is running.  The default is <code>false</code>.
		/// An open line begins running when the first data is presented in response to an
		/// invocation of the <code>start</code> method, and continues
		/// until presentation ceases in response to a call to <code>stop</code> or
		/// because playback completes.
		/// </remarks>
		/// <returns><code>true</code> if the line is running, otherwise <code>false</code></returns>
		/// <seealso cref="start()">start()</seealso>
		/// <seealso cref="stop()">stop()</seealso>
		bool isRunning();

		/// <summary>
		/// Indicates whether the line is engaging in active I/O (such as playback
		/// or capture).
		/// </summary>
		/// <remarks>
		/// Indicates whether the line is engaging in active I/O (such as playback
		/// or capture).  When an inactive line becomes active, it sends a
		/// <code>
		/// <see cref="Type.START">START</see>
		/// </code> event to its listeners.  Similarly, when
		/// an active line becomes inactive, it sends a
		/// <code>
		/// <see cref="Type.STOP">STOP</see>
		/// </code> event.
		/// </remarks>
		/// <returns>
		/// <code>true</code> if the line is actively capturing or rendering
		/// sound, otherwise <code>false</code>
		/// </returns>
		/// <seealso cref="Line.isOpen()">Line.isOpen()</seealso>
		/// <seealso cref="Line.addLineListener(LineListener)">Line.addLineListener(LineListener)
		/// 	</seealso>
		/// <seealso cref="Line.removeLineListener(LineListener)">Line.removeLineListener(LineListener)
		/// 	</seealso>
		/// <seealso cref="LineEvent">LineEvent</seealso>
		/// <seealso cref="LineListener">LineListener</seealso>
		bool isActive();

		/// <summary>
		/// Obtains the current format (encoding, sample rate, number of channels,
		/// etc.) of the data line's audio data.
		/// </summary>
		/// <remarks>
		/// Obtains the current format (encoding, sample rate, number of channels,
		/// etc.) of the data line's audio data.
		/// <p>If the line is not open and has never been opened, it returns
		/// the default format. The default format is an implementation
		/// specific audio format, or, if the <code>DataLine.Info</code>
		/// object, which was used to retrieve this <code>DataLine</code>,
		/// specifies at least one fully qualified audio format, the
		/// last one will be used as the default format. Opening the
		/// line with a specific audio format (e.g.
		/// <see cref="SourceDataLine#open(AudioFormat)">SourceDataLine#open(AudioFormat)</see>
		/// ) will override the
		/// default format.
		/// </remarks>
		/// <returns>current audio data format</returns>
		/// <seealso cref="AudioFormat">AudioFormat</seealso>
		AudioFormat getFormat();

		/// <summary>
		/// Obtains the maximum number of bytes of data that will fit in the data line's
		/// internal buffer.
		/// </summary>
		/// <remarks>
		/// Obtains the maximum number of bytes of data that will fit in the data line's
		/// internal buffer.  For a source data line, this is the size of the buffer to
		/// which data can be written.  For a target data line, it is the size of
		/// the buffer from which data can be read.  Note that
		/// the units used are bytes, but will always correspond to an integral
		/// number of sample frames of audio data.
		/// </remarks>
		/// <returns>the size of the buffer in bytes</returns>
		int getBufferSize();

		/// <summary>
		/// Obtains the number of bytes of data currently available to the
		/// application for processing in the data line's internal buffer.
		/// </summary>
		/// <remarks>
		/// Obtains the number of bytes of data currently available to the
		/// application for processing in the data line's internal buffer.  For a
		/// source data line, this is the amount of data that can be written to the
		/// buffer without blocking.  For a target data line, this is the amount of data
		/// available to be read by the application.  For a clip, this value is always
		/// 0 because the audio data is loaded into the buffer when the clip is opened,
		/// and persists without modification until the clip is closed.
		/// <p>
		/// Note that the units used are bytes, but will always
		/// correspond to an integral number of sample frames of audio data.
		/// <p>
		/// An application is guaranteed that a read or
		/// write operation of up to the number of bytes returned from
		/// <code>available()</code> will not block; however, there is no guarantee
		/// that attempts to read or write more data will block.
		/// </remarks>
		/// <returns>the amount of data available, in bytes</returns>
		int available();

		/// <summary>Obtains the current position in the audio data, in sample frames.</summary>
		/// <remarks>
		/// Obtains the current position in the audio data, in sample frames.
		/// The frame position measures the number of sample
		/// frames captured by, or rendered from, the line since it was opened.
		/// This return value will wrap around after 2^31 frames. It is recommended
		/// to use <code>getLongFramePosition</code> instead.
		/// </remarks>
		/// <returns>the number of frames already processed since the line was opened</returns>
		/// <seealso cref="getLongFramePosition()">getLongFramePosition()</seealso>
		int getFramePosition();

		/// <summary>Obtains the current position in the audio data, in sample frames.</summary>
		/// <remarks>
		/// Obtains the current position in the audio data, in sample frames.
		/// The frame position measures the number of sample
		/// frames captured by, or rendered from, the line since it was opened.
		/// </remarks>
		/// <returns>the number of frames already processed since the line was opened</returns>
		/// <since>1.5</since>
		long getLongFramePosition();

		/// <summary>Obtains the current position in the audio data, in microseconds.</summary>
		/// <remarks>
		/// Obtains the current position in the audio data, in microseconds.
		/// The microsecond position measures the time corresponding to the number
		/// of sample frames captured by, or rendered from, the line since it was opened.
		/// The level of precision is not guaranteed.  For example, an implementation
		/// might calculate the microsecond position from the current frame position
		/// and the audio sample frame rate.  The precision in microseconds would
		/// then be limited to the number of microseconds per sample frame.
		/// </remarks>
		/// <returns>the number of microseconds of data processed since the line was opened</returns>
		long getMicrosecondPosition();

		/// <summary>Obtains the current volume level for the line.</summary>
		/// <remarks>
		/// Obtains the current volume level for the line.  This level is a measure
		/// of the signal's current amplitude, and should not be confused with the
		/// current setting of a gain control. The range is from 0.0 (silence) to
		/// 1.0 (maximum possible amplitude for the sound waveform).  The units
		/// measure linear amplitude, not decibels.
		/// </remarks>
		/// <returns>
		/// the current amplitude of the signal in this line, or
		/// <code>
		/// <see cref="AudioSystem.NOT_SPECIFIED">AudioSystem.NOT_SPECIFIED</see>
		/// </code>
		/// </returns>
		float getLevel();
	}

    /// <summary>
    /// Besides the class information inherited from its superclass,
    /// <code>DataLine.Info</code> provides additional information specific to data lines.
    /// </summary>
    /// <remarks>
    /// Besides the class information inherited from its superclass,
    /// <code>DataLine.Info</code> provides additional information specific to data lines.
    /// This information includes:
    /// <ul>
    /// <li> the audio formats supported by the data line
    /// <li> the minimum and maximum sizes of its internal buffer
    /// </ul>
    /// Because a <code>Line.Info</code> knows the class of the line its describes, a
    /// <code>DataLine.Info</code> object can describe <code>DataLine</code>
    /// subinterfaces such as <code>
    /// <see cref="SourceDataLine">SourceDataLine</see>
    /// </code>,
    /// <code>
    /// <see cref="TargetDataLine">TargetDataLine</see>
    /// </code>, and <code>
    /// <see cref="Clip">Clip</see>
    /// </code>.
    /// You can query a mixer for lines of any of these types, passing an appropriate
    /// instance of <code>DataLine.Info</code> as the argument to a method such as
    /// <code>
    /// <see cref="Mixer#getLine">Mixer.getLine(Line.Info)</see>
    /// </code>.
    /// </remarks>
    /// <seealso cref="Info">Info</seealso>
    /// <author>Kara Kytle</author>
    /// <version>1.34, 05/11/17</version>
    /// <since>1.3</since>
    public class DataLineInfo : LineInfo
    {
        private AudioFormat[] formats;

        private int minBufferSize;

        private int maxBufferSize;

        /// <summary>
        /// Constructs a data line's info object from the specified information,
        /// which includes a set of supported audio formats and a range for the buffer size.
        /// </summary>
        /// <remarks>
        /// Constructs a data line's info object from the specified information,
        /// which includes a set of supported audio formats and a range for the buffer size.
        /// This constructor is typically used by mixer implementations
        /// when returning information about a supported line.
        /// </remarks>
        /// <param name="lineClass">the class of the data line described by the info object</param>
        /// <param name="formats">set of formats supported</param>
        /// <param name="minBufferSize">minimum buffer size supported by the data line, in bytes
        /// 	</param>
        /// <param name="maxBufferSize">maximum buffer size supported by the data line, in bytes
        /// 	</param>
        public DataLineInfo(System.Type lineClass, AudioFormat[] formats, int minBufferSize
            , int maxBufferSize)
            : base(lineClass)
        {
            if (formats == null)
            {
                this.formats = new AudioFormat[0];
            }
            else
            {
                this.formats = formats;
            }
            this.minBufferSize = minBufferSize;
            this.maxBufferSize = maxBufferSize;
        }

        /// <summary>
        /// Constructs a data line's info object from the specified information,
        /// which includes a single audio format and a desired buffer size.
        /// </summary>
        /// <remarks>
        /// Constructs a data line's info object from the specified information,
        /// which includes a single audio format and a desired buffer size.
        /// This constructor is typically used by an application to
        /// describe a desired line.
        /// </remarks>
        /// <param name="lineClass">the class of the data line described by the info object</param>
        /// <param name="format">desired format</param>
        /// <param name="bufferSize">desired buffer size in bytes</param>
        public DataLineInfo(System.Type lineClass, AudioFormat format, int bufferSize
            )
            : base(lineClass)
        {
            if (format == null)
            {
                this.formats = new AudioFormat[0];
            }
            else
            {
                AudioFormat[] formats = new AudioFormat[] { format };
                this.formats = formats;
            }
            this.minBufferSize = bufferSize;
            this.maxBufferSize = bufferSize;
        }

        /// <summary>
        /// Constructs a data line's info object from the specified information,
        /// which includes a single audio format.
        /// </summary>
        /// <remarks>
        /// Constructs a data line's info object from the specified information,
        /// which includes a single audio format.
        /// This constructor is typically used by an application to
        /// describe a desired line.
        /// </remarks>
        /// <param name="lineClass">the class of the data line described by the info object</param>
        /// <param name="format">desired format</param>
        public DataLineInfo(System.Type lineClass, AudioFormat format)
            : this(lineClass
                , format, AudioSystem.NOT_SPECIFIED)
        {
        }

        /// <summary>Obtains a set of audio formats supported by the data line.</summary>
        /// <remarks>
        /// Obtains a set of audio formats supported by the data line.
        /// Note that <code>isFormatSupported(AudioFormat)</code> might return
        /// <code>true</code> for certain additional formats that are missing from
        /// the set returned by <code>getFormats()</code>.  The reverse is not
        /// the case: <code>isFormatSupported(AudioFormat)</code> is guaranteed to return
        /// <code>true</code> for all formats returned by <code>getFormats()</code>.
        /// Some fields in the AudioFormat instances can be set to
        /// <see cref="javax.sound.sampled.AudioSystem.NOT_SPECIFIED">NOT_SPECIFIED</see>
        /// if that field does not apply to the format,
        /// or if the format supports a wide range of values for that field.
        /// For example, a multi-channel device supporting up to
        /// 64 channels, could set the channel field in the
        /// <code>AudioFormat</code> instances returned by this
        /// method to <code>NOT_SPECIFIED</code>.
        /// </remarks>
        /// <returns>a set of supported audio formats.</returns>
        /// <seealso cref="isFormatSupported(AudioFormat)">isFormatSupported(AudioFormat)</seealso>
        public virtual AudioFormat[] getFormats()
        {
            AudioFormat[] returnedArray = new AudioFormat[formats.Length];
            System.Array.Copy(formats, 0, returnedArray, 0, formats.Length);
            return returnedArray;
        }

        /// <summary>Indicates whether this data line supports a particular audio format.</summary>
        /// <remarks>
        /// Indicates whether this data line supports a particular audio format.
        /// The default implementation of this method simply returns <code>true</code> if
        /// the specified format matches any of the supported formats.
        /// </remarks>
        /// <param name="format">the audio format for which support is queried.</param>
        /// <returns><code>true</code> if the format is supported, otherwise <code>false</code>
        /// 	</returns>
        /// <seealso cref="getFormats()">getFormats()</seealso>
        /// <seealso cref="AudioFormat.matches(AudioFormat)">AudioFormat.matches(AudioFormat)
        /// 	</seealso>
        public virtual bool isFormatSupported(AudioFormat format)
        {
            for (int i = 0; i < formats.Length; i++)
            {
                if (format.matches(formats[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Obtains the minimum buffer size supported by the data line.</summary>
        /// <remarks>Obtains the minimum buffer size supported by the data line.</remarks>
        /// <returns>minimum buffer size in bytes, or <code>AudioSystem.NOT_SPECIFIED</code></returns>
        public virtual int getMinBufferSize()
        {
            return minBufferSize;
        }

        /// <summary>Obtains the maximum buffer size supported by the data line.</summary>
        /// <remarks>Obtains the maximum buffer size supported by the data line.</remarks>
        /// <returns>maximum buffer size in bytes, or <code>AudioSystem.NOT_SPECIFIED</code></returns>
        public virtual int getMaxBufferSize()
        {
            return maxBufferSize;
        }

        /// <summary>Determines whether the specified info object matches this one.</summary>
        /// <remarks>
        /// Determines whether the specified info object matches this one.
        /// To match, the superclass match requirements must be met.  In
        /// addition, this object's minimum buffer size must be at least as
        /// large as that of the object specified, its maximum buffer size must
        /// be at most as large as that of the object specified, and all of its
        /// formats must match formats supported by the object specified.
        /// </remarks>
        /// <returns>
        /// <code>true</code> if this object matches the one specified,
        /// otherwise <code>false</code>.
        /// </returns>
        public override bool matches(LineInfo info)
        {
            if (!(base.matches(info)))
            {
                return false;
            }
            DataLineInfo dataLineInfo = (DataLineInfo)info;
            // treat anything < 0 as NOT_SPECIFIED
            // demo code in old Java Sound Demo used a wrong buffer calculation
            // that would lead to arbitrary negative values
            if ((getMaxBufferSize() >= 0) && (dataLineInfo.getMaxBufferSize() >= 0))
            {
                if (getMaxBufferSize() > dataLineInfo.getMaxBufferSize())
                {
                    return false;
                }
            }
            if ((getMinBufferSize() >= 0) && (dataLineInfo.getMinBufferSize() >= 0))
            {
                if (getMinBufferSize() < dataLineInfo.getMinBufferSize())
                {
                    return false;
                }
            }
            AudioFormat[] localFormats = getFormats();
            if (localFormats != null)
            {
                for (int i = 0; i < localFormats.Length; i++)
                {
                    if (!(localFormats[i] == null))
                    {
                        if (!(dataLineInfo.isFormatSupported(localFormats[i])))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>Obtains a textual description of the data line info.</summary>
        /// <remarks>Obtains a textual description of the data line info.</remarks>
        /// <returns>a string description</returns>
        public override string ToString()
        {
            System.Text.StringBuilder buf = new System.Text.StringBuilder();
            if ((formats.Length == 1) && (formats[0] != null))
            {
                buf.Append(" supporting format " + formats[0]);
            }
            else
            {
                if (getFormats().Length > 1)
                {
                    buf.Append(" supporting " + getFormats().Length + " audio formats");
                }
            }
            if ((minBufferSize != AudioSystem.NOT_SPECIFIED) && (maxBufferSize != AudioSystem
                .NOT_SPECIFIED))
            {
                buf.Append(", and buffers of " + minBufferSize + " to " + maxBufferSize + " bytes"
                    );
            }
            else
            {
                if ((minBufferSize != AudioSystem.NOT_SPECIFIED) && (minBufferSize > 0))
                {
                    buf.Append(", and buffers of at least " + minBufferSize + " bytes");
                }
                else
                {
                    if (maxBufferSize != AudioSystem.NOT_SPECIFIED)
                    {
                        buf.Append(", and buffers of up to " + minBufferSize + " bytes");
                    }
                }
            }
            return base.ToString() + buf;
        }
    }
    // class Info
}
