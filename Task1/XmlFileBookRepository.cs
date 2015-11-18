using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
            var fs = new FileStream(Path + ".xml", FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                var xml = new XmlSerializer(typeof(List<Book>));
                var bookList = (List<Book>)xml.Deserialize(fs);
                return bookList;
            }
            catch
            {
                return null;
            }
            finally
            {
                fs.Close();
            }
        }

        public void Save(IEnumerable<Book> books)
        {
            var f = File.Open(Path + ".xml", FileMode.Truncate);
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(List<Book>));
                s.Serialize(f, books);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                f.Close();
            }
        }
    }
}
