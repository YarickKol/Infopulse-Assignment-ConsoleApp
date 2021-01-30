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
            operations.Notify += DisplayMessage;


            operations.DownloadFeed("Windows");           
            Console.WriteLine("done");
        }

        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
