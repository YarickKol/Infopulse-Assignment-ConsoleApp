using System;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace RSS_Feed_Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            FeedManager operations = new FeedManager("FirstTry.xml");
            string url = "http://feeds.feedburner.com/techulator/articles";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            //foreach (SyndicationItem item in feed.Items)
            //{
            //    operations.AddFeed(item.Title.Text, url);

            //}
            operations.RemoveFeed("3 Ways to Configure a Proxy Server on Android");
            Console.WriteLine("done");
        }        
    }
}
