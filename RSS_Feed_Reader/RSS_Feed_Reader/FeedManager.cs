using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RSS_Feed_Reader
{
    public class FeedManager
    {
        XmlFile file;

        public FeedManager(string filename)
        {
            file = new XmlFile();
            file.Filename = filename;
        }

        public void AddFeed(string feedName, string feedUrl)
        {
            file.CreateNode(feedName, feedUrl);
        }
        public void RemoveFeed(string feedname)
        {
            file.RemoveData("Name", feedname);
        }
        public void DownloadFeed(string name)
        {

        }
    }
}
