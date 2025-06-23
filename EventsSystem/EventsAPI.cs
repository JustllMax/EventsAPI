using System.Collections.Generic;
using UnityEngine;

namespace EventsSystem
{
    public static class EventsAPI
    {
        /// <summary>
        /// A set of all registered event handler instances, used to clear or remove handlers in bulk.
        /// </summary>
        private static readonly HashSet<IEventHandler> Handlers = new();


        /// <summary>
        /// Ensures the specified handler instance is cached.
        /// </summary>
        /// <typeparam name="TCallbackHandler">The concrete type of the handler implementing <see cref="IEventHandler"/>.</typeparam>
        /// <param name="handler">The handler instance to track.</param>
        public static void EnsureHandlerStored<TCallbackHandler>(TCallbackHandler handler)
        where TCallbackHandler : IEventHandler
        {
            Handlers.Add(handler);
        }
        
        /// <summary>
        /// Removes the specified handler type from tracking, called by the handler.
        /// </summary>
        /// <typeparam name="TCallbackHandler">The concrete type of the handler to remove.</typeparam>
        /// <param name="handler">The handler instance to remove.</param>
        public static void RemoveEmptyHandler<TCallbackHandler>(TCallbackHandler handler)
        where TCallbackHandler : IEventHandler
        {
            Handlers.Remove(handler);
        }
        
        /// <summary>
        /// Registers a callback for a specific event type, forwarding to the corresponding EventHandler.
        /// </summary>
        /// <typeparam name="TEventType">The struct type implementing <see cref="IEvent"/>.</typeparam>
        /// <param name="callback">The delegate to invoke when the event is fired.</param>
        public static void Register<TEventType>(CallbackDelegate<TEventType> callback)
        where TEventType : struct, IEvent
        {
            EventHandler<TEventType>.RegisterCallback(callback);
        } 
        
        /// <summary>
        /// Unregisters a previously registered callback for a specific event type.
        /// </summary>
        /// <typeparam name="TEventType">The struct type implementing <see cref="IEvent"/>.</typeparam>
        /// <param name="callback">The delegate to remove.</param>
        public static void Unregister<TEventType>(CallbackDelegate<TEventType> callback)
            where TEventType : struct, IEvent
        {
            EventHandler<TEventType>.UnregisterCallback(callback);
        }

        /// <summary>
        /// Invokes all callbacks registered for the given event data.
        /// </summary>
        /// <typeparam name="TDelegateType">The struct type implementing <see cref="IEvent"/>.</typeparam>
        /// <param name="data">The event data passed by reference.</param>
        public static void Invoke<TDelegateType>(ref TDelegateType data)
            where TDelegateType : struct, IEvent
        {
            EventHandler<TDelegateType>.Invoke(ref data);
        }

        /// <summary>
        /// Creates a default instance of the event type and invokes all registered callbacks.
        /// </summary>
        /// <typeparam name="TDelegateType">The struct type implementing <see cref="IEvent"/>.</typeparam>
        public static void Invoke<TDelegateType>()
            where TDelegateType : struct, IEvent
        {
            TDelegateType requestData = new TDelegateType();
            EventHandler<TDelegateType>.Invoke(ref requestData);
        }

        /// <summary>
        /// Clears all registered callbacks across all tracked handlers and logs the action.
        /// </summary>
        public static void Clear()
        {
            foreach (var handler in Handlers)
            {
                handler.Clear();
            }
            Handlers.Clear();
        }
    }
}
