using System;
using System.Text;

namespace Task1
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        public Book() { }
        public Book(string author, string title, double price, int pages)
        {
            Author = author;
            Title = title;
            Price = price;
            Pages = pages;
        }

        public string Author { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Pages { get; set; }
        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Author == other.Author && Title == other.Title && Price.Equals(other.Price) && Pages == other.Pages;
        }

        public int CompareTo(Book other)
        {
            if (other == null) return -1;
            return Pages.CompareTo(other.Pages);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Author?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Title?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ Pages;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Book)obj);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Author + '/');
            sb.Append(Title + '/');
            sb.Append(Price + "y.e/");
            sb.Append(Pages + " pages/");

            return sb.ToString();
        }
    }
}

