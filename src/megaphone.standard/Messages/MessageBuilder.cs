namespace Megaphone.Standard.Messages
{
    public class MessageBuilder
    {
        private readonly CommandMessage e;

        private MessageBuilder(string eventName) => e = new CommandMessage(eventName);

        public static MessageBuilder NewCommand(string eventName)
        {
            var b = new MessageBuilder(eventName);
            return b;
        }

        public MessageBuilder WithMetadata(string name, string value)
        {
            e.Metadata.Add(name, value);
            return this;
        } 
        
        public MessageBuilder WithParameters(string name, string value)
        {
            e.Parameters.Add(name, value);
            return this;
        }

        public CommandMessage Make()
        {
            return e;
        }
    }
}