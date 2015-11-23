using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task1
{
    public class LinqToXmlRepository : IRepository<Book>
    {
        private string Path { get; }

        public LinqToXmlRepository(string path)
        {
            Path = path;
        }

        public List<Book> LoadBooks()
        {
            try
            {
                var xDocument = XDocument.Load(Path + ".xml", LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
                var bookList = (from xml in xDocument.Elements("Books").Elements("Book")
                    select new Book
                    {
                        Author = xml.Element("Author").Value,
                        Title = xml.Element("Title").Value,
                        Price = int.Parse(xml.Element("Price").Value),
                        Pages = int.Parse(xml.Element("Pages").Value)
                    });
                return bookList.ToList();
            }
            catch
            {
                return null;
            }
        }

        public void Save(IEnumerable<Book> books)
        {
            var xDoc = new XElement("Books",
                books.Select(b => 
                    new XElement("Book",
                    new XElement("Author", b.Author),
                    new XElement("Title", b.Title),
                    new XElement("Price", b.Price),
                    new XElement("Pages", b.Price))));

            xDoc.Save(Path + ".xml");
        }
    }
}
