using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace Task1
{
    public class XmlFileBookRepository : IRepository<Book>
    {
        private string Path { get; }

        public XmlFileBookRepository(string path)
        {
            Path = path;
        }

        public List<Book> LoadBooks()
        {
            var reader = XmlReader.Create(Path + ".xml");
            var books = new List<Book>();

            try
            {
                while (reader.Read())
                {
                    if (reader.Name != "Book") continue;
                    var book = new Book();
                    while (true)
                    {
                        reader.Read();
                        reader.ReadString();
                        if (reader.Name == "Book") break;
                        var s = reader.ReadString();
                        int val;
                        if(int.TryParse(s, out val))
                            typeof(Book).GetProperty(reader.Name).SetValue(book, val);
                        else typeof(Book).GetProperty(reader.Name).SetValue(book,s);
                    }
                    books.Add(book);
                }
                return books;
            }
            catch
            {
                return null;
            }
            finally
            {
                reader.Close();
            }
        }

        public void Save(IEnumerable<Book> books)
        {
            var writer = XmlWriter.Create(Path + ".xml",
                new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Auto, Indent = true });

            writer.WriteStartDocument(true);
            writer.WriteStartElement("Books");

            var properties = typeof (Book).GetProperties();
            foreach (var book in books)
            {
                writer.WriteStartElement("Book");
                foreach (var property in properties)
                    writer.WriteElementString(property.Name, typeof(Book).GetProperty(property.Name).GetValue(book).ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Close();
        }
    }
}
