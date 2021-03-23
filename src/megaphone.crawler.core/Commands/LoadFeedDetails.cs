using CodeHollow.FeedReader;
using Megaphone.Crawler.Core.Models;
using Megaphone.Standard.Commands;
using Megaphone.Standard.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Megaphone.Crawler.Core.Commands
{
    internal class LoadFeedDetails : ICommand<Resource>
    {
        private readonly string content;

        private static Dictionary<string, string> timeZoneOffsets = new()
        {
            { "ACDT", "+10:30" },
            { "ACST", "+09:30" },
            { "ADT", "-03:00" },
            { "AEDT", "+11:00" },
            { "AEST", "+10:00" },
            { "AHDT", "-09:00" },
            { "AHST", "-10:00" },
            { "AST", "-04:00" },
            { "AT", "-02:00" },
            { "AWDT", "+09:00" },
            { "AWST", "+08:00" },
            { "BAT", "+03:00" },
            { "BDST", "+02:00" },
            { "BET", "-11:00" },
            { "BST", "-03:00" },
            { "BT", "+03:00" },
            { "BZT2", "-03:00" },
            { "CADT", "+10:30" },
            { "CAST", "+09:30" },
            { "CAT", "-10:00" },
            { "CCT", "+08:00" },
            { "CDT", "-05:00" },
            { "CED", "+02:00" },
            { "CET", "+01:00" },
            { "CEST", "+02:00" },
            { "CST", "-06:00" },
            { "EAST", "+10:00" },
            { "EDT", "-04:00" },
            { "EED", "+03:00" },
            { "EET", "+02:00" },
            { "EEST", "+03:00" },
            { "EST", "-05:00" },
            { "FST", "+02:00" },
            { "FWT", "+01:00" },
            { "GMT", "+00:00" },
            { "GST", "+10:00" },
            { "HDT", "-09:00" },
            { "HST", "-10:00" },
            { "IDLE", "+12:00" },
            { "IDLW", "-12:00" },
            { "IST", "+05:30" },
            { "IT", "+03:30" },
            { "JST", "+09:00" },
            { "JT", "+07:00" },
            { "MDT", "-06:00" },
            { "MED", "+02:00" },
            { "MET", "+01:00" },
            { "MEST", "+02:00" },
            { "MEWT", "+01:00" },
            { "MST", "-07:00" },
            { "MT", "+08:00" },
            { "NDT", "-02:30" },
            { "NFT", "-03:30" },
            { "NT", "-11:00" },
            { "NST", "+06:30" },
            { "NZ", "+11:00" },
            { "NZST", "+12:00" },
            { "NZDT", "+13:00" },
            { "NZT", "+12:00" },
            { "PDT", "-07:00" },
            { "PST", "-08:00" },
            { "ROK", "+09:00" },
            { "SAD", "+10:00" },
            { "SAST", "+09:00" },
            { "SAT", "+09:00" },
            { "SDT", "+10:00" },
            { "SST", "+02:00" },
            { "SWT", "+01:00" },
            { "USZ3", "+04:00" },
            { "USZ4", "+05:00" },
            { "USZ5", "+06:00" },
            { "USZ6", "+07:00" },
            { "UT", "-00:00" },
            { "UTC", "-00:00" },
            { "UZ10", "+11:00" },
            { "WAT", "-01:00" },
            { "WET", "-00:00" },
            { "WST", "+08:00" },
            { "YDT", "-08:00" },
            { "YST", "-09:00" },
            { "ZP4", "+04:00" },
            { "ZP5", "+05:00" },
            { "ZP6", "+06:00" },
            { "+0000", "-00:00" },
            { "-0800", "-08:00" },
            { "Z", "-00:00" }
        };

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

                DateTimeOffset published = DateTime.MinValue;

                if (i.PublishingDate.HasValue)
                {
                    published = i.PublishingDate.GetValueOrDefault();
                }
                if (!string.IsNullOrEmpty(i.PublishingDateString))
                {
                    if (DateTimeOffset.TryParse(i.PublishingDateString, out DateTimeOffset parsedDateTimeOffset))
                    {
                        published = parsedDateTimeOffset;
                    }
                    else
                    {
                        string dateString = i.PublishingDateString;
                        int timeZonePos = dateString.LastIndexOf(' ') + 1;
                        string tz = dateString.Substring(timeZonePos);
                        dateString = dateString.Substring(0, dateString.Length - tz.Length);
                        dateString += timeZoneOffsets[tz];

                        published = DateTimeOffset.ParseExact(dateString, "ddd, d MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture);
                    }
                }

                var uri = i.Link;
                return new Resource
                {
                    Id = uri.ToUri().ToGuid().ToString(),
                    Self = uri,
                    Published = published,
                    Display = i.Title,
                    Description = i.Description
                };
            }));
        }
    }
}
