namespace java.util
{
    /**
     * <p>
     * The root class from which all event state objects shall be derived.
     * <p>
     * All Events are constructed with a reference to the object, the "source",
     * that is logically deemed to be the object upon which the Event in question
     * initially occurred upon.
     * 
     * @since JDK1.1
     */
    public class EventObject
    {

        /**
         * The object on which the Event initially occurred.
         */
        protected object source;

        /**
         * Constructs a prototypical Event.
         *
         * @param    source    The object on which the Event initially occurred.
         * @exception  IllegalArgumentException  if source is null.
         */
        public EventObject(object source)
        {
            if (source == null)
                throw new System.ArgumentNullException("null source");

            this.source = source;
        }

        /**
         * The object on which the Event initially occurred.
         *
         * @return   The object on which the Event initially occurred.
         */
        public object getSource()
        {
            return source;
        }

        /**
         * Returns a String representation of this EventObject.
         *
         * @return  A a String representation of this EventObject.
         */
        public System.String toString()
        {
            return GetType().Name + "[source=" + source + "]";
        }
    }
}
