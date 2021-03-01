using CodeHollow.FeedReader;
using Megaphone.Crawler.Core.Models;
using Megaphone.Standard.Commands;
using Megaphone.Standard.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core.Commands
{
    internal class LoadFeedDetails : ICommand<Resource>
    {
        private readonly string content;

        public LoadFeedDetails(string content)
        {
            this.content = content;
        }

        public async Task ApplyAsync(Resource model)
        {
            model.Type = ResourceType.Feed;

            var feed = FeedReader.ReadFromString(model.Cache);

            model.Published = feed.LastUpdatedDate.GetValueOrDefault();
            model.Display = feed.Title;
            model.Description = feed.Description;

            model.Resources.AddRange(feed.Items.Select(i =>
            {
                var uri = i.Link;
                return new Resource
                {
                    Id = uri.ToUri().ToGuid().ToString(),
                    Self = uri,
                    Published = i.PublishingDate.GetValueOrDefault(),
                    Display = i.Title,
                    Description = i.Description
                };
            }));
        }
    }
}
