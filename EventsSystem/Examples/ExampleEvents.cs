using EventsSystem;

namespace EventsSystem.Example
{
    
    public struct ExampleEventOne : IEvent
    {
    
    }

    public struct ExampleEventTwo : IEvent
    {
        public int value;

        public ExampleEventTwo(int value)
        {
            this.value = value;
        }
    }
}