using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RSS_Feed_Reader
{
    public class XmlFile
    {
        public string Filename { get; set; }

        public void CreateNode(string feedName, string feedURL)
        {
            if (!File.Exists(Filename))
            {
                CreateNewData(feedName, feedURL);
            }
            else
            {
                LoadToOldData(feedName, feedURL);
            }

        }

        public void RemoveData(string nodeToDelete, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Filename);
            if (xmlDoc.DocumentElement != null)
            {
                for (var i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; ++i)
                {
                    var name = xmlDoc.DocumentElement.ChildNodes[i].SelectSingleNode(nodeToDelete);

                    if (name == null || (name.InnerText != @value))
                    {
                        continue;
                    }

                    var nodeToRemove = xmlDoc.DocumentElement.ChildNodes[i];

                    if (nodeToRemove.ParentNode != null)
                    {
                        nodeToRemove.ParentNode.RemoveChild(nodeToRemove);
                    }

                    break;
                }
            }
            xmlDoc.Save(Filename);
        }

        private void CreateNewData(string feedName, string feedURL)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.NewLineOnAttributes = true;
            using (XmlWriter writer = XmlWriter.Create(Filename, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Feed_list");

                writer.WriteStartElement("Feed");
                writer.WriteElementString("Name", feedName);
                writer.WriteElementString("Url", feedURL);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
        }

        private void LoadToOldData(string feedName, string feedURL)
        {
            XDocument xDocument = XDocument.Load(Filename);
            XElement root = xDocument.Element("Feed_list");
            IEnumerable<XElement> rows = root.Descendants("Feed");
            XElement firstRow = rows.First();
            firstRow.AddBeforeSelf(
               new XElement("Feed",
               new XElement("Name", feedName),
               new XElement("Url", feedURL)));
            xDocument.Save(Filename);
        }


    }
}
