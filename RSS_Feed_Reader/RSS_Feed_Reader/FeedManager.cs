using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RSS_Feed_Reader
{
    public class FeedManager
    {
        public delegate void FeedHandler(string message);
        public event FeedHandler Notify;

        XmlFile file;
        
        public FeedManager(string filename)
        {
            file = new XmlFile();
            file.Filename = filename;                     
        }

        public void AddFeed(string feedName, string feedUrl)
        {
            file.CreateNode(feedName, feedUrl);
            Notify("Added new feed to xml");
        }

        public void RemoveFeed(string feedname)
        {
            file.RemoveData("Name", feedname);
            Notify("Removed feed from xml");
        }

        public void DownloadFeed(string feedName)
        {
            if (FeedExists(feedName))
            {            
                DownloadOneFeed(feedName);
            }
            else
            {
                Notify("No feed with such headline found");
                DownloadAllFeed();                
            }            
        }

        /// <summary>
        /// TO DO: fix searching of several feeds that contains similar words
        /// </summary>
        
        private void DownloadOneFeed(string feedName)
        {
            XmlReader reader = XmlReader.Create(GetUrl(feedName));
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                if (item.Title.Text == feedName)
                {
                    Notify(FeedInfo(item.Title.Text, item.Summary.Text));                    
                }
                else if (item.Title.Text.Contains(feedName))
                {
                    Notify("There were found several feed with similar name");
                }
            }            
        }

        private void DownloadAllFeed()
        {
            string url = "http://feeds.feedburner.com/techulator/articles";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                Notify(FeedInfo(item.Title.Text, item.Summary.Text));
            }
        }
        private bool FeedExists(string feedName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file.Filename);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.SelectSingleNode(@"Name").InnerText == feedName ||
                    node.SelectSingleNode(@"Name").InnerText.Contains(feedName))
                    return true;
            }
            return false;
        }

        private string GetUrl(string feedName)
        {
            string url = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load(file.Filename);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.SelectSingleNode(@"Name").InnerText == feedName)
                {
                    url = node.SelectSingleNode(@"Url").InnerText;
                    break;
                }
            }
            return url;
        }

        private string FeedInfo(string headline, string summary)
        {
            return $"Feed`s name: {headline} \nFeed`s Summary: {summary}";
        }
    }
}
