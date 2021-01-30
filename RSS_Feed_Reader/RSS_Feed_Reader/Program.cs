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
            
            FeedManager operations = new FeedManager("Feeds.xml");
            operations.Notify += DisplayMessage;

            string url = "http://feeds.feedburner.com/techulator/articles";
            string feedname1 = "Avoiding API Integration Mistakes for App Developers";
            string feedname2 = "Best Ways to Delete Similar Photos on Windows PC";
            operations.AddFeed(feedname1, url);
            operations.AddFeed(feedname2, url);

            operations.RemoveFeed(feedname1);

            operations.DownloadFeed(feedname2);           
            Console.WriteLine("done");
        }

        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        
    }
}
