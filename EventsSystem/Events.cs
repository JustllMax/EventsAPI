namespace EventsSystem
{
    /// <summary>
    /// Base interface for Events
    /// </summary>
    public interface IEvent { }
    
    /// <summary>
    /// Represents a callback that is invoked when an event of type <typeparamref name="TEvent"/> occurs.
    /// </summary>
    /// <typeparam name="TEvent">The struct type implementing <see cref="IEvent"/> to handle.</typeparam>
    public delegate void CallbackDelegate<TEvent>(ref TEvent type) where TEvent : struct, IEvent;
    
}