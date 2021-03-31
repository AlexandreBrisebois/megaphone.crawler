using Megaphone.Crawler.Core.Models;
using Megaphone.Standard.Messages;

namespace Megaphone.Crawler.Models
{
    internal static class CommandMessageFactory
    {
        public static CommandMessage Make(string uri)
        {
            return MessageBuilder.NewCommand("crawl-request")
                                        .WithParameters("uri", uri)
                                        .Make();
        }

        public static CommandMessage Make(Resource resource)
        {
            return MessageBuilder.NewCommand("crawl-request")
                                         .WithParameters("uri", resource.Self)
                                         .WithParameters("display", resource.Display)
                                         .WithParameters("description", resource.Description)
                                         .WithParameters("published", resource.Published.ToString())
                                         .Make();
        }
    }
}
