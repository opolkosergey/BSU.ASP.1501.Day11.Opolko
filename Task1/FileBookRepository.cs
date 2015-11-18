using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class FileBookRepository : IRepository<Book>
    {
        private string Path { get; }

        public FileBookRepository(string path)
        {
            Path = path;
        }

        public List<Book> LoadBooks()
        {
            var books = new List<Book>();

            var sourceFile = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read);
            var reader = new BinaryReader(sourceFile);
            while (reader.PeekChar() > -1)
            {
                string author = reader.ReadString();
                string title = reader.ReadString();
                double price = reader.ReadDouble();
                int pages = reader.ReadInt32();
                books.Add(new Book(author, title, price, pages));
            }
            reader.Dispose();
            sourceFile.Close();
            return books;
        }

        public void Save(IEnumerable<Book> books)
        {
            if (books == null)
                throw new ArgumentNullException(nameof(books));

            var file = new FileStream(Path, FileMode.Truncate, FileAccess.Write);
            var writer = new BinaryWriter(file);

            foreach (var item in books)
            {
                writer.Write(item.Author);
                writer.Write(item.Title);
                writer.Write(item.Price);
                writer.Write(item.Pages);
            }

            writer.Dispose();
            file.Close();
        }
    }
}
