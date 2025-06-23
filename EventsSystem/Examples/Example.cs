using UnityEngine;
using EventsSystem;

namespace EventsSystem.Example
{
    public class Example : MonoBehaviour
    {
        private void OnEnable()
        {
            // Registering callback to event
            EventsAPI.Register<ExampleEventOne>(OnEventInvoked); 
        }

        private void Start()
        {
            // Crating an event with some data
            var example = new ExampleEventTwo(5);
        
            // Invoking event that has data
            EventsAPI.Invoke<ExampleEventTwo>(ref example);
        
            // Invoke event without data
            EventsAPI.Invoke<ExampleEventOne>();
        }

        private void OnEventInvoked(ref ExampleEventOne @event)
        {
            //Do something
        }

        private void OnDisable()
        {
            // Removing callback to event
            EventsAPI.Unregister<ExampleEventOne>(OnEventInvoked); 
        }
    }
}
