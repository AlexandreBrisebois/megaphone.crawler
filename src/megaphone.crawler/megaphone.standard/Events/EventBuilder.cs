using System.Collections.Generic;

namespace Megaphone.Standard.Events
{
    public class EventBuilder
    {
        private readonly Event e;

        private EventBuilder(string eventName) => e = new Event(eventName);

        public static EventBuilder NewEvent(string eventName)
        {
            var b = new EventBuilder(eventName);
            return b;
        }
  
        public EventBuilder WithMetadata(string name, string value)
        {
            this.e.Metadata.Add(name, value);
            return this;
        }

        public EventBuilder WithData(string group, string name, string value)
        {
            if (!this.e.Data.ContainsKey(group))
                this.e.Data.Add(group, new Dictionary<string, string>());

            this.e.Data[group].Add(name, value);
            return this;
        }

        public Event Make()
        {
            return this.e;
        }
    }
}