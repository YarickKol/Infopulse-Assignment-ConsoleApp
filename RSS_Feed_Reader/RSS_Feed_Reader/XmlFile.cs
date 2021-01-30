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

        public void DeleteData()
        {
            XElement root = XElement.Parse(@"<Root>
    <Child1>
        <GrandChild1/>
        <GrandChild2/>
        <GrandChild3/>
    </Child1>
    <Child2>
        <GrandChild4/>
        <GrandChild5/>
        <GrandChild6/>
    </Child2>
    <Child3>
        <GrandChild7/>
        <GrandChild8/>
        <GrandChild9/>
    </Child3>
</Root>");
            root.Element("Feed").Element("GrandChild1").Remove();
            root.Element("Child2").Elements().ToList().Remove();
            root.Element("Child3").Elements().Remove();
            Console.WriteLine(root);
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
