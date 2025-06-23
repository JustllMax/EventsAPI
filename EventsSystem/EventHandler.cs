using System.Collections.Generic;
using JetBrains.Annotations;

namespace EventsSystem
{
    
    /// <summary>
    /// Base interface for event handlers
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// Clears all registered callbacks.
        /// </summary>
        public void Clear();
    }

    /// <summary>
    /// Manages and invokes callbacks for events of type <typeparamref name="TEvent"/>.
    /// Provides methods to add, remove, and clear listeners.
    /// </summary>
    /// <typeparam name="TEvent">The struct type implementing <see cref="IEvent"/> to handle.</typeparam>
    internal sealed class EventHandler<TEvent> : IEventHandler 
        where TEvent : struct, IEvent 
    {
        
        private static EventHandler<TEvent> _instance;

        private static EventHandler<TEvent> GetInstance() => _instance ??= new EventHandler<TEvent>();

        /// <summary>
        /// List of all cached requests
        /// </summary>
        private readonly List<CallbackDelegate<TEvent>> _registeredCallbacks = new();
        
        /// <summary>
        /// Registers request callback
        /// </summary>
        public static void RegisterCallback(CallbackDelegate<TEvent> callback)
        {
            EventHandler<TEvent> instance = GetInstance();
            
            EventsAPI.EnsureHandlerStored(instance);
            

            // Register callback to instance if it does not exist
            if (!instance._registeredCallbacks.Contains(callback))
            {
                instance._registeredCallbacks.Add(callback);
            }
        }

        /// <summary>
        /// Unregister callback from this handler
        /// </summary>
        public static void UnregisterCallback([NotNull] CallbackDelegate<TEvent> callback)
        {
            EventHandler<TEvent> instance = GetInstance();
            
            // Unregister callback
            if (instance._registeredCallbacks.Contains(callback))
            {
                instance._registeredCallbacks.Remove(callback);
            }

            if (instance._registeredCallbacks.Count <= 0)
            {
                EventsAPI.RemoveEmptyHandler(instance);
            }
        }

        /// <summary>
        /// Invoke all registered callbacks to this handler
        /// </summary>
        public static void Invoke(ref TEvent data)
        {
            var callbacks = GetInstance()._registeredCallbacks;
            foreach (CallbackDelegate<TEvent> callbackDelegate in callbacks)
            {
                callbackDelegate.Invoke(ref data);
            }
        }
        
        /// <summary>
        /// Invoke all callbacks registered to this handler
        /// </summary>
        public void Clear()
        {
            _registeredCallbacks.Clear();
        }
    }
}