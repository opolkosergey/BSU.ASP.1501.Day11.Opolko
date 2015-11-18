using System;
using Task1;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookService = new BookListService(new XmlFileBookRepository("f7"));
            foreach (var book in bookService.BookList)
                Console.WriteLine($"{book}");
            Console.WriteLine("------------------------------------------");
            //bookService.AddBook(new Book("J. Richter", "C# via", 500.0, 800));
            //bookService.AddBook(new Book("D. Samal", "Sifo VMSIS", 350.0, 85));
           // bookService.AddBook(new Book("A. Pushkin", "E.Onegin", 110.0, 150));
            bookService.AddBook(new Book("L. Tolstoi", "Voina i mir", 650.0, 1000));
            bookService.AddBook(new Book("S. Perro", "Kot v sapogah", 60.0, 50));

           foreach (var book in bookService.BookList)
                Console.WriteLine($"{book}");

            bookService.DeleteBook(new Book("L. Tolstoi", "Voina i mir", 650.0, 1000));
            bookService.DeleteBook(new Book("S. Perro", "Kot v sapogah", 60.0, 50));
            Console.WriteLine("------------------------------------------");

            foreach (var book in bookService.BookList)
                Console.WriteLine($"{book}");
            Console.WriteLine("------------------------------------------");
            bookService.Sort(new BookComparer());

            foreach (var book in bookService.BookList)
                Console.WriteLine($"{book}");
            Console.WriteLine("------------------------------------------");
            var selectedBooksByTag = bookService.FindBooksByTags(b => b.Author.Contains('S'.ToString()));

            foreach (var book in selectedBooksByTag)
                Console.WriteLine($"{book}");

            bookService.Repository.Save(bookService.BookList);
            Console.ReadKey();
        }
    }
}
